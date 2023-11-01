//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class EntityData : IReference
    {
        protected Space m_Space = Space.Self;

        protected Vector3 m_Position = Vector3.zero;

        protected Quaternion m_Rotation = Quaternion.identity;

        public EntityData()
        {
            m_Position = Vector3.zero;
            m_Rotation = Quaternion.identity;
            UserData = null;
        }

        /// <summary>
        /// 实体位置。
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        public virtual Space Space
        {
            get
            {
                return m_Space;
            }
            set
            {
                m_Space = value;
            }
        }

        /// <summary>
        /// 实体朝向。
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                return m_Rotation;
            }
            set
            {
                m_Rotation = value;
            }
        }

        public object UserData
        {
            get;
            protected set;
        }

        public static EntityData Create(object userData = null)
        {
            EntityData entityData = ReferencePool.Acquire<EntityData>();
            entityData.Position = Vector3.zero;
            entityData.Rotation = Quaternion.identity;
            entityData.UserData = userData;
            entityData.Space = Space.World;
            return entityData;
        }

        public static EntityData Create(Vector3 position, object userData = null)
        {
            EntityData entityData = ReferencePool.Acquire<EntityData>();
            entityData.Position = position;
            entityData.Rotation = Quaternion.identity;
            entityData.UserData = userData;
            entityData.Space = Space.World;
            return entityData;
        }

        public static EntityData Create(Vector3 position, Quaternion quaternion, object userData = null)
        {
            EntityData entityData = ReferencePool.Acquire<EntityData>();
            entityData.Position = position;
            entityData.Rotation = quaternion;
            entityData.UserData = userData;
            entityData.Space = Space.World;
            return entityData;
        }

        public virtual void Clear()
        {
            m_Position = Vector3.zero;
            m_Rotation = Quaternion.identity;
            m_Space = Space.World;
            UserData = null;
        }
    }
}
