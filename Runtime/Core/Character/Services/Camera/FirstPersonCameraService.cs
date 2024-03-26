using System;
using UnityEngine;
using Pangoo.Core.Services;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using Cinemachine;

namespace Pangoo.Core.Characters
{

    [Serializable]
    public class FirstPersonCameraService : CharacterBaseService
    {
        private Vector2 m_AnglesCurrent = new Vector2(0, 0f);
        private Vector2 m_AnglesTarget = new Vector2(0, 0f);


        private const float _threshold = 0.01f;


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

        public FirstPersonCameraService(NestedBaseService parent) : base(parent)
        {
        }

        public void SetRotation(Quaternion rotation)
        {
            this.m_AnglesTarget = rotation.eulerAngles;
            this.m_AnglesCurrent = rotation.eulerAngles;
        }

        public void SetDirection(Vector3 direction)
        {
            this.SetRotation(Quaternion.Euler(direction));
        }

        public float CameraIncluded(Vector3 position)
        {
            if (CameraTransform == null)
            {
                return 0;
            }

            Vector3 forwardVector = CameraTransform.forward;
            Vector3 targetVector = (position - CameraTransform.position).normalized;
            float angle = Vector3.Dot(forwardVector, targetVector);
            return angle;
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

        protected override void DoStart()
        {
            SetFirstPerson();

        }

        protected override void DoAwake()
        {
            Debug.Log($"Character:{Character}");
            Debug.Log($"Character.gameObject:{Character.gameObject}");
            m_VirtualCamera = Character.gameObject.GetComponentInChildren<CinemachineVirtualCamera>();
            if (m_VirtualCamera == null)
            {
                var go = new GameObject("VirtualCamera");
                go.transform.parent = Character.CachedTransfrom;
                m_VirtualCamera = go.AddComponent<CinemachineVirtualCamera>();
            }
        }

        protected override void DoUpdate()
        {
            CameraRotation();
        }



        private void CameraRotation()
        {
            // if there is an input

            Vector2 deltaInput = Character.CharacterInput.InputRotation;
            // Debug.Log($"deltaInput:{deltaInput}");

            this.ComputeInput(new Vector2(
                deltaInput.x * DeltaTime * Character.MotionInfo.RotationSpeedX,
                deltaInput.y * DeltaTime * Character.MotionInfo.RotationSpeedY
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

            if (Character.IsYAxisClamp)
            {
                m_VirtualCamera.transform.localRotation = rotation;
            }
            else
            {
                m_VirtualCamera.transform.rotation = rotation;
            }
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

        CinemachineTransposer m_CinemachineTransposer;


        public CinemachineTransposer Transposer
        {
            get
            {

                if (m_CinemachineTransposer == null)
                {
                    m_CinemachineTransposer = m_VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
                    if (m_CinemachineTransposer == null)
                    {
                        m_CinemachineTransposer = m_VirtualCamera.AddCinemachineComponent<CinemachineTransposer>();
                        m_CinemachineTransposer.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
                        m_CinemachineTransposer.m_FollowOffset = Vector3.zero;
                        m_CinemachineTransposer.m_XDamping = 0;
                        m_CinemachineTransposer.m_YawDamping = 0;
                        m_CinemachineTransposer.m_YDamping = 0;
                        m_CinemachineTransposer.m_ZDamping = 0;
                    }
                }

                if (m_CinemachineTransposer == null)
                {
                    LogError($"When GetCinemachineTransposer Inited.CinemachineTransposer  still null!");
                }

                return m_CinemachineTransposer;
            }
        }


        [ShowInInspector]
        public Vector3 CameraOffset
        {
            get
            {
                return Transposer?.m_FollowOffset ?? Vector3.zero;
            }
            set
            {
                if (Transposer != null)
                {
                    Transposer.m_FollowOffset = value;
                }
            }
        }

        [ShowInInspector]
        public float CameraYOffset
        {
            get
            {
                return Transposer?.m_FollowOffset.y ?? 0;
            }
            set
            {
                if (Transposer != null)
                {
                    Transposer.m_FollowOffset = new Vector3(Transposer.m_FollowOffset.x, value, Transposer.m_FollowOffset.z);
                }

            }
        }

        public void SetFirstPerson()
        {
            Debug.Log($"m_VirtualCamera:{m_VirtualCamera} Character:{Character}");
            m_VirtualCamera.Follow = Character.CachedTransfrom;
            m_VirtualCamera.DestroyCinemachineComponent<CinemachineComposer>();
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
            float xAngle = Character.xAxisMaxPitch / 2f;

            m_AnglesTarget.x = Mathf.Clamp(m_AnglesTarget.x, -xAngle, xAngle);
            if (Character.IsYAxisClamp)
            {
                float yAngle = Character.yAxisMaxPitch / 2f;
                m_AnglesTarget.y = Mathf.Clamp(m_AnglesTarget.y, -yAngle, yAngle);
            }
            else
            {
                if (m_AnglesTarget.y < 0f) m_AnglesTarget.y += 360f;
                if (m_AnglesTarget.y >= 360f) m_AnglesTarget.y -= 360f;
            }

        }

        private void ComputeInput(Vector2 deltaInput)
        {
            this.m_AnglesTarget += new Vector2(
                deltaInput.y,
                deltaInput.x
            );
        }

    }

}

