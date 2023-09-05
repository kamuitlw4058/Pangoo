// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo;

namespace Pangoo
{
    [Serializable]
    public partial class DynamicObjectTable : ExcelTableBase
    {
        [Serializable]
        public partial class DynamicObjectRow : ExcelNamedRowBase
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
            [TableTitleGroup("中文名")]
            [HideLabel]
            [JsonMember("NameCn")]
            [ExcelTableCol("NameCn","NameCn","string", "中文名",4)]
            public string NameCn ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("位置")]
            [HideLabel]
            [JsonMember("Position")]
            [ExcelTableCol("Position","Position","Vector3", "位置",5)]
            public Vector3 Position ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("旋转")]
            [HideLabel]
            [JsonMember("Rotation")]
            [ExcelTableCol("Rotation","Rotation","Vector3", "旋转",6)]
            public Vector3 Rotation ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("触发事件Ids")]
            [HideLabel]
            [JsonMember("TriggerEventIds")]
            [ExcelTableCol("TriggerEventIds","TriggerEventIds","string", "触发事件Ids",7)]
            public string TriggerEventIds ;
        }


        [TableList]
        public List<DynamicObjectRow> Rows = new();

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
        public Dictionary<int,DynamicObjectRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as DynamicObjectTable;
          Rows.AddRange(table.Rows);
        }

        public DynamicObjectRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<DynamicObjectRow>(row_id);
          #else
          DynamicObjectRow row;
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
          Rows = LoadExcelFile<DynamicObjectRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

