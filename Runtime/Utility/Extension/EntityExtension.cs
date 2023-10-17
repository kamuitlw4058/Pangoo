using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using System;

namespace Pangoo
{
    public static class EntityExtension
    {
        private static int s_SerialId = 0;



        // public static void ShowEntity<T>(this EntityComponent entityComponent, int serialId, EnumEntity enumEntity, object userData = null)
        // {
        //     entityComponent.ShowEntity(serialId, enumEntity, typeof(T), userData);
        // }

        // public static int ShowEntity(this EntityComponent entityComponent,  EnumEntity enumEntity, Type logicType, object userData = null)
        // {
        //     return  entityComponent.ShowEntity( (int)enumEntity, logicType, userData);
        // }

        // public static int ShowEntity<T>(this EntityComponent entityComponent,  int entityId,string path, string groupName, object userData = null)
        // {
        //     return entityComponent.ShowEntity( entityId, typeof(T), path,groupName, userData);
        // }

        public static int ShowEntity(this EntityComponent entityComponent, EnumEntity enumEntity, EntityInfo entityInfo, object userData = null)
        {
            // Data.EntityData entityData = GameEntry.Data.GetData<DataEntity>().GetEntityData(entityId);



            if (entityInfo == null)
            {
                // Log.Error("Can not load entity id '{0}' from data table.", entityId.ToString());
                return 0;
            }

            Type LogicType = null;
            switch (enumEntity)
            {
                case EnumEntity.None:
                    return 0;
                case EnumEntity.StaticScene:
                    LogicType = typeof(EntityStaticScene);
                    break;
                case EnumEntity.DynamicObject:
                    LogicType = typeof(EntityDynamicObject);
                    break;
                case EnumEntity.Character:
                    LogicType = typeof(EntityCharacter);
                    break;


            }

            if (LogicType == null)
            {
                return 0;
            }

            // 真是的去创建实体。
            var serialId = entityComponent.GenerateSerialId();
            Debug.Log($"entityComponent:{entityComponent} entityInfo:{entityInfo} entityInfo:{entityInfo.GroupName}");
            if (!entityComponent.HasEntityGroup(entityInfo.GroupName))
            {
                // PoolParamData poolParamData = entityData.EntityGroupData.PoolParamData;
                // PangooEntry.Entity.AddEntityGroup(entityInfo.GroupName, poolParamData.InstanceAutoReleaseInterval, poolParamData.InstanceCapacity, poolParamData.InstanceExpireTime, poolParamData.InstancePriority);
                PangooEntry.Entity.AddEntityGroup(entityInfo.GroupName, (float)entityInfo.GroupInstanceAutoReleaseInterval, entityInfo.GroupInstanceCapacity, (float)entityInfo.GroupInstanceExpireTime, entityInfo.GroupInstancePriority);
            }

            entityComponent.ShowEntity(serialId, LogicType, entityInfo.AssetPath, entityInfo.GroupName, Constant.AssetPriority.EntityAsset, userData);
            return serialId;
        }

        private static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return ++s_SerialId;
        }

    }

}