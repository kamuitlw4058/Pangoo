using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using System;
using System.Collections.Generic;

namespace Pangoo
{
    public class EntityLoader : IReference
    {
        private Dictionary<int, Action<Entity>> dicCallback;
        private Dictionary<int, Entity> dicSerial2Entity;


        private List<int> tempList;

        public object Owner
        {
            get;
            private set;
        }

        public EntityLoader()
        {
            dicSerial2Entity = new Dictionary<int, Entity>();
            dicCallback = new Dictionary<int, Action<Entity>>();
            tempList = new List<int>();
            Owner = null;
        }


        public void AttachEntity(Entity child, int parentEntityId, string path)
        {
            PangooEntry.Entity.AttachEntity(child, parentEntityId, path);
        }

        public int ShowEntity(EnumEntity enumEntity, Action<Entity> onShowSuccess, EntityInfo entityInfo, object userData = null)
        {
            int serialId = PangooEntry.Entity.ShowEntity(enumEntity, entityInfo, userData);
            if (serialId != 0)
            {
                dicCallback.Add(serialId, onShowSuccess);
            }
            return serialId;
        }


        public bool HasEntity(int serialId)
        {
            return GetEntity(serialId) != null;
        }

        public Entity GetEntity(int serialId)
        {
            if (dicSerial2Entity.ContainsKey(serialId))
            {
                return dicSerial2Entity[serialId];
            }

            return null;
        }

        public IEnumerable<Entity> GetAllEntities()
        {
            return dicSerial2Entity.Values;
        }

        public void HideEntity(int serialId)
        {
            Entity entity = null;
            if (!dicSerial2Entity.TryGetValue(serialId, out entity))
            {
                Log.Error("Can find entity('serial id:{0}') ", serialId);
            }

            dicSerial2Entity.Remove(serialId);
            dicCallback.Remove(serialId);

            if (entity == null)
            {
                return;
            }

            Entity[] entities = PangooEntry.Entity.GetChildEntities(entity);
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    //若Child Entity由这个Loader对象托管，则由此Loader释放
                    if (dicSerial2Entity.ContainsKey(item.Id))
                    {
                        HideEntity(item);
                    }
                    else//若Child Entity不由这个Loader对象托管，则从Parent Entity脱离
                        PangooEntry.Entity.DetachEntity(item);
                }
            }

            PangooEntry.Entity.HideEntity(entity);
        }

        public void HideEntity(Entity entity)
        {
            if (entity == null)
                return;

            HideEntity(entity.Id);
        }

        public void HideAllEntity()
        {
            tempList.Clear();

            foreach (var entity in dicSerial2Entity.Values)
            {
                Entity parentEntity = PangooEntry.Entity.GetParentEntity(entity);
                //有ParentEntity
                if (parentEntity != null)
                {
                    //若Parent Entity由这个Loader对象托管，则把这个Child Entity从数据中移除，在隐藏Parent Entity，GF内部会处理Child Entity
                    if (dicSerial2Entity.ContainsKey(parentEntity.Id))
                    {
                        dicSerial2Entity.Remove(entity.Id);
                        dicCallback.Remove(entity.Id);
                    }
                    //若Parent Entity不由这个Loader对象托管，则从Parent Entity脱离
                    else
                    {
                        PangooEntry.Entity.DetachEntity(entity);
                    }
                }
            }

            foreach (var serialId in dicSerial2Entity.Keys)
            {
                tempList.Add(serialId);
            }

            foreach (var serialId in tempList)
            {
                HideEntity(serialId);
            }

            dicSerial2Entity.Clear();
            dicCallback.Clear();
        }

        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne == null)
            {
                return;
            }

            Action<Entity> callback = null;
            if (!dicCallback.TryGetValue(ne.Entity.Id, out callback))
            {
                return;
            }

            dicSerial2Entity.Add(ne.Entity.Id, ne.Entity);
            callback?.Invoke(ne.Entity);
        }

        private void OnShowEntityFail(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            if (ne == null)
            {
                return;
            }

            if (dicCallback.ContainsKey(ne.EntityId))
            {
                dicCallback.Remove(ne.EntityId);
                Log.Warning("{0} Show entity failure with error message '{1}'.", Owner.ToString(), ne.ErrorMessage);
            }
        }

        public static EntityLoader Create(object owner)
        {
            EntityLoader entityLoader = ReferencePool.Acquire<EntityLoader>();
            entityLoader.Owner = owner;
            PangooEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, entityLoader.OnShowEntitySuccess);
            PangooEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, entityLoader.OnShowEntityFail);

            return entityLoader;
        }

        public void Clear()
        {
            Owner = null;
            dicSerial2Entity.Clear();
            dicCallback.Clear();
            PangooEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            PangooEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFail);
        }
    }
}
