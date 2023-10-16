using System;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.Characters
{
    public interface IInteractionMode
    {
        float CalculatePriority(Character character, IInteractive interactive);

        void DrawGizmos(Character character);
    }
}