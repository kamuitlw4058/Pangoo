using System;
using Pangoo.Core.Common;
using Pangoo.Core.Services;

namespace Pangoo.Core.VisualScripting
{
    // [Image(typeof(IconCircleOutline), ColorTheme.Type.Yellow)]

    [Serializable]
    public abstract class HotSpot : MonoSubService<DynamicObject>
    {
        public HotspotTable.HotspotRow Row { get; set; }

        public DynamicObject dynamicObject { get; set; }

        public virtual void LoadParamsFromJson(string val) { }
        public virtual string ParamsToJson()
        {
            return "{}";
        }

    }
}