using System;

namespace Pangoo.Core.Common
{

    [Serializable]
    public abstract class InputValueBase<T> : InputBase where T : struct
    {
        public abstract T Read();
    }
}