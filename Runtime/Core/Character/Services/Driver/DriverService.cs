using System;
using UnityEngine;
using Pangoo.Service;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class DriverService : CharacterControllerService<CharacterDriverTypeEnum>
    {

        public DriverService(INestedService parent) : base(parent)
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

