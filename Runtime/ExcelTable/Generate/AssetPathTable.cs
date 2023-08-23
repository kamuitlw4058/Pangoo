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
        public partial class AssetPathRow : ExcelRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源名称")]
            [HideLabel]
            [JsonMember("Name")]
            [ExcelTableCol("Name","Name","string", "资源名称",2)]
            public string Name ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源类型")]
            [HideLabel]
            [JsonMember("AssetType")]
            [ExcelTableCol("AssetType","AssetType","string", "资源类型",3)]
            public string AssetType ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源包Id")]
            [HideLabel]
            [JsonMember("AssetPackageId")]
            [ExcelTableCol("AssetPackageId","AssetPackageId","int", "资源包Id",4)]
            public int AssetPackageId ;

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
            [TableTitleGroup("资源描述")]
            [HideLabel]
            [JsonMember("Desc")]
            [ExcelTableCol("Desc","Desc","string", "资源描述",6)]
            public string Desc ;
        }


        [TableList]
        public List<AssetPathRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
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

        public AssetPathRow GetRowById(int row_id){
          AssetPathRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
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

