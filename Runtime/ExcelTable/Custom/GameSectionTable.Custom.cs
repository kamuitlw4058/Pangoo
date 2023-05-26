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
    public partial class GameSectionTable 
    {
        [NonSerialized]
        Dictionary<int,GameSectionRow> m_Dict;


        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            if(m_Dict == null){
                m_Dict =new Dictionary<int, GameSectionRow>();
            }

            m_Dict.Clear();
            foreach(var row in Rows){
                m_Dict.Add(row.Id,row);
            }

        }

        public GameSectionRow GetGameSectionRow(int id){
            return m_Dict[id];
        }
        

        public override void Merge(ExcelTableBase val){
        var table = val as GameSectionTable;
        Rows.AddRange(table.Rows);
        }
    }
}

