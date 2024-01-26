using System;
using UnityEngine;
using Pangoo.Core.Services;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Pangoo.Core.Characters
{

    [Serializable]
    public partial class Character : MonoMasterService
    {

        public MainService Main { get; set; }


        [SerializeField] protected bool m_IsPlayer;

        [SerializeField] MotionInfo m_MotionInfo;

        public Vector3 OriginalCameraOffset { get; set; }
        public Vector3 CameraOffset { get; set; }
        [ShowInInspector]
        public float xAxisMaxPitch { get; set; }

        [ShowInInspector]
        public float yAxisMaxPitch = 360;

        [ShowInInspector]
        public bool IsYAxisClamp
        {
            get
            {
                if (yAxisMaxPitch >= 360 || yAxisMaxPitch < 0)
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsControllable { get; set; }

        [ShowInInspector]
        public bool IsInteractive { get; set; } = true;

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

        public DriverInfo DriverInfo
        {
            get;
            set;
        }

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

        public void SetCharacterSpeed(float val)
        {
            m_MotionInfo.SetLinearSpeed(val);
        }

        public void SetCameraOffset(Vector3 offset)
        {
            CameraOffset = offset;
            m_CharacterCameraService.SetCameraOffset(offset);
        }

        public void SetCamreaHightFollowCharacterHeight(float height)
        {
            float offsetY = height-DriverInfo.Height+OriginalCameraOffset.y;
            //Debug.Log($"offsetY:{offsetY},height:{height},DriverInfo.Height:{DriverInfo.Height},OriginalCameraOffset.y:{OriginalCameraOffset.y}");
            SetCameraOffset(new Vector3(CameraOffset.x,offsetY,CameraOffset.z));
        }

        public void SetDriverInfo(DriverInfo driverInfo)
        {
            m_DriverService.SetDriverInfo(driverInfo);
        }
        
        public bool EnabledFootstep
        {
            get
            {
                return m_FootstepsService.Enabled;
            }
            set
            {
                m_FootstepsService.Enabled = value;
            }
        }
        
        public bool IsMoveInputDown
        {
            get { return m_CharacterInputService?.IsMoveInputDown ?? false; }
        }

        public void SetCharacterHeight(float val)
        {
            m_DriverService.SetCharacterControllerHeight(val);
        }

        public Character(GameObject gameObject, bool onlyCamera = false) : base(gameObject)
        {
            m_CharacterInputService = new CharacterInputService(this);
            m_CharacterCameraService = new CharacterCameraService(this);
            m_DriverService = new DriverService(this);
            m_MotionActionService = new MotionActionService(this);
            m_PlayerService = new PlayerService(this);
            m_InteractionService = new InteractionService(this);
            m_FootstepsService = new FootstepsService(this);


            AddService(m_CharacterInputService);
            AddService(m_CharacterCameraService);
            AddService(m_InteractionService);

            if (!onlyCamera)
            {
                AddService(m_DriverService);
                AddService(m_MotionActionService);
                AddService(m_PlayerService);
                AddService(m_FootstepsService);
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