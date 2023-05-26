// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;

namespace Pangoo
{
    public partial class EntityGroupTable 
    {
        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,EntityGroupRow> m_Dict;
        public void InitDict(){
            if(m_Dict == null){
                m_Dict = new Dictionary<int, EntityGroupRow>();
                foreach(var row in Rows){
                    m_Dict.Add(row.Id,row);
                }
            }
        }

        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            InitDict();
            m_Dict.Clear();
            foreach(var row in Rows){
                m_Dict.Add(row.Id,row);
            }
        }

        public EntityGroupRow GetEntityGroupRow(int id){
            InitDict();
            return m_Dict[id];
        }

public override void Merge(ExcelTableBase val){
 var table = val as EntityGroupTable;
 Rows.AddRange(table.Rows);
}
    }
}

