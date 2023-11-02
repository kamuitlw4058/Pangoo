using System;
using UnityEngine;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using Cinemachine;


namespace Pangoo.Core.Characters
{

    [Serializable]
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

        public void SetCameraOffset(Vector3 offset)
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

                    m_FirstPersonCameraService.SetCameraOffset(offset);
                    break;
            }

        }


    }
}

