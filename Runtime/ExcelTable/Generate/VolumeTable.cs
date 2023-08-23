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
    public partial class VolumeTable : ExcelTableBase
    {
        [Serializable]
        public partial class VolumeRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("中文名")]
            [HideLabel]
            [JsonMember("NameCn")]
            [ExcelTableCol("NameCn","NameCn","string", "中文名",3)]
            public string NameCn ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("相关描述")]
            [HideLabel]
            [JsonMember("Desc")]
            [ExcelTableCol("Desc","Desc","string", "相关描述",4)]
            public string Desc ;
        }


        [TableList]
        public List<VolumeRow> Rows = new();

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
        public Dictionary<int,VolumeRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as VolumeTable;
          Rows.AddRange(table.Rows);
        }

        public VolumeRow GetRowById(int row_id){
          VolumeRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
         }

#if UNITY_EDITOR
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<VolumeRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

