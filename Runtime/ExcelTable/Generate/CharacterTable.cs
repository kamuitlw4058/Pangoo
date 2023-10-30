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
    public partial class CharacterTable : ExcelTableBase
    {
        [Serializable]
        public partial class CharacterRow : ExcelNamedRowBase
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
            [TableTitleGroup("是否是玩家")]
            [HideLabel]
            [JsonMember("IsPlayer")]
            [ExcelTableCol("IsPlayer","IsPlayer","bool", "是否是玩家",4)]
            public bool IsPlayer ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("移动速度")]
            [HideLabel]
            [JsonMember("MoveSpeed")]
            [ExcelTableCol("MoveSpeed","MoveSpeed","float", "移动速度",5)]
            public float MoveSpeed ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("高度")]
            [HideLabel]
            [JsonMember("Height")]
            [ExcelTableCol("Height","Height","float", "高度",6)]
            public float Height ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("相机偏移")]
            [HideLabel]
            [JsonMember("CameraOffset")]
            [ExcelTableCol("CameraOffset","CameraOffset","Vector3", "相机偏移",7)]
            public Vector3 CameraOffset ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("俯仰范围")]
            [HideLabel]
            [JsonMember("MaxPitch")]
            [ExcelTableCol("MaxPitch","MaxPitch","float", "俯仰范围",8)]
            public float MaxPitch ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("只使用相机")]
            [HideLabel]
            [JsonMember("CameraOnly")]
            [ExcelTableCol("CameraOnly","CameraOnly","bool", "只使用相机",9)]
            public bool CameraOnly ;
        }


        [TableList]
        public List<CharacterRow> Rows = new();

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
        public Dictionary<int,CharacterRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as CharacterTable;
          Rows.AddRange(table.Rows);
        }

        #if UNITY_EDITOR

        public override void RemoveId(int Id){
          var row = GetRowById<CharacterRow>(Id);
          if(row == null) return;
          Rows.Remove(row);
        }

         public override void AddNamedRow(ExcelNamedRowBase row){
          Rows.Add(row as CharacterRow);
        }

        #endif

        public CharacterRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<CharacterRow>(row_id);
          #else
          CharacterRow row;
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
          Rows = LoadExcelFile<CharacterRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

