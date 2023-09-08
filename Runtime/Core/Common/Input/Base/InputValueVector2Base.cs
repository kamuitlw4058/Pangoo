using System;
using UnityEngine;

namespace Pangoo.Core.Common
{

    [Title("Input Vector2")]
    [Serializable]
    public abstract class InputValueVector2Base : InputValueBase<Vector2>
    {
        public abstract override Vector2 Read();
    }
}