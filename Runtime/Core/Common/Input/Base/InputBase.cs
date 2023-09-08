using System;

namespace Pangoo.Core.Common
{

    [Serializable]
    public abstract class InputBase
    {
        public virtual bool Active { get; set; } = false;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public virtual void OnAwake()
        { }

        public virtual void OnDestroy()
        { }

        public virtual void OnUpdate()
        { }
    }
}