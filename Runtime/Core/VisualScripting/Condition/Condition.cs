using System;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public abstract class Condition : TPolymorphicItem<Condition>, IParams
    {
        public virtual IParams Params { get; }

        [SerializeField]
        [HideInInspector]
        protected bool m_Sign = true;

        // PROPERTIES: ----------------------------------------------------------------------------

        public sealed override string Title => string.Format(
            "{0} {1}",
            this.m_Sign ? "If" : "Not",
            this.Summary
        );

        protected virtual string Summary => TextUtils.Humanize(this.GetType().ToString());

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public int GetState(Args args)
        {
            return this.Run(args);
        }

        public bool Check(Args args)
        {
            // if (!this.IsEnabled) return this.m_Sign;
            // if (this.Breakpoint) Debug.Break();

            return this.m_Sign ? (this.Run(args) != 0) : !(this.Run(args) != 0);
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected abstract int Run(Args args);



        public void Load(string val)
        {
            Params.Load(val);
        }

        public string Save()
        {
            return Params.Save();
        }

    }
}