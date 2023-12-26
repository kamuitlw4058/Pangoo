using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Services;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Characters
{

    [Serializable]
    public class InteractionService : CharacterControllerService<CharacterInteractionTypeEnum>
    {
        private static readonly Color COLOR_GIZMO_TARGET = new Color(0f, 1f, 0f, 1f);
        private const float INFINITY = 9999f;

        [ShowInInspector]
        public IInteractive Target { get; private set; }

        [ShowInInspector]
        public int Count
        {
            get
            {
                return SpatialHashInteractionItems.Count();
            }
        }

        IInteractionMode m_InteractionMode;



        [ShowInInspector]
        [HideInEditorMode]
        private List<ISpatialHash> m_Interactions = new List<ISpatialHash>();

        public InteractionService(NestedBaseService parent) : base(parent)
        {
            m_InteractionMode = new InteractionModeNearAndScreen();
            m_ServiceType = CharacterInteractionTypeEnum.NearAndScreen;

        }


        public bool Interact()
        {
            if (this.Target == null) return false;

            // this.EventInteract?.Invoke(this.m_Character, this.Target);
            this.Target.Interact(Character);

            return true;
        }

        public List<Tuple<IInteractive, float>> InteractiveResults = new List<Tuple<IInteractive, float>>();


        protected override void DoUpdate()
        {
            SpatialHashInteractionItems.Find(
                Character.CachedTransfrom.position,
                Character.MotionInfo.InteractionRadius,
                this.m_Interactions
            );

            IInteractive newTarget = null;
            float targetPriority = float.MaxValue;
            InteractiveResults.Clear();

            foreach (ISpatialHash interaction in this.m_Interactions)
            {
                if (interaction is not IInteractive interactive || !interactive.InteractEnable || interactive.InteractDisabled) continue;
                float priority = m_InteractionMode.CalculatePriority(
                    Character, interactive
                );
                InteractiveResults.Add(new Tuple<IInteractive, float>(interaction as IInteractive, priority));

                if (priority > INFINITY) continue;

                if (newTarget == null)
                {
                    newTarget = interactive;
                    targetPriority = priority;
                    continue;
                }

                if (targetPriority > priority)
                {
                    newTarget = interactive;
                    targetPriority = priority;
                }
            }

            if (this.Target == newTarget) return;
            // this.EventBlur?.Invoke(this.Character, this.Target);

            this.Target = newTarget;
            // this.EventFocus?.Invoke(this.m_Character, newTarget);
        }


        protected override void DoDrawGizmos()
        {
            if (Character == null) return;
            if (!Character.IsPlayer) return;
            if (!Application.isPlaying) return;

            if (this.Target != null)
            {
                Gizmos.color = COLOR_GIZMO_TARGET;
                Gizmos.DrawLine(this.Target.Position, Character.CachedTransfrom.position);
            }
            m_InteractionMode.DrawGizmos(Character);
        }

        public override void AddService(CharacterInteractionTypeEnum newType)
        {
            switch (newType)
            {
                case CharacterInteractionTypeEnum.NearCharacter:
                    m_InteractionMode = new InteractionModeNearCharacter();
                    break;
                case CharacterInteractionTypeEnum.NearAndScreen:
                    m_InteractionMode = new InteractionModeNearAndScreen();
                    break;
            }


        }



    }

}

