using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.MetaTable;
using System;

namespace Pangoo
{
    public abstract class EntityData : EntityInfo, IReference
    {
        protected Space m_Space = Space.Self;

        protected Vector3 m_Position = Vector3.zero;

        protected Quaternion m_Rotation = Quaternion.identity;

        public EntityData()
        {
        }

        /// <summary>
        /// 实体位置。
        /// </summary>
        public virtual Vector3 Position
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
        public virtual Quaternion Rotation
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


        public override void Clear()
        {
            base.Clear();
            m_Position = Vector3.zero;
            m_Rotation = Quaternion.identity;
            m_Space = Space.World;
            UserData = null;
        }
    }
}
