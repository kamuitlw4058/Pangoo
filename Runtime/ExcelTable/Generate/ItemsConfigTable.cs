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
    public partial class ItemsConfigTable : ExcelTableBase
    {
        [Serializable]
        public partial class ItemsConfigRow : ExcelRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("编号")]
            [HideLabel]
            [JsonMember("ID")]
            [ExcelTableCol("ID","ID","int", "编号",1)]
            public int ID ;

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
            [TableTitleGroup("能否拾取")]
            [HideLabel]
            [JsonMember("CanPickup")]
            [ExcelTableCol("CanPickup","CanPickup","bool", "能否拾取",3)]
            public bool CanPickup ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("位置坐标")]
            [HideLabel]
            [JsonMember("Pos")]
            [ExcelTableCol("Pos","Pos","Vector3", "位置坐标",4)]
            public Vector3 Pos ;
        }


        [TableList]
        public List<ItemsConfigRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }


        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,ItemsConfigRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as ItemsConfigTable;
          Rows.AddRange(table.Rows);
        }

        public ItemsConfigRow GetRowById(int row_id){
          ItemsConfigRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
         }

#if UNITY_EDITOR
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<ItemsConfigRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

