using System;
using Pangoo.Core.Common;

namespace Pangoo.Core.VisualScripting
{
    // [Image(typeof(IconCircleOutline), ColorTheme.Type.Yellow)]

    [Serializable]
    public abstract class HotSpot : TPolymorphicItem<HotSpot>
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public abstract override string Title { get; }

        // METHODS: -------------------------------------------------------------------------------

        public virtual void OnAwake()
        { }

        public virtual void OnStart()
        { }

        public virtual void OnEnable()
        { }

        public virtual void OnDisable()
        { }

        public virtual void OnUpdate()
        { }

        public virtual void OnGizmos()
        { }

        public virtual void OnDestroy()
        { }

        public virtual void OnPointerEnter()
        { }

        public virtual void OnPointerExit()
        { }
    }
}