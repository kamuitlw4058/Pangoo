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
    public partial class GameSectionTable : ExcelTableBase
    {
        [Serializable]
        public partial class GameSectionRow : ExcelRowBase
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
            [TableTitleGroup("动态加载场景")]
            [HideLabel]
            [JsonMember("DynamicSceneIds")]
            [ExcelTableCol("DynamicSceneIds","DynamicSceneIds","string", "动态加载场景",3)]
            public string DynamicSceneIds ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("持续加载场景")]
            [HideLabel]
            [JsonMember("KeepSceneIds")]
            [ExcelTableCol("KeepSceneIds","KeepSceneIds","string", "持续加载场景",4)]
            public string KeepSceneIds ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("章节跳转的场景")]
            [HideLabel]
            [JsonMember("SectionJumpByScene")]
            [ExcelTableCol("SectionJumpByScene","SectionJumpByScene","string", "章节跳转的场景",5)]
            public string SectionJumpByScene ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("进入章节默认加载的场景")]
            [HideLabel]
            [JsonMember("FirstDynamicSceneIds")]
            [ExcelTableCol("FirstDynamicSceneIds","FirstDynamicSceneIds","string", "进入章节默认加载的场景",6)]
            public string FirstDynamicSceneIds ;
        }


        [TableList]
        public List<GameSectionRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }

        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,GameSectionRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
          CustomInit();
        }

        public override void Merge(ExcelTableBase val){
          var table = val as GameSectionTable;
          Rows.AddRange(table.Rows);
        }

        public GameSectionRow GetRowById(int row_id){
          GameSectionRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
         }

#if UNITY_EDITOR
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<GameSectionRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

