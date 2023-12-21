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
    public partial class AssetGroupOverview
    {
        public static string GetAssetGroupUuidByAssetGroup(string AssetGroup)
        {
            if (AssetGroup.IsNullOrWhiteSpace())
            {
                return null;
            }

            var overviews = AssetDatabaseUtility.FindAsset<AssetGroupOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {

                    if (row.Row.AssetGroup.Equals(AssetGroup))
                    {
                        return row.Uuid;
                    }
                }
            }
            return null;
        }

        public static string GetAssetGroupByAssetGroupUuid(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace())
            {
                return null;
            }

            var row = GetUnityRowByUuid(uuid);
            if (row != null)
            {
                return row.Row.AssetGroup;
            }

            return null;
        }

    }
}
#endif

