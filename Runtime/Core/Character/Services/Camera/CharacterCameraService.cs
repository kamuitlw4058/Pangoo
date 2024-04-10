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

        public void SetCameraNoise(bool isOpen, NoiseSettings noiseSettings = default, float amplitudeGain = 0, float frequencyGain = 0)
        {
            CinemachineBasicMultiChannelPerlin noise = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (isOpen)
            {
                if (noise == null)
                {
                    noise = Camera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                }

                if (noiseSettings == default)
                {
                    Debug.LogWarning("不是有效的NoiseSettings,请确认配置是否正确");
                }
                noise.m_NoiseProfile = noiseSettings;
                noise.m_AmplitudeGain = amplitudeGain;
                noise.m_FrequencyGain = frequencyGain;
            }
            else
            {
                if (noise != null)
                {
                    Camera.DestroyCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                }
                else
                {
                    Debug.Log("相机没有Noise的组件，不用关闭Noise");
                }
            }

        }


    }
}

