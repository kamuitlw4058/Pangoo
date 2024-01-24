using System;
using UnityEngine;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Characters
{

    public abstract class CharacterControllerService<T> : CharacterBaseService where T : Enum
    {
        [SerializeField]
        protected T m_ServiceType = default(T);

        public CharacterControllerService(NestedBaseService parent) : base(parent)
        {
        }


        public T ServiceType
        {
            get
            {
                return m_ServiceType;
            }
            set
            {
                ChangeServiceType(m_ServiceType, value);
                m_ServiceType = value;
            }
        }

        public virtual void RemoveService(T val)
        {

        }

        public virtual void SetDriverInfo(DriverInfo driverInfo)
        {
            
        }

        public virtual void SetCharacterControllerHeight(float val)
        {
            
        }

        public virtual void AddService(T val)
        {

        }


        public virtual void ChangeServiceType(T oldType, T newType, bool overwrite = false)
        {
            if (oldType.Equals(newType) && !overwrite)
            {
                return;
            }

            RemoveService(oldType);
            AddService(newType);
        }

        protected override void DoStart()
        {
            ChangeServiceType(m_ServiceType, m_ServiceType, true);

        }

    }

}

