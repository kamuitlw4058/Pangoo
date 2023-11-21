using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;

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


    }
}
