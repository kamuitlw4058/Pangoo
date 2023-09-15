using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class CharacterContainer : NestedServiceBase
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

        public CharacterContainer(GameObject gameObject)
        {
            m_GameObject = gameObject;
            AddService(new CharacterInputService(this));
            AddService(new CharacterCameraService(this));
            AddService(new DriverCharacterController(this));
            AddService(new MotionActionService(this));
            AddService(new PlayerService(this));
        }


    }
}