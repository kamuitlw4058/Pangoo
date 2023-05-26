using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo
{
    public partial class AssetPathTable 
    {
        public Dictionary<int,AssetPathRow> m_Dict;

        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            if(m_Dict == null){
                m_Dict = new Dictionary<int, AssetPathRow>();
            }
            m_Dict.Clear();
            foreach(var row in Rows){
                m_Dict.Add(row.Id,row);
            }
        }

        public AssetPathRow GetAssetPathRow(int id){
            return m_Dict[id];
        }

public override void Merge(ExcelTableBase val){
 var table = val as AssetPathTable;
 Rows.AddRange(table.Rows);
}
    }
}

