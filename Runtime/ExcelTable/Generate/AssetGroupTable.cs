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
    public partial class AssetGroupTable : ExcelTableBase
    {
        [Serializable]
        public partial class AssetGroupRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("")]
            [HideLabel]
            [JsonMember("AssetGroup")]
            [ExcelTableCol("AssetGroup","AssetGroup","string", "",3)]
            public string AssetGroup ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源描述")]
            [HideLabel]
            [JsonMember("Desc")]
            [ExcelTableCol("Desc","Desc","string", "资源描述",4)]
            public string Desc ;
        }


        [TableList]
        public List<AssetGroupRow> Rows = new();

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
        public Dictionary<int,AssetGroupRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as AssetGroupTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<AssetGroupRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as AssetGroupRow);
        }

        #endif

        public AssetGroupRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<AssetGroupRow>(row_id);
          #else
          AssetGroupRow row;
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
          Rows = LoadExcelFile<AssetGroupRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

