using System;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public abstract class Variable : TPolymorphicItem<Variable>, IParams
    {
        public virtual IParams Params { get; }

        public virtual string Save()
        {
            return Params.Save();
        }

        public virtual void Load(string val)
        {
            Params.Load(val);
        }

    }
}