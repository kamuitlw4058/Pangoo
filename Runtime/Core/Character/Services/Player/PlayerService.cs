using System;
using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class PlayerService : CharacterBaseService
    {

        public override int Priority
        {
            get
            {
                return 1;
            }
        }

        [SerializeField] protected bool m_IsControllable;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        protected PlayerService()
        {
            this.m_IsControllable = true;
        }

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsControllable
        {
            get => this.m_IsControllable;
            set => this.m_IsControllable = value;
        }

        [ShowInInspector]
        public Vector3 InputDirection { get; protected set; } = Vector3.zero;

    }

}

