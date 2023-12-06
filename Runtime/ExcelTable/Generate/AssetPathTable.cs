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
    public partial class AssetPathTable : ExcelTableBase
    {
        [Serializable]
        public partial class AssetPathRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源包Id")]
            [HideLabel]
            [JsonMember("AssetPackageDir")]
            [ExcelTableCol("AssetPackageDir","AssetPackageDir","string", "资源包Id",3)]
            public string AssetPackageDir ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源类型")]
            [HideLabel]
            [JsonMember("AssetType")]
            [ExcelTableCol("AssetType","AssetType","string", "资源类型",4)]
            public string AssetType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源路径")]
            [HideLabel]
            [JsonMember("AssetPath")]
            [ExcelTableCol("AssetPath","AssetPath","string", "资源路径",5)]
            public string AssetPath ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("")]
            [HideLabel]
            [JsonMember("AssetGroup")]
            [ExcelTableCol("AssetGroup","AssetGroup","string", "",6)]
            public string AssetGroup ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源描述")]
            [HideLabel]
            [JsonMember("Desc")]
            [ExcelTableCol("Desc","Desc","string", "资源描述",7)]
            public string Desc ;
        }


        [TableList]
        public List<AssetPathRow> Rows = new();

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
        public Dictionary<int,AssetPathRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as AssetPathTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<AssetPathRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as AssetPathRow);
        }

        #endif

        public AssetPathRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<AssetPathRow>(row_id);
          #else
          AssetPathRow row;
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
          Rows = LoadExcelFile<AssetPathRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

