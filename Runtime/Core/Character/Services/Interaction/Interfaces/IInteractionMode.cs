using System;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.Character
{
    public interface IInteractionMode
    {
        float CalculatePriority(CharacterService character, IInteractive interactive);

        void DrawGizmos(CharacterService character);
    }
}