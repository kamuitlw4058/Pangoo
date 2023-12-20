using System;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{
    // [Image(typeof(IconCircleOutline), ColorTheme.Type.Yellow)]

    [Serializable]
    public abstract class HotSpot : MonoSubService<DynamicObject>
    {
        public IHotspotRow Row { get; set; }

        public DynamicObject dynamicObject { get; set; }

        public virtual void LoadParamsFromJson(string val) { }
        public virtual string ParamsToJson()
        {
            return "{}";
        }

    }
}