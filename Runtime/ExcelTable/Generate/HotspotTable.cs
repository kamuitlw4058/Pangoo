// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.Common;

namespace Pangoo
{
    [Serializable]
    public partial class HotspotTable : ExcelTableBase
    {
        [Serializable]
        public partial class HotspotRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("热点区域类型")]
            [HideLabel]
            [JsonMember("HotspotType")]
            [ExcelTableCol("HotspotType","HotspotType","string", "热点区域类型",3)]
            public string HotspotType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("参数")]
            [HideLabel]
            [JsonMember("Params")]
            [ExcelTableCol("Params","Params","string", "参数",4)]
            public string Params ;
        }


        [TableList]
        public List<HotspotRow> Rows = new();

        public override IReadOnlyList<ExcelRowBase> BaseRows{
          get{
              return Rows;
          }
        }

        public override IReadOnlyList<ExcelNamedRowBase> NamedBaseRows{
          get{
              return Rows;
          }
        }

        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,HotspotRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as HotspotTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<HotspotRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as HotspotRow);
        }

        #endif

        public HotspotRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<HotspotRow>(row_id);
          #else
          HotspotRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
          #endif
         }

#if UNITY_EDITOR
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<HotspotRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

