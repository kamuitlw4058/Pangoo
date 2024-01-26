
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


        public List<string> GetHotspotUuidList()
        {
            if (HotspotUuids.IsNullOrWhiteSpace())
            {
                return new List<string>();
            }
            return HotspotUuids.ToSplitList<string>();
        }


        public List<string> GetTriggerEventUuidList()
        {
            List<string> ret = new List<string>();
            if (!TriggerEventUuids.IsNullOrWhiteSpace())
            {
                ret.AddRange(TriggerEventUuids.ToSplitList<string>());
            }




            return ret;
        }

    }
}

