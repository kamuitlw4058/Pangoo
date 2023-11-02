using System;
using UnityEngine;
using Pangoo.Core.Services;
using Pangoo.Core.Common;

namespace Pangoo.Core.Characters
{

    [Serializable]
    public partial class Character : MonoMasterService
    {

        public MainService Main { get; set; }


        [SerializeField] protected bool m_IsPlayer;

        [SerializeField] MotionInfo m_MotionInfo;

        public Vector3 CameraOffset { get; set; }

        public float MaxPitch { get; set; }

        public bool IsControllable { get; set; }

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

        public IInteractive Target => m_InteractionService?.Target;


        public void SetMotionInfo(MotionInfo motionInfo)
        {
            this.MotionInfo = motionInfo;
        }

        public void SetIsPlayer(bool val)
        {
            this.IsPlayer = val;
        }

        public void SetDirection(Vector3 direction)
        {
            m_CharacterCameraService.SetDirection(direction);
        }

        public float CameraIncluded(Vector3 position)
        {
            return m_CharacterCameraService.CameraIncluded(position);
        }

        public void ResetCameraDirection()
        {
            m_CharacterCameraService.SetDirection(gameObject.transform.rotation.eulerAngles);

        }

        public void SetCameraOffset(Vector3 offset)
        {
            CameraOffset = offset;
            m_CharacterCameraService.SetCameraOffset(offset);
        }


        public Character(GameObject gameObject, bool onlyCamera = false) : base(gameObject)
        {
            m_CharacterInputService = new CharacterInputService(this);
            m_CharacterCameraService = new CharacterCameraService(this);
            m_DriverService = new DriverService(this);
            m_MotionActionService = new MotionActionService(this);
            m_PlayerService = new PlayerService(this);
            m_InteractionService = new InteractionService(this);


            AddService(m_CharacterInputService);
            AddService(m_CharacterCameraService);

            if (!onlyCamera)
            {
                AddService(m_DriverService);
                AddService(m_MotionActionService);
                AddService(m_PlayerService);
                AddService(m_InteractionService);
            }
        }


        public void Interact()
        {
            if (m_InteractionService != null)
            {
                m_InteractionService.Interact();
            }
        }


    }
}