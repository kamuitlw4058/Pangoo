// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo
{
    public partial class EntityGroupTable 
    {
        public Dictionary<int,EntityGroupRow> m_Dict;

        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            if(m_Dict == null){
                m_Dict = new Dictionary<int, EntityGroupRow>();
            }
            m_Dict.Clear();
            foreach(var row in Rows){
                m_Dict.Add(row.Id,row);
            }
        }

        public EntityGroupRow GetEntityGroupRow(int id){
            return m_Dict[id];
        }

public override void Merge(ExcelTableBase val){
 var table = val as EntityGroupTable;
 Rows.AddRange(table.Rows);
}
    }
}

