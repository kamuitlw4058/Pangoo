
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using System.Collections.Generic;


namespace Pangoo
{
    [Serializable]
   public partial class AssetPathInfo: BaseInfo
    {
        Dictionary<int,AssetPathInfoRow> Dict = new Dictionary<int, AssetPathInfoRow>();

        AssetPathTable m_AssetPathTable;
        // AssetPackageTable m_AssetPackageTable;
        protected override void OnInit(){
            m_AssetPathTable = PangooEntry.ExcelTable.GetExcelTable<AssetPathTable>();

            foreach(var row in m_AssetPathTable.Rows){
                Dict.Add(row.Id, new AssetPathInfoRow(row));
            }
        }


        public AssetPathInfoRow GetAssetPathInfoRow(int id){
            AssetPathInfoRow ret;
            if(Dict.TryGetValue(id,out ret)){
                return ret;
            }

            return null;
        }

        
    }
}
