using System;
using System.Linq;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.Characters
{
    [Title("Near Character")]
    [Category("Near Character")]

    // [Image(typeof(IconCharacter), ColorTheme.Type.Green)]
    // [Description("Selects the closest interactive element to the Character")]

    [Serializable]
    public class InteractionModeNearAndScreen : InteractionModeBase
    {

        [SerializeField] private Vector3 m_Offset = new Vector3(0f, 0f, 0f);

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override float CalculatePriority(Character character, IInteractive interactive)
        {
            interactive.InteractBlocked = false;

            if (character == null) return float.MaxValue;
            if (interactive.InteractDisabled) return float.MaxValue;


            var distance = Vector3.Distance(
                character.CachedTransfrom.TransformPoint(this.m_Offset),
                interactive.Position
            );

            var direction = interactive.Position - character.CachedTransfrom.TransformPoint(this.m_Offset);
            Ray ray = new Ray(character.CachedTransfrom.TransformPoint(Vector3.zero), direction);
            RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance: distance);
            if (hits != null && hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (!interactive.ColliderGameObjects.Contains(hits[i].collider.gameObject) && !hits[i].collider.isTrigger)
                    {
                        interactive.Blocked = hits[i].collider.gameObject;
                        interactive.InteractBlocked = true;
                        return float.MaxValue;
                    }

                }

            }
            // Debug.Log($"no hit:interactive:{interactive.Instance.name} :{direction} :{direction.magnitude} :{character.CachedTransfrom.TransformPoint(this.m_Offset)},{character.CachedTransfrom.position}");



            var InteractRadius = interactive.InteractRadius > 0 ? interactive.InteractRadius : character.Main.DefaultInteractRadius;
            if (InteractRadius > 0 && distance > InteractRadius) return float.MaxValue;




            // Debug.Log($"distance:{distance}, InteractRadius:{InteractRadius},interactive.InteractRadius:{interactive.InteractRadius}");

            var angle = character.CameraIncluded(interactive.Position);
            var InteractRadian = interactive.InteractRadian == 0 ? character.Main.GameConfig.GetGameMainConfig().DefaultInteractRadian : interactive.InteractRadian;

            // Debug.Log($"distance:{distance}, angle:{angle} interactive.InteractRadian:{interactive.InteractRadian},InteractRadian:{InteractRadian},{angle < InteractRadian}");

            if (angle < InteractRadian) return float.MaxValue;

            return (1 - angle);
        }

        // GIZMOS: --------------------------------------------------------------------------------

        public override void DrawGizmos(Character character)
        {
            base.DrawGizmos(character);

            Vector3 position = character.CachedTransfrom.TransformPoint(this.m_Offset) + character.CameraOffset;

            Gizmos.color = COLOR_GIZMOS;
            Gizmos.DrawCube(position, GIZMO_SIZE);
        }


    }
}