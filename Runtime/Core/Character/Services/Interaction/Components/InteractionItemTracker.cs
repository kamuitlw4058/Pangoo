using System;
using System.Collections.Generic;
using System.Linq;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.Characters
{
    [DisallowMultipleComponent]
    [Serializable]
    public class InteractionItemTracker : MonoBehaviour, IInteractive
    {
        private const HideFlags FLAGS = HideFlags.HideAndDontSave | HideFlags.HideInInspector;

        // MEMBERS: -------------------------------------------------------------------------------
        [NonSerialized] private Vector3 m_LastPosition;

        [NonSerialized] private int m_InstanceID;
        [NonSerialized] private bool m_IsInteracting;

        [ShowInInspector]
        public bool InteractEnable { get; set; } = true;

        [ShowInInspector]
        public bool IsInteracting
        {
            get
            {
                return m_IsInteracting;
            }
            set
            {
                m_IsInteracting = value;
            }
        }

        [NonSerialized] private Character m_Character;

        // EVENTS: --------------------------------------------------------------------------------

        public event Action<Character, IInteractive> EventInteract;
        public event Action<Character, IInteractive> EventStop;

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

        public Vector3 Position => Instance.transform.TransformPoint(InteractOffset);



        // INTERACTIVE INTERFACE: -----------------------------------------------------------------


        int IInteractive.InstanceID => this.m_InstanceID;

        bool IInteractive.IsInteracting => this.m_IsInteracting;

        [ShowInInspector]
        public bool InteractDisabled
        {
            get
            {
                if ((InteractCanBan && InteractTriggerEnter) || !InteractEnable)
                {
                    return true;
                }
                return false;
            }
        }

        [ShowInInspector]
        public float InteractRadius { get; set; }

        [ShowInInspector]
        public Vector3 InteractOffset { get; set; }

        [ShowInInspector]
        public float InteractRadian { get; set; }

        GameObject m_Instance;

        [ShowInInspector]
        public GameObject Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    return this.gameObject;
                }
                return m_Instance;
            }
            set
            {
                m_Instance = value;
            }
        }
        [ShowInInspector]
        public bool InteractCanBan { get; set; }

        [ShowInInspector]
        public bool InteractTriggerEnter { get; set; }

        [ShowInInspector]
        public GameObject Blocked { get; set; }


        [ShowInInspector]
        public bool InteractBlocked { get; set; }

        GameObject[] m_ColliderGameObjects;

        public void BuildColliderGameObjects()
        {
            if (m_ColliderGameObjects == null)
            {
                List<GameObject> gameObjects = new List<GameObject>();
                var colliders = GetComponents<Collider>();
                gameObjects.AddRange(colliders.Where(o => !o.isTrigger).Select(o => o.gameObject));

                var childernColliders = GetComponentsInChildren<Collider>(includeInactive: true);
                gameObjects.AddRange(childernColliders.Where(o => !o.isTrigger).Select(o => o.gameObject));

                m_ColliderGameObjects = gameObjects.ToArray();
            }
        }

        [ShowInInspector]
        public GameObject[] ColliderGameObjects
        {
            get
            {
                BuildColliderGameObjects();

                return m_ColliderGameObjects;
            }
        }


        public void Interact(Character character)
        {
            Debug.Log($"Enter Interacting.Name:{name} this.m_IsInteracting:{this.m_IsInteracting}");

            if (this.m_IsInteracting) return;
            Debug.Log($"Interacting.Name:{name} this.EventInteract:{this.EventInteract}");

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


        void OnDrawGizmos()
        {
            if (this.m_IsInteracting)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawSphere(Position, 0.05f);
        }
    }
}