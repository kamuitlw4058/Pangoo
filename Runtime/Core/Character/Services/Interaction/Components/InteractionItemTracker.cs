using System;
using Pangoo.Core.Common;
using UnityEditor;
using UnityEngine;

namespace Pangoo.Core.Character
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]

    [Serializable]
    public class InteractionItemTracker : MonoBehaviour, IInteractive
    {
        private const HideFlags FLAGS = HideFlags.HideAndDontSave | HideFlags.HideInInspector;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Vector3 m_LastPosition;

        [NonSerialized] private int m_InstanceID;
        [NonSerialized] private bool m_IsInteracting;

        [NonSerialized] private CharacterService m_Character;

        // EVENTS: --------------------------------------------------------------------------------

        public event Action<CharacterService, IInteractive> EventInteract;
        public event Action<CharacterService, IInteractive> EventStop;

        // INITIALIZE METHODS: --------------------------------------------------------------------

        public static InteractionItemTracker Require(GameObject target)
        {
            return target.GetOrAddComponent<InteractionItemTracker>();
        }

        private void Awake()
        {
            // this.hideFlags = FLAGS;
            this.m_InstanceID = this.gameObject.GetInstanceID();
        }

        private void OnEnable()
        {
            this.m_LastPosition = this.transform.position;
            SpatialHashInteractionItems.Insert(this);
        }

        private void OnDisable()
        {
            SpatialHashInteractionItems.Remove(this);
        }

        // SPATIAL HASH INTERFACE: ----------------------------------------------------------------

        int ISpatialHash.UniqueCode => this.GetInstanceID();

        Vector3 ISpatialHash.Position => this.transform.position;

        // INTERACTIVE INTERFACE: -----------------------------------------------------------------

        GameObject IInteractive.Instance => this.gameObject;

        int IInteractive.InstanceID => this.m_InstanceID;

        bool IInteractive.IsInteracting => this.m_IsInteracting;

        void IInteractive.Interact(CharacterService character)
        {
            if (this.m_IsInteracting) return;

            this.m_IsInteracting = true;
            this.m_Character = character;

            this.EventInteract?.Invoke(character, this);
        }

        void IInteractive.Stop()
        {
            if (!this.m_IsInteracting) return;

            this.m_IsInteracting = false;
            this.EventStop?.Invoke(this.m_Character, this);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}