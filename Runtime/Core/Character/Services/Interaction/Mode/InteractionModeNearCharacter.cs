using System;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.Character
{
    [Title("Near Character")]
    [Category("Near Character")]

    // [Image(typeof(IconCharacter), ColorTheme.Type.Green)]
    // [Description("Selects the closest interactive element to the Character")]

    [Serializable]
    public class InteractionModeNearCharacter : InteractionModeBase
    {

        [SerializeField] private Vector3 m_Offset = new Vector3(0f, 0f, 1f);

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override float CalculatePriority(Character character, IInteractive interactive)
        {
            if (character == null) return float.MaxValue;

            return Vector3.Distance(
                character.CachedTransfrom.TransformPoint(this.m_Offset),
                interactive.Position
            );
        }

        // GIZMOS: --------------------------------------------------------------------------------

        public override void DrawGizmos(Character character)
        {
            base.DrawGizmos(character);

            Vector3 position = character.CachedTransfrom.TransformPoint(this.m_Offset);

            Gizmos.color = COLOR_GIZMOS;
            Gizmos.DrawCube(position, GIZMO_SIZE);
        }


    }
}