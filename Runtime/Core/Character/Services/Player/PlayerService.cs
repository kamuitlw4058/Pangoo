using System;
using UnityEngine;
using Pangoo.Service;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class PlayerService : CharacterBaseService
    {


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

    }

}

