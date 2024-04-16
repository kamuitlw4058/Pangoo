//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;

namespace Pangoo
{
        public abstract class EntityBase : EntityLogic
        {

                public virtual string EntityName => "Entity";
                public void Log(string message)
                {
                        Debug.Log($"{EntityName}-> {message}");
                }

                public void LogError(string message)
                {
                        Debug.LogError($"{EntityName}-> {message}");
                }


                [SerializeField]
                private EntityData m_EntityData = null;

                [ShowInInspector]
                public int Id
                {
                        get
                        {
                                return Entity.Id;
                        }
                }

                public Animation CachedAnimation
                {
                        get;
                        private set;
                }

                EventHelper m_EventHelper;

                public EventHelper EventHelper
                {
                        get
                        {
                                return m_EventHelper;
                        }
                }


#if UNITY_2017_3_OR_NEWER
                protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
                {
                        base.OnInit(userData);
                        CachedAnimation = GetComponent<Animation>();
                }

#if UNITY_2017_3_OR_NEWER
                protected override void OnRecycle()
#else
        protected internal override void OnRecycle()
#endif
                {
                        base.OnRecycle();
                }

#if UNITY_2017_3_OR_NEWER
                protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
                {
                        base.OnShow(userData);

                        m_EntityData = userData as EntityData;
                        if (m_EntityData == null)
                        {
                                LogError("Entity data is invalid.");
                                return;
                        }

                        Name = Utility.Text.Format("[Entity {0}]", Id.ToString());
                        CachedTransform.localPosition = m_EntityData.Position;
                        CachedTransform.localRotation = m_EntityData.Rotation;
                        CachedTransform.localScale = Vector3.one;
                        m_EventHelper = EventHelper.Create(this);
                }

#if UNITY_2017_3_OR_NEWER
                protected override void OnHide(bool isShutdown, object userData)
#else
        protected internal override void OnHide(bool isShutdown, object userData)
#endif
                {
                        base.OnHide(isShutdown, userData);
                        if (m_EventHelper != null)
                        {
                                m_EventHelper.UnSubscribeAll();
                                ReferencePool.Release(m_EventHelper);
                        }

                        // ReferencePool.Release(m_EntityData);
                }

#if UNITY_2017_3_OR_NEWER
                protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
#else
        protected internal override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
#endif
                {
                        base.OnAttached(childEntity, parentTransform, userData);
                }

#if UNITY_2017_3_OR_NEWER
                protected override void OnDetached(EntityLogic childEntity, object userData)
#else
        protected internal override void OnDetached(EntityLogic childEntity, object userData)
#endif
                {
                        base.OnDetached(childEntity, userData);
                }

#if UNITY_2017_3_OR_NEWER
                protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
#else
        protected internal override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
#endif
                {
                        base.OnAttachTo(parentEntity, parentTransform, userData);
                }

#if UNITY_2017_3_OR_NEWER
                protected override void OnDetachFrom(EntityLogic parentEntity, object userData)
#else
        protected internal override void OnDetachFrom(EntityLogic parentEntity, object userData)
#endif
                {
                        base.OnDetachFrom(parentEntity, userData);
                }

#if UNITY_2017_3_OR_NEWER
                protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
                {
                        base.OnUpdate(elapseSeconds, realElapseSeconds);
                }


                public virtual void OnUpdateEntityData(EntityData entityData)
                {

                }
        }
}
