using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Character
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




        [NonSerialized] private List<ISpatialHash> m_Interactions = new List<ISpatialHash>();


        public InteractionService(INestedService parent) : base(parent)
        {
            m_InteractionMode = new InteractionModeNearCharacter();
        }

        // public bool IsInsideFrustum(Vector3 itemPosition)
        // {
        //     Vector3 screenPoint = Character.CharacterCamera.Camera..WorldToScreenPoint(itemPosition);

        //     // 判断点是否在摄像机视锥内
        //     return screenPoint.x > 0 && screenPoint.x < Screen.width &&
        //                            screenPoint.y > 0 && screenPoint.y < Screen.height &&
        //                            screenPoint.z > 0;
        // }

        public bool Interact()
        {
            if (this.Target == null) return false;

            // this.EventInteract?.Invoke(this.m_Character, this.Target);
            this.Target.Interact(Character);

            return true;
        }


        public override void DoUpdate()
        {
            SpatialHashInteractionItems.Find(
                Character.CachedTransfrom.position,
                Character.MotionInfo.InteractionRadius,
                this.m_Interactions
            );

            IInteractive newTarget = null;
            float targetPriority = float.MaxValue;

            foreach (ISpatialHash interaction in this.m_Interactions)
            {
                if (interaction is not IInteractive interactive) continue;
                float priority = m_InteractionMode.CalculatePriority(
                    Character, interactive
                );

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


        public override void DoDrawGizmos()
        {
            if (Character == null) return;
            if (!Character.IsPlayer) return;
            if (!Application.isPlaying) return;

            if (this.Target != null)
            {
                Gizmos.color = COLOR_GIZMO_TARGET;
                Gizmos.DrawLine(this.Target.Position, Character.CachedTransfrom.position);
            }
        }

        public override void AddService(CharacterInteractionTypeEnum newType)
        {
            switch (newType)
            {
                case CharacterInteractionTypeEnum.NearCharacter:
                    m_InteractionMode = new InteractionModeNearCharacter();
                    break;
            }
        }



    }

}

