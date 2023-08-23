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
    public partial class UiConfigInfoTable : ExcelTableBase
    {
        [Serializable]
        public partial class UiConfigInfoRow : ExcelRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("UI名")]
            [HideLabel]
            [JsonMember("Name")]
            [ExcelTableCol("Name","Name","string", "UI名",2)]
            public string Name ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("FGUI包名")]
            [HideLabel]
            [JsonMember("PackageName")]
            [ExcelTableCol("PackageName","PackageName","string", "FGUI包名",3)]
            public string PackageName ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("组件名")]
            [HideLabel]
            [JsonMember("ComponentName")]
            [ExcelTableCol("ComponentName","ComponentName","string", "组件名",4)]
            public string ComponentName ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("排序索引")]
            [HideLabel]
            [JsonMember("SortingOrder")]
            [ExcelTableCol("SortingOrder","SortingOrder","int", "排序索引",5)]
            public int SortingOrder ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("模糊背景")]
            [HideLabel]
            [JsonMember("BlurMask")]
            [ExcelTableCol("BlurMask","BlurMask","bool", "模糊背景",6)]
            public bool BlurMask ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("忽略刘海屏")]
            [HideLabel]
            [JsonMember("IgnoreNotch")]
            [ExcelTableCol("IgnoreNotch","IgnoreNotch","bool", "忽略刘海屏",7)]
            public bool IgnoreNotch ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("从不关闭")]
            [HideLabel]
            [JsonMember("NeverClose")]
            [ExcelTableCol("NeverClose","NeverClose","bool", "从不关闭",8)]
            public bool NeverClose ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("不进入堆栈")]
            [HideLabel]
            [JsonMember("IgnoreStack")]
            [ExcelTableCol("IgnoreStack","IgnoreStack","bool", "不进入堆栈",9)]
            public bool IgnoreStack ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("隐藏上层界面")]
            [HideLabel]
            [JsonMember("HidePause")]
            [ExcelTableCol("HidePause","HidePause","bool", "隐藏上层界面",10)]
            public bool HidePause ;
        }


        [TableList]
        public List<UiConfigInfoRow> Rows = new();

        public override List<ExcelRowBase> BaseRows{
          get{
              List<ExcelRowBase> ret = new List<ExcelRowBase>();
              ret.AddRange(Rows);
              return ret;
          }
        }

        [NonSerialized]
        [XmlIgnore]
        public Dictionary<int,UiConfigInfoRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as UiConfigInfoTable;
          Rows.AddRange(table.Rows);
        }

        public UiConfigInfoRow GetRowById(int row_id){
          UiConfigInfoRow row;
          if(Dict.TryGetValue(row_id,out row)){
              return row;
          }
          return null;
         }

#if UNITY_EDITOR
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<UiConfigInfoRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

