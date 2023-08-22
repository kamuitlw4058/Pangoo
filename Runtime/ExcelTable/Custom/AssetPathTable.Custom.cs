using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


namespace Pangoo
{
    public partial class AssetPathTable 
    {
        [NonSerialized]
        [XmlIgnore]
        AssetPackageTable m_AssetPackageTable = null;

        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            if(m_AssetPackageTable == null){
                m_AssetPackageTable = PangooEntry.ExcelTable.GetExcelTable<AssetPackageTable>();  
            }

        }

        public AssetPathRow GetAssetPathRow(int id){
            return GetRowById(id);

        }

    }
}

