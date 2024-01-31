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

        public int HitNum
        {
            get
            {
                switch (m_ServiceType)
                {
                    case CharacterFootstepTypeEnum.CharacterFootstepDefault:
                        return m_Footstep.m_HitNum;
                }

                return 0;
            }
        }

        public RaycastHit Hit
        {
            get
            {
                switch (m_ServiceType)
                {
                    case CharacterFootstepTypeEnum.CharacterFootstepDefault:
                        return m_Footstep.m_Hit;
                }

                return new RaycastHit();
            }
        }

        public RaycastHit[] Hits
        {
            get
            {
                switch (m_ServiceType)
                {
                    case CharacterFootstepTypeEnum.CharacterFootstepDefault:
                        return m_Footstep.m_HitsBuffer;
                }

                return new RaycastHit[5];
            }
        }

        public Renderer Renderer
        {
            get
            {
                switch (m_ServiceType)
                {
                    case CharacterFootstepTypeEnum.CharacterFootstepDefault:
                        return m_Footstep.m_Renderer;
                }

                return null;
            }
        }

        public FootstepAsset FootstepConfig
        {
            get
            {
                return Character.Main.GameConfig.GetFootstepAsset();
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

        protected override void DoDrawGizmos()
        {
            if (Character == null) return;
            if (!Character.IsPlayer) return;
            if (!Application.isPlaying) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Character.CachedTransfrom.position, Character.CachedTransfrom.position - (Character.CachedTransfrom.up * 2));

        }

    }

}

