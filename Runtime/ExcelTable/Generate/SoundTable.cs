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
    public partial class SoundTable : ExcelTableBase
    {
        [Serializable]
        public partial class SoundRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("包目录")]
            [HideLabel]
            [JsonMember("PackageDir")]
            [ExcelTableCol("PackageDir","PackageDir","string", "包目录",3)]
            public string PackageDir ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("音频类型")]
            [HideLabel]
            [JsonMember("SoundType")]
            [ExcelTableCol("SoundType","SoundType","string", "音频类型",4)]
            public string SoundType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源路径")]
            [HideLabel]
            [JsonMember("AssetPath")]
            [ExcelTableCol("AssetPath","AssetPath","string", "资源路径",5)]
            public string AssetPath ;
        }


        [TableList]
        public List<SoundRow> Rows = new();

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
        public Dictionary<int,SoundRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as SoundTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<SoundRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as SoundRow);
        }

        #endif

        public SoundRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<SoundRow>(row_id);
          #else
          SoundRow row;
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
          Rows = LoadExcelFile<SoundRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

