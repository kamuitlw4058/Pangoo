using System;
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
            if (character == null) return float.MaxValue;



            var distance = Vector3.Distance(
                character.CachedTransfrom.TransformPoint(this.m_Offset),
                interactive.Position
            );
            // Debug.Log($"distance:{distance} interactive.InteractRadius:{interactive.InteractRadius}");
            if (interactive.InteractRadius > 0 && distance > interactive.InteractRadius) return float.MaxValue;


            var angle = character.CameraIncluded(interactive.Position);
            var InteractRadian = interactive.InteractRadian == 0 ? character.Main.GameConfig.GetGameMainConfig().DefaultInteractRadian : interactive.InteractRadian;

            // Debug.Log($"distance:{distance}, angle:{angle} interactive.InteractRadian:{interactive.InteractRadian},InteractRadian:{InteractRadian},{angle < InteractRadian}");

            if (angle < InteractRadian) return float.MaxValue;

            return angle;
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