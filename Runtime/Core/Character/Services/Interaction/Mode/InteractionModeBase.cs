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
    public class InteractionModeBase : IInteractionMode
    {
        public static readonly Color COLOR_GIZMOS = new Color(0f, 1f, 0f, 0.5f);

        public static readonly Vector3 GIZMO_SIZE = Vector3.one * 0.05f;

        public virtual float CalculatePriority(Character character, IInteractive interactive)
        {
            return 0;
        }


        public virtual void DrawGizmos(Character character)
        {

        }


    }
}