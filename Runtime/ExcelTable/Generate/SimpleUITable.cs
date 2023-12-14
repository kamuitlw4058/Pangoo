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
    public partial class SimpleUITable : ExcelTableBase
    {
        [Serializable]
        public partial class SimpleUIRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源路径")]
            [HideLabel]
            [JsonMember("AssetPathId")]
            [ExcelTableCol("AssetPathId","AssetPathId","int", "资源路径",3)]
            public int AssetPathId ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("排序层")]
            [HideLabel]
            [JsonMember("SortingLayer")]
            [ExcelTableCol("SortingLayer","SortingLayer","string", "排序层",4)]
            public string SortingLayer ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("排序索引")]
            [HideLabel]
            [JsonMember("SortingOrder")]
            [ExcelTableCol("SortingOrder","SortingOrder","int", "排序索引",5)]
            public int SortingOrder ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("UI参数类")]
            [HideLabel]
            [JsonMember("UIParamsType")]
            [ExcelTableCol("UIParamsType","UIParamsType","string", "UI参数类",6)]
            public string UIParamsType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("UI参数")]
            [HideLabel]
            [JsonMember("Params")]
            [ExcelTableCol("Params","Params","string", "UI参数",7)]
            public string Params ;
        }


        [TableList]
        public List<SimpleUIRow> Rows = new();

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
        public Dictionary<int,SimpleUIRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as SimpleUITable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<SimpleUIRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as SimpleUIRow);
        }

        #endif

        public SimpleUIRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<SimpleUIRow>(row_id);
          #else
          SimpleUIRow row;
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
          Rows = LoadExcelFile<SimpleUIRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

