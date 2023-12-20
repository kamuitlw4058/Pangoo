using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{

    public static class AssetPathRowExtension
    {
        public static string ToDirPath(this AssetPathTable.AssetPathRow row)
        {
            return AssetUtility.GetAssetPathDir(row.AssetPackageDir, row.AssetType, row.AssetGroup);
        }


        public static string ToPrefabPath(this AssetPathTable.AssetPathRow row)
        {
            return AssetUtility.GetAssetPath(row.AssetPackageDir, row.AssetType, row.AssetPath, row.AssetGroup);
        }

        public static IAssetPathRow ToInterface(this AssetPathTable.AssetPathRow row)
        {
            var json = LitJson.JsonMapper.ToJson(row);
            return LitJson.JsonMapper.ToObject<Pangoo.MetaTable.AssetPathRow>(json);
        }

    }
}
