
using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    public partial class DynamicObjectRow
    {
        public List<int> GetHotspotIdList()
        {
            if (HotspotIds.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return HotspotIds.ToSplitList<int>();
        }


        public List<int> GetTriggerEventIdList()
        {
            if (TriggerEventIds.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return TriggerEventIds.ToSplitList<int>();
        }

    }
}

