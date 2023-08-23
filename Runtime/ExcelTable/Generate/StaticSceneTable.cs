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
    public partial class StaticSceneTable : ExcelTableBase
    {
        [Serializable]
        public partial class StaticSceneRow : ExcelRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("名字")]
            [HideLabel]
            [JsonMember("Name")]
            [ExcelTableCol("Name","Name","string", "名字",2)]
            public string Name ;

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
            [TableTitleGroup("实体组")]
            [HideLabel]
            [JsonMember("EntityGroupId")]
            [ExcelTableCol("EntityGroupId","EntityGroupId","int", "实体组",4)]
            public int EntityGroupId ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("加载场景")]
            [HideLabel]
            [JsonMember("LoadSceneIds")]
            [ExcelTableCol("LoadSceneIds","LoadSceneIds","string", "加载场景",5)]
            public string LoadSceneIds ;
        }


        [TableList]
        public List<StaticSceneRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }


        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,StaticSceneRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as StaticSceneTable;
          Rows.AddRange(table.Rows);
        }

        public StaticSceneRow GetRowById(int row_id){
          StaticSceneRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
         }

#if UNITY_EDITOR
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<StaticSceneRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

