using System;
using UnityEngine;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Characters
{

    public class PlayerService : CharacterBaseService
    {

        CharacterPlayerTypeEnum m_CharacterPlayerType = CharacterPlayerTypeEnum.Directional;

        public override int Priority
        {
            get
            {
                return 2;
            }
        }

        [SerializeField] protected bool m_IsControllable;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public PlayerService(NestedBaseService parent) : base(parent)
        {
            this.m_IsControllable = true;
        }

        PlayerDirectionalService m_PlayerDirectionalService;


        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsControllable
        {
            get => this.m_IsControllable;
            set => this.m_IsControllable = value;
        }

        [ShowInInspector]
        public Vector3 InputDirection { get; set; } = Vector3.zero;



        public CharacterPlayerTypeEnum CharacterPlayerType
        {
            get
            {
                return m_CharacterPlayerType;
            }
            set
            {
                ChangePlayerType(m_CharacterPlayerType, value);
                m_CharacterPlayerType = value;
            }
        }


        public void ChangePlayerType(CharacterPlayerTypeEnum oldType, CharacterPlayerTypeEnum newType, bool overwrite = false)
        {
            if (oldType == newType && !overwrite)
            {
                return;
            }
            switch (oldType)
            {
                case CharacterPlayerTypeEnum.Directional: RemoveDirectional(); break;
            }

            switch (newType)
            {
                case CharacterPlayerTypeEnum.Directional: AddDirectional(); break;
            }
        }

        public void RemoveDirectional()
        {
            var service = GetService<PlayerDirectionalService>();
            if (service != null)
            {
                RemoveService(service);
            }
        }

        public void AddDirectional()
        {
            if (m_PlayerDirectionalService == null)
            {
                m_PlayerDirectionalService = new PlayerDirectionalService(this);
                m_PlayerDirectionalService.Parent = this;
                m_PlayerDirectionalService.Awake();
            }
            AddService(m_PlayerDirectionalService);
        }

        protected override void DoStart()
        {
            ChangePlayerType(m_CharacterPlayerType, m_CharacterPlayerType, true);

        }



    }

}

