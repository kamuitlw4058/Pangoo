
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;

namespace Pangoo
{
    [Serializable]
    public sealed class UIInfo : IReference
    {

        public AssetPathTable.AssetPathRow AssetPathRow;


        public int AssetPathId
        {
            get
            {
                return AssetPathRow.Id;
            }
        }

        public string AssetName
        {
            get
            {
                return AssetPathRow.Name;
            }
        }



        public string AssetPath
        {
            get
            {
                return AssetPathRow.ToPrefabPath();
            }
        }




        public static UIInfo Create(AssetPathTable.AssetPathRow assetPath)
        {
            var info = ReferencePool.Acquire<UIInfo>();
            info.AssetPathRow = assetPath;
            return info;
        }

        public void Clear()
        {
            AssetPathRow = null;
        }

    }
}
