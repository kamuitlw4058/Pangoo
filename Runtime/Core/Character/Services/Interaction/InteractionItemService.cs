using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;
using Pangoo.Core.Service;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class InteractionItemService : MonoMasterService, IInteractive
    {
        [NonSerialized] private int m_InstanceID;
        [NonSerialized] private bool m_IsInteracting;
        public InteractionItemService(GameObject gameObject) : base(gameObject)
        {
            m_InstanceID = gameObject.GetInstanceID();
        }



        public GameObject Instance => gameObject;

        public int InstanceID => m_InstanceID;

        public bool IsInteracting => m_IsInteracting;

        public Vector3 Position => this.CachedTransfrom.position;

        public int UniqueCode => m_InstanceID;


        public event Action<Character, IInteractive> EventInteract;
        public event Action<Character, IInteractive> EventStop;


        [NonSerialized] private Character m_Character;

        public void Interact(Character character)
        {
            if (this.m_IsInteracting) return;

            this.m_IsInteracting = true;
            this.m_Character = character;

            this.EventInteract?.Invoke(character, this);
        }

        public void Stop()
        {
            if (!this.m_IsInteracting) return;

            this.m_IsInteracting = false;
            this.EventStop?.Invoke(this.m_Character, this);
        }
    }
}