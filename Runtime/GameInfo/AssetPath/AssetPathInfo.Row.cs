
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{

    public class AssetPathInfoRow : BaseInfoRow
    {
        IAssetPathRow m_AssetPathRow;

        public AssetPathInfoRow(IAssetPathRow assetPathRow)
        {
            this.m_AssetPathRow = assetPathRow;
        }


    }
}
