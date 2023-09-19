using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class CharacterService : NestedServiceBase
    {
        [SerializeField] TimeMode CharacterTime;

        public override float DeltaTime => CharacterTime.DeltaTime;
        public override float Time => CharacterTime.DeltaTime;

        [SerializeField] protected bool m_IsPlayer;

        [SerializeField] MotionInfo m_MotionInfo;


        Transform m_CachedTransfrom;

        [SerializeField]
        GameObject m_GameObject;


        public MotionInfo MotionInfo
        {
            get
            {
                return m_MotionInfo;
            }
            private set
            {
                m_MotionInfo = value;
            }
        }

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

        public void SetMotionInfo(MotionInfo motionInfo)
        {
            this.MotionInfo = motionInfo;
        }

        public void SetIsPlayer(bool val)
        {
            this.IsPlayer = val;
        }

        CharacterInputService m_CharacterInputService;

        public CharacterInputService CharacterInput
        {
            get
            {
                return m_CharacterInputService;
            }
        }

        CharacterCameraService m_CharacterCameraService;

        public CharacterCameraService CharacterCamera
        {
            get
            {
                return m_CharacterCameraService;
            }
        }

        DriverService m_DriverService;


        public DriverService Driver
        {
            get
            {
                return m_DriverService;
            }
        }


        MotionActionService m_MotionActionService;


        public MotionActionService MotionAction
        {
            get
            {
                return m_MotionActionService;
            }
        }


        PlayerService m_PlayerService;


        public PlayerService PlayerService
        {
            get
            {
                return m_PlayerService;
            }
        }

        InteractionService m_InteractionService;

        public InteractionService InteractionService
        {
            get
            {
                return m_InteractionService;
            }
        }




        public CharacterService(GameObject gameObject)
        {
            m_GameObject = gameObject;
            m_CharacterInputService = new CharacterInputService(this);
            m_CharacterCameraService = new CharacterCameraService(this);
            m_DriverService = new DriverService(this);
            m_MotionActionService = new MotionActionService(this);
            m_PlayerService = new PlayerService(this);
            m_InteractionService = new InteractionService(this);


            AddService(m_CharacterInputService);
            AddService(m_CharacterCameraService);
            AddService(m_DriverService);
            AddService(m_MotionActionService);
            AddService(m_PlayerService);
            AddService(m_InteractionService);
        }


    }
}