using System;
using UnityEngine;
using Pangoo.Core.Services;

namespace Pangoo.Core.Characters
{

    [Serializable]
    public class FootstepsService : CharacterControllerService<CharacterFootstepTypeEnum>
    {
        bool m_Enabled;
        public bool Enabled
        {
            get
            {
                switch (m_ServiceType)
                {
                    case CharacterFootstepTypeEnum.CharacterFootstepDefault:
                        var service = GetService<CharacterFootstepDefault>();
                        return service.Enabled;
                }

                return false;
            }
            set
            {
                switch (m_ServiceType)
                {
                    case CharacterFootstepTypeEnum.CharacterFootstepDefault:
                        var service = GetService<CharacterFootstepDefault>();
                        service.Enabled = value;
                        break;
                }

            }
        }


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
                    service = GetService<CharacterFootstepDefault>();
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

