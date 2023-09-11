using System;
using UnityEngine;
using Pangoo.Service;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class CharacterContainer : LifeCycleServiceContainer
    {
        [SerializeField] protected bool m_IsPlayer;

        Transform m_CachedTransfrom;

        [SerializeField]
        GameObject m_GameObject;

        public GameObject gameObject
        {
            get
            {
                return m_GameObject;
            }
            private set
            {
                m_GameObject = value;
            }
        }

        public Transform CachedTransfrom
        {
            get
            {
                if (m_CachedTransfrom == null)
                {
                    m_CachedTransfrom = m_GameObject.transform;
                }
                return m_CachedTransfrom;
            }
        }

        public bool IsPlayer
        {
            get => this.m_IsPlayer;
            set
            {
                //TODO: 全局变量的设置。来通知变更。
                //ShortcutPlayer.Change(value ? this.gameObject : null);
                this.m_IsPlayer = value;

                // switch (this.m_IsPlayer)
                // {
                //     case true: this.EventChangeToPlayer?.Invoke(); break;
                //     case false: this.EventChangeToNPC?.Invoke(); break;
                // }
            }
        }

        public CharacterContainer(GameObject gameObject)
        {
            m_GameObject = gameObject;
            RegisterService(new CharacterCameraService());
            RegisterService(new DriverCharacterController());
            RegisterService(new MotionActionService());
            RegisterService(new PlayerDirectionalService());
        }


    }
}