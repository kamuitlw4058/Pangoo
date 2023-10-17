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
            return Utility.Text.Format("{0}/StreamRes/Prefab/{1}", row.AssetPackageDir, row.AssetType);
        }

        public static string ToFullPath(this AssetPathTable.AssetPathRow row)
        {
            return Utility.Text.Format("{0}/StreamRes/Prefab/{1}/{2}", row.AssetPackageDir, row.AssetType, row.AssetPath);
        }

        public static string ToPrefabPath(this AssetPathTable.AssetPathRow row)
        {
            return Utility.Text.Format("{0}/StreamRes/Prefab/{1}/{2}", row.AssetPackageDir, row.AssetType, row.AssetPath);
        }


    }
}
