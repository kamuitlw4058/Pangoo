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
            [ExcelTableCol("Position","Position","string", "位置",5)]
            public string Position ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("旋转")]
            [HideLabel]
            [JsonMember("Rotation")]
            [ExcelTableCol("Rotation","Rotation","string", "旋转",6)]
            public string Rotation ;
        }


        [TableList]
        public List<DynamicObjectRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }

        public override List<ExcelNamedRowBase> NamedBaseRows{
          get{
              List<ExcelNamedRowBase> ret = new List<ExcelNamedRowBase>();
              ret.AddRange(Rows);
              return ret;
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
          DynamicObjectRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
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

