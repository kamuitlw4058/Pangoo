#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
using System.Linq;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class DynamicObjectRowWrapper : MetaTableRowWrapper<DynamicObjectOverview, DynamicObjectNewRowWrapper, UnityDynamicObjectRow>
    {
        [ShowInInspector]
        [TableTitleGroup("资源id")]
        [HideLabel]
        public int AssetPathId
        {
            get
            {
                return UnityRow.Row.AssetPathId;
            }
        }

        [ShowInInspector]
        [TableTitleGroup("资源Uuid")]
        [HideLabel]
        public string AssetPathUuid
        {
            get
            {
                return UnityRow.Row.AssetPathUuid;
            }
        }

        [ShowInInspector]
        [TableTitleGroup("热点区域")]
        [HideLabel]
        public int HotspotUuidsCount
        {
            get
            {
                return UnityRow.Row.HotspotUuids.ToSplitArr<int>().Count();
            }
        }

        [ShowInInspector]
        [TableTitleGroup("触发器")]
        [HideLabel]
        public int TriggerEventUuidsCount
        {
            get
            {
                return UnityRow.Row.TriggerEventUuids.ToSplitArr<int>().Count();
            }
        }

    }
}
#endif

