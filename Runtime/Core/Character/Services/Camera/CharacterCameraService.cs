using System;
using UnityEngine;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using Cinemachine;


namespace Pangoo.Core.Characters
{

    // [Serializable]
    public class CharacterCameraService : CharacterControllerService<CharacterCameraTypeEnum>
    {
        public CharacterCameraService(NestedBaseService parent) : base(parent)
        {

        }


        public Transform CameraTransform
        {
            get
            {
                switch (m_ServiceType)
                {
                    case CharacterCameraTypeEnum.FirstPerson: return m_FirstPersonCameraService.CameraTransform;
                }

                return null;
            }
        }



        [ShowInInspector]
        public CinemachineVirtualCamera Camera
        {
            get
            {
                switch (m_ServiceType)
                {
                    case CharacterCameraTypeEnum.FirstPerson: return m_FirstPersonCameraService?.Camera;
                }

                return null;
            }
        }

        [SerializeField]
        FirstPersonCameraService m_FirstPersonCameraService;



        public override int Priority
        {
            get
            {
                return 1;
            }
        }


        public override void RemoveService(CharacterCameraTypeEnum val)
        {
            CharacterBaseService service = null;
            switch (val)
            {
                case CharacterCameraTypeEnum.FirstPerson:
                    service = GetService<FirstPersonCameraService>();
                    break;
            }

            if (service != null)
            {
                RemoveService(service);
            }
        }

        public override void AddService(CharacterCameraTypeEnum val)
        {
            switch (val)
            {
                case CharacterCameraTypeEnum.FirstPerson:
                    if (m_FirstPersonCameraService == null)
                    {
                        m_FirstPersonCameraService = new FirstPersonCameraService(this);
                        m_FirstPersonCameraService.Awake();
                        m_FirstPersonCameraService.Start();
                    }

                    Debug.Log($"m_FirstPersonCameraService:{m_FirstPersonCameraService}");

                    AddService(m_FirstPersonCameraService);
                    break;
            }
        }

        public void SetDirection(Vector3 direction)
        {
            switch (m_ServiceType)
            {
                case CharacterCameraTypeEnum.FirstPerson:
                    if (m_FirstPersonCameraService == null)
                    {
                        m_FirstPersonCameraService = new FirstPersonCameraService(this);
                        m_FirstPersonCameraService.Awake();
                        m_FirstPersonCameraService.Start();
                    }

                    m_FirstPersonCameraService.SetDirection(direction);
                    break;
            }

        }

        public float CameraIncluded(Vector3 position)
        {
            switch (m_ServiceType)
            {
                case CharacterCameraTypeEnum.FirstPerson:
                    if (m_FirstPersonCameraService == null)
                    {
                        m_FirstPersonCameraService = new FirstPersonCameraService(this);
                        m_FirstPersonCameraService.Awake();
                        m_FirstPersonCameraService.Start();
                    }

                    return m_FirstPersonCameraService.CameraIncluded(position);
            }
            return 0;
        }

        public void FirstPersonTryInit()
        {
            if (m_FirstPersonCameraService == null)
            {
                m_FirstPersonCameraService = new FirstPersonCameraService(this);
                m_FirstPersonCameraService.Awake();
                m_FirstPersonCameraService.Start();
            }
        }



        public Vector3 CameraOffset
        {
            get
            {

                switch (m_ServiceType)
                {
                    case CharacterCameraTypeEnum.FirstPerson:
                        FirstPersonTryInit();
                        return m_FirstPersonCameraService.CameraOffset;
                }
                return Vector3.zero;
            }
            set
            {
                switch (m_ServiceType)
                {
                    case CharacterCameraTypeEnum.FirstPerson:
                        FirstPersonTryInit();
                        m_FirstPersonCameraService.CameraOffset = value;
                        break;
                }
            }

        }


        public float CameraYOffset
        {
            get
            {
                switch (m_ServiceType)
                {
                    case CharacterCameraTypeEnum.FirstPerson:
                        FirstPersonTryInit();
                        return m_FirstPersonCameraService.CameraYOffset;
                }

                return 0;
            }
            set
            {
                switch (m_ServiceType)
                {
                    case CharacterCameraTypeEnum.FirstPerson:
                        FirstPersonTryInit();
                        m_FirstPersonCameraService.CameraYOffset = value;
                        break;
                }

            }
        }

        protected override void DoAwake()
        {
            base.DoAwake();
            switch (m_ServiceType)
            {
                case CharacterCameraTypeEnum.FirstPerson:
                    FirstPersonTryInit();
                    InitPerlineNoise();
                    break;
            }
        }

        public float CurrentGain = 0;

        public const float NoiseGainChangeTime = 0.5f;

        public const float NoiseGainChangeSpeed = 1 / NoiseGainChangeTime;

        public const float NoiseGainLerpPercent = 0.3f;


        protected override void DoUpdate()
        {
            base.DoUpdate();
            if (PerlinNoise != null)
            {
                if (Character.IsMoveInputDown)
                {
                    if (CurrentGain < 1)
                    {
                        // CurrentGain += DeltaTime * NoiseGainChangeSpeed;
                        // CurrentGain = Mathf.Clamp01(CurrentGain);
                        CurrentGain = Mathf.Lerp(CurrentGain, 1, NoiseGainLerpPercent);
                        if (CurrentGain >= 0.99)
                        {
                            CurrentGain = 1;
                        }
                    }
                    // PerlinNoise.m_FrequencyGain = 1;
                    PerlinNoise.m_AmplitudeGain = CurrentGain;
                }
                else
                {
                    if (CurrentGain > 0)
                    {
                        // CurrentGain -= DeltaTime * NoiseGainChangeSpeed;
                        // CurrentGain = Mathf.Clamp01(CurrentGain);

                        CurrentGain = Mathf.Lerp(CurrentGain, 0, NoiseGainLerpPercent);
                        if (CurrentGain <= 0.01)
                        {
                            CurrentGain = 0;
                        }
                    }
                    // PerlinNoise.m_FrequencyGain = 0;
                    PerlinNoise.m_AmplitudeGain = CurrentGain;
                }
            }
        }

        [ShowInInspector]
        CinemachineBasicMultiChannelPerlin PerlinNoise;

        public void InitPerlineNoise()
        {
            if (PerlinNoise == null)
            {
                PerlinNoise = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                if (PerlinNoise == null)
                {
                    PerlinNoise = Camera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                }
            }

        }

        public void SetCameraNoise(bool isOpen, NoiseSettings noiseSettings = default, float amplitudeGain = 0, float frequencyGain = 0)
        {



            switch (m_ServiceType)
            {
                case CharacterCameraTypeEnum.FirstPerson:
                    FirstPersonTryInit();
                    break;
            }


            if (isOpen)
            {
                if (PerlinNoise == null)
                {
                    return;
                }

                if (noiseSettings == default)
                {
                    Debug.LogWarning("不是有效的NoiseSettings,请确认配置是否正确");

                }
                PerlinNoise.m_NoiseProfile = noiseSettings;
                PerlinNoise.m_AmplitudeGain = amplitudeGain;
                PerlinNoise.m_FrequencyGain = frequencyGain;
            }


        }


    }
}

