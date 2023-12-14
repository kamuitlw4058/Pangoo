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

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class AssetGroupDetailRowWrapper : MetaTableDetailRowWrapper<AssetGroupOverview, UnityAssetGroupRow>
    {
        [ShowInInspector]
        [DelayedProperty]
        public string AssetGroup
        {
            get
            {
                if (UnityRow.Row.AssetGroup.IsNullOrWhiteSpace())
                {
                    UnityRow.Row.AssetGroup = UnityRow.Row.Name.ToPinyin();
                    Save();
                }

                return UnityRow.Row.AssetGroup;
            }
            set
            {
                UnityRow.Row.AssetGroup = value;
                Save();
            }
        }

    }
}
#endif

