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
        public Dictionary<int,AssetPathRow> m_Dict;

        public void InitDict(){
            if(m_Dict == null){
                m_Dict = new Dictionary<int, AssetPathRow>();
                foreach(var row in Rows){
                    m_Dict.Add(row.Id,row);
                }
            }  
        }

        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            m_Dict = null;
            InitDict();
            m_Dict.Clear();
            foreach(var row in Rows){
                m_Dict.Add(row.Id,row);
            }
        }

        public AssetPathRow GetAssetPathRow(int id){
            Debug.Log($"Rows:{Rows.Count} {this.GetHashCode()}");
            return m_Dict[id];

        }

public override void Merge(ExcelTableBase val){
 var table = val as AssetPathTable;
 Rows.AddRange(table.Rows);
}
    }
}

