using System;
using UnityEngine;
using Pangoo.Core.Services;

namespace Pangoo.Core.Characters
{

    // [Serializable]
    public class DriverService : CharacterControllerService<CharacterDriverTypeEnum>
    {

        public DriverService(NestedBaseService parent) : base(parent)
        {

        }

        public Vector3 MoveDirection { get; set; }

        // public MovementTypeEnum CurrentMovementType { get; set; }

        public override int Priority
        {
            get
            {
                return 4;
            }
        }

        DriverCharacterController m_DriverCharacterController;

        public override void SetDriverInfo(DriverInfo driverInfo)
        {
            switch (driverInfo.CharacterDriverTypeEnum)
            {
                case CharacterDriverTypeEnum.CharacterController:
                    m_DriverCharacterController.UpdateControllerData(driverInfo);
                    break;
            }
        }

        public float ColliderHeight
        {
            get
            {
                return m_DriverCharacterController.ColliderHeight;
            }
            set
            {
                m_DriverCharacterController.ColliderHeight = value;
            }
        }


        public override void RemoveService(CharacterDriverTypeEnum val)
        {
            CharacterBaseService service = null;
            switch (val)
            {
                case CharacterDriverTypeEnum.CharacterController:
                    service = GetService<DriverCharacterController>();
                    break;
            }

            if (service != null)
            {
                RemoveService(service);
            }
        }

        public override void AddService(CharacterDriverTypeEnum val)
        {
            switch (val)
            {
                case CharacterDriverTypeEnum.CharacterController:
                    if (m_DriverCharacterController == null)
                    {
                        m_DriverCharacterController = new DriverCharacterController(this);
                        m_DriverCharacterController.Awake();
                    }

                    AddService(m_DriverCharacterController);
                    break;
            }
        }

    }

}

