using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Services;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Pangoo.Core.Characters
{

    [Serializable]
    public partial class Character : MonoMasterService
    {
        [ShowInInspector]
        public ICharacterRow Row { get; set; }

        public MainService Main { get; set; }


        [SerializeField] protected bool m_IsPlayer;

        [SerializeField] MotionInfo m_MotionInfo;


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

        [ShowInInspector]
        [LabelText("热点UI的开关")]
        public bool EnabledHotspot { get; set; } = true;


        public bool IsPlayer
        {
            get => this.m_IsPlayer;
            set
            {

                this.m_IsPlayer = value;
            }
        }

        public IInteractive Target => m_InteractionService?.Target;

        public CharacterController CharacterController
        {
            get => gameObject.GetComponent<CharacterController>();
        }

        public EntityCharacter Entity { get; set; }

        protected override void DoAwake()
        {
            DoAwakeSubDynamicObject();
            base.DoAwake();
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

        public void SetCharacterSpeed(float walkVal,float runVal)
        {
            m_MotionInfo.SetLinearSpeed(walkVal);
            m_MotionInfo.SetRunSpeed(runVal);
        }

        float m_CameraHeight;


        [ShowInInspector]
        public Vector3 CameraOffset
        {
            get
            {
                return new Vector3(0, m_CharacterCameraService.CameraYOffset);
            }
        }

        [LabelText("相机高度")]
        [ShowInInspector]
        public float CameraHight
        {
            get
            {
                return m_CharacterCameraService.CameraYOffset + (m_DriverService.ColliderHeight / 2);
            }
            set
            {
                m_CameraHeight = value;
                m_CharacterCameraService.CameraYOffset = m_CameraHeight - (m_DriverService.ColliderHeight / 2);
            }
        }

        [LabelText("碰撞高度")]
        [ShowInInspector]
        public float ColliderHight
        {
            get
            {
                return m_DriverService.ColliderHeight;
            }
            set
            {
                m_DriverService.ColliderHeight = value;
                CameraHight = m_CameraHeight;
            }
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

        public bool MoveStepChanged
        {
            get { return m_CharacterInputService?.MoveStepChanged ?? false; }
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

        public T GetVariable<T>(string uuid)
        {
            return Main.RuntimeData.GetVariable<T>(uuid);
        }


    }
}