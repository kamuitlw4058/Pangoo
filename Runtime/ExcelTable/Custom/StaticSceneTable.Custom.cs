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
    public partial class StaticSceneTable 
    {

        Dictionary<int,StaticSceneInfo> m_Dict= null;
        AssetPathTable m_AssetPathTable = null;
        EntityGroupTable m_EntityGroupTable = null;

        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            if(m_Dict == null){
                m_Dict = new Dictionary<int, StaticSceneInfo>();
            }

            if(m_AssetPathTable == null){
                m_AssetPathTable = PangooEntry.ExcelTable.GetExcelTable<AssetPathTable>();
            }

            if(m_EntityGroupTable == null){
                m_EntityGroupTable = PangooEntry.ExcelTable.GetExcelTable<EntityGroupTable>();
            }

            m_Dict.Clear();
            foreach(var row in Rows){
                var assetPath =  m_AssetPathTable.GetAssetPathRow(row.AssetPathId);
                // var group = m_EntityGroupTable.GetEntityGroupRow(row.)
                // m_Dict.Add(row.Id,new StaticSceneInfo(row,assetPath));
            }
        }

        public StaticSceneInfo GetStaticSceneInfo(int id){
            StaticSceneInfo row;
            if(m_Dict.TryGetValue(id,out row)){
                return row;
            }
            return null;
        }

public override void Merge(ExcelTableBase val){
 var table = val as StaticSceneTable;
 Rows.AddRange(table.Rows);
}
    }
}

