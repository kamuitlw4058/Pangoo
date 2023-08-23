
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;

namespace Pangoo
{
   public partial class AssetPathInfo: BaseInfo
    {
        public class AssetPathInfoRow{
            AssetPathTable.AssetPathRow m_AssetPathRow;
            // AssetPackageTable.AssetPackageRow m_AssetPackageRow;

            public AssetPathInfoRow(AssetPathTable.AssetPathRow assetPathRow)
            // public AssetPathInfoRow(AssetPathTable.AssetPathRow assetPathRow, AssetPackageTable.AssetPackageRow assetPackageRow)
            {
                this.m_AssetPathRow = assetPathRow;
                // this.m_AssetPackageRow = assetPackageRow;
            }
        }
    }
}
