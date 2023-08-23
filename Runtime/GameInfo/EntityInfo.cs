
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;

namespace Pangoo
{
    [Serializable]
    public sealed class EntityInfo: IReference
    {

        public AssetPathTable.AssetPathRow AssetPathRow;
        public EntityGroupTable.EntityGroupRow EntityGroupRow;
        public AssetPackageTable.AssetPackageRow AssetPackageRow;



        public int AssetPathId
        {
            get
            {
                return AssetPathRow.Id;
            }
        }

        public string AssetName{
            get{
                return AssetPathRow.Name;
            }
        }



        public string AssetPath
        {
            get
            {
                return $"{AssetPackageRow.AssetPackagePath}/{AssetPathRow.AssetType}/{AssetPathRow.AssetPath}";
            }
        }

        public string GroupName{

            get{
                return EntityGroupRow.Name;
            }
        }

        public static EntityInfo Create(AssetPackageTable.AssetPackageRow assetPackage,AssetPathTable.AssetPathRow assetPath, EntityGroupTable.EntityGroupRow entityGroup){
            var info = ReferencePool.Acquire<EntityInfo>();
            info.AssetPackageRow = assetPackage;
            info.AssetPathRow = assetPath;
            info.EntityGroupRow = entityGroup;
            return info;
        }

        public void Clear(){
            AssetPathRow = null;
            EntityGroupRow = null;
        }

    }
}
