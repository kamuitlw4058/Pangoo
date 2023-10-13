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
        public partial class GameSectionRow : ExcelNamedRowBase
        {

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
            [JsonMember("InitSceneIds")]
            [ExcelTableCol("InitSceneIds","InitSceneIds","string", "进入章节默认加载的场景",6)]
            public string InitSceneIds ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("动态物体")]
            [HideLabel]
            [JsonMember("DynamicObjectIds")]
            [ExcelTableCol("DynamicObjectIds","DynamicObjectIds","string", "动态物体",7)]
            public string DynamicObjectIds ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("初始化后执行的指令")]
            [HideLabel]
            [JsonMember("InitedInstructionIds")]
            [ExcelTableCol("InitedInstructionIds","InitedInstructionIds","string", "初始化后执行的指令",8)]
            public string InitedInstructionIds ;
        }


        [TableList]
        public List<GameSectionRow> Rows = new();

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
        public Dictionary<int,GameSectionRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as GameSectionTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<GameSectionRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as GameSectionRow);
        }

        #endif

        public GameSectionRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<GameSectionRow>(row_id);
          #else
          GameSectionRow row;
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

