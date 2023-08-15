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
    public partial class StaticSceneTable 
    {
        [NonSerialized]
        [XmlIgnore]
        Dictionary<int,StaticSceneInfo> m_Dict= null;

        [NonSerialized]
        [XmlIgnore]
        AssetPathTable m_AssetPathTable = null;

        [NonSerialized]
        [XmlIgnore]
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
                var group = m_EntityGroupTable.GetEntityGroupRow(row.EntityGroupId);
                m_Dict.Add(row.Id,StaticSceneInfo.Create(row,group,assetPath));
                Debug.Log($"当前字典的ID:{row.Id}");
            }
        }

        public StaticSceneInfo GetStaticSceneInfo(int id){
            // Debug.Log($"this:{this.GetHashCode()}");
            return m_Dict[id];
        }


    }
}

