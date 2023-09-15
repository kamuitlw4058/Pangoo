using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using Cinemachine;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class FirstPersonCameraService : CharacterBaseService
    {
        private Vector2 m_AnglesCurrent = new Vector2(0, 0f);
        private Vector2 m_AnglesTarget = new Vector2(0, 0f);



        private const float _threshold = 0.01f;

        public InputValueVector2Base m_Input;


        public float RotationSpeedX = 60;

        public float RotationSpeedY = 60;

        private float m_VelocityX;
        private float m_VelocityY;

        [SerializeField] private float m_SmoothTime = 0.1f;

        [SerializeField, Range(1f, 179f)] private float m_MaxPitch = 60f;


        [Header("Cinemachine")]

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 90.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -90.0f;

        public bool reverse = false;

        public FirstPersonCameraService(INestedService parent) : base(parent)
        {
            m_Input = InputValueVector2MotionSecondary.Create();
        }

        public void SetRotation(Quaternion rotation)
        {
            this.m_AnglesTarget = rotation.eulerAngles;
            this.m_AnglesCurrent = rotation.eulerAngles;
        }

        public void SetDirection(Vector3 direction)
        {
            this.SetRotation(Quaternion.LookRotation(direction, Vector3.up));
        }




        protected CinemachineVirtualCamera m_VirtualCamera;

        public Transform CameraTransform
        {
            get
            {
                if (m_VirtualCamera == null)
                {
                    return null;
                }
                return m_VirtualCamera.transform;
            }
        }

        [ShowInInspector]
        public CinemachineVirtualCamera Camera
        {
            get
            {
                return m_VirtualCamera;
            }
        }


        public override int Priority
        {
            get
            {
                return 0;
            }
        }

        public override void DoStart()
        {
            SetFirstPerson();

        }

        public override void DoAwake(INestedService parent)
        {
            base.DoAwake(parent);
            m_Input.OnAwake();
            // m_InputMove = InputValueVector2MotionPrimary.Create();
            m_VirtualCamera = Character.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
            if (m_VirtualCamera == null)
            {
                var go = new GameObject("VirtualCamera");
                go.transform.parent = Character.CachedTransfrom;
                m_VirtualCamera = go.AddComponent<CinemachineVirtualCamera>();
            }
        }

        public override void DoUpdate()
        {
            this.m_Input?.OnUpdate();
            CameraRotation();
        }

        private void CameraRotation()
        {
            // if there is an input

            Vector2 deltaInput = this.m_Input.Read();
            // Debug.Log($"deltaInput:{deltaInput}");

            this.ComputeInput(new Vector2(
                deltaInput.x * DeltaTime * RotationSpeedX,
                deltaInput.y * DeltaTime * RotationSpeedY
            ));

            this.ConstrainTargetAngles();

            this.m_AnglesCurrent = new Vector2(
                this.GetRotationDamp(
                    this.m_AnglesCurrent.x,
                    this.m_AnglesTarget.x,
                    ref this.m_VelocityX,
                    this.m_SmoothTime,
                    DeltaTime),
                this.GetRotationDamp(
                    this.m_AnglesCurrent.y,
                    this.m_AnglesTarget.y,
                    ref this.m_VelocityY,
                    this.m_SmoothTime,
                    DeltaTime)
            );

            // Vector3 position = this.GetTargetPosition(shotType);
            Quaternion rotation = Quaternion.Euler(
                this.m_AnglesCurrent.x,
                this.m_AnglesCurrent.y,
                0f
            );

            m_VirtualCamera.transform.localRotation = rotation;
            // Debug.Log($"set Rotation:{rotation}");


            // if (input_val.magnitude >= _threshold)
            // {
            //     //Don't multiply mouse input by Time.deltaTime
            //     // float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            //     float deltaTimeMultiplier = 1;

            //     _cinemachineTargetPitch += input_val.y * RotationSpeed * deltaTimeMultiplier * (reverse ? -1 : 1);
            //     _rotationVelocity = input_val.x * RotationSpeed * deltaTimeMultiplier * (reverse ? -1 : 1);


            //     // clamp our pitch rotation
            //     _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);


            //     // Update Cinemachine camera target pitch
            //     CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

            //     // rotate the player left and right
            //     transform.Rotate(Vector3.up * _rotationVelocity);
            // }
        }


        public void SetFirstPerson()
        {
            Debug.Log($"m_VirtualCamera:{m_VirtualCamera} Character:{Character}");
            Debug.Log($"Character:{Character.CachedTransfrom}");
            m_VirtualCamera.Follow = Character.CachedTransfrom;
            var transposer = m_VirtualCamera.AddCinemachineComponent<CinemachineTransposer>();
            transposer.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
            transposer.m_FollowOffset = Vector3.zero;
            transposer.m_XDamping = 0;
            transposer.m_YawDamping = 0;
            transposer.m_ZDamping = 0;
        }

        private float GetRotationDamp(float current, float target, ref float velocity,
            float smoothTime, float deltaTime)
        {
            return Mathf.SmoothDampAngle(
                current,
                target,
                ref velocity,
                smoothTime,
                Mathf.Infinity,
                deltaTime
            );
        }

        private void ConstrainTargetAngles()
        {
            float angle = this.m_MaxPitch / 2f;
            m_AnglesTarget.x = Mathf.Clamp(m_AnglesTarget.x, -angle, angle);

            if (m_AnglesTarget.y < 0f) m_AnglesTarget.y += 360f;
            if (m_AnglesTarget.y >= 360f) m_AnglesTarget.y -= 360f;
        }

        public override void DoDestroy()
        {
            m_Input.OnDestroy();
            base.DoDestroy();
        }

        public override void DoEnable()
        {
            base.DoEnable();
            m_Input.Active = true;
        }
        public override void DoDisable()
        {
            m_Input.Active = false;
            base.DoDisable();
        }

        // private Vector3 GetTargetPosition(TShotType shotType)
        // {
        //     Character target = this.m_Target.Get<Character>(shotType.Args);
        //     if (target == null) return this.m_LastTargetPosition;

        //     return target.transform.position + this.m_Offset.Get(shotType.Args);
        // }

        private void ComputeInput(Vector2 deltaInput)
        {
            this.m_AnglesTarget += new Vector2(
                deltaInput.y,
                deltaInput.x
            );
        }

    }

}

