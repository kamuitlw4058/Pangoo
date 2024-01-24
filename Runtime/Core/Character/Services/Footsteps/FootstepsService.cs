using System;
using UnityEngine;
using Pangoo.Core.Services;

namespace Pangoo.Core.Characters
{

    [Serializable]
    public class FootstepsService : CharacterControllerService<CharacterFootstepTypeEnum>
    {

        public FootstepsService(NestedBaseService parent) : base(parent)
        {

        }
        CharacterFootstepDefault m_Footstep;



        public override void RemoveService(CharacterFootstepTypeEnum val)
        {
            CharacterBaseService service = null;
            switch (val)
            {
                case CharacterFootstepTypeEnum.CharacterFootstepDefault:
                    service = GetService<DriverCharacterController>();
                    break;
            }

            if (service != null)
            {
                RemoveService(service);
            }
        }

        public override void AddService(CharacterFootstepTypeEnum val)
        {
            switch (val)
            {
                case CharacterFootstepTypeEnum.CharacterFootstepDefault:
                    if (m_Footstep == null)
                    {
                        m_Footstep = new CharacterFootstepDefault(this);
                        m_Footstep.Awake();
                    }

                    AddService(m_Footstep);
                    break;
            }
        }
    }

}

