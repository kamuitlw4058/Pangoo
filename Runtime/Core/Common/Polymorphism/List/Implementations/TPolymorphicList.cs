using System;
using UnityEngine;

namespace Pangoo.Core.Common
{
    [Serializable]
    public abstract class TPolymorphicList<TItem> : IPolymorphicList where TItem : IPolymorphicItem
    {
        public abstract int Length { get; }
    }
}
