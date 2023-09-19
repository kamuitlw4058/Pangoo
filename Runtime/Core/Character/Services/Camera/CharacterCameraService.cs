using System;
using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;
using Cinemachine;


namespace Pangoo.Core.Character
{

    [Serializable]
    public class CharacterCameraService : CharacterControllerService<CharacterCameraTypeEnum>
    {
        public CharacterCameraService(INestedService parent) : base(parent)
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
                    case CharacterCameraTypeEnum.FirstPerson: return m_FirstPersonCameraService.Camera;
                }

                return null;
            }
        }

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
                    }

                    AddService(m_FirstPersonCameraService);
                    break;
            }
        }
    }
}

