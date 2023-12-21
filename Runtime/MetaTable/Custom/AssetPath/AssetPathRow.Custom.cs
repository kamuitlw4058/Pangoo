
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
    public partial class AssetPathRow
    {
        public string ToPrefabPath()
        {
            return AssetUtility.GetAssetPath(AssetPackageDir, AssetType, AssetPath, AssetGroup);
        }

        public string ToDirPath()
        {
            return AssetUtility.GetAssetPathDir(AssetPackageDir, AssetType, AssetGroup);
        }

    }
}

