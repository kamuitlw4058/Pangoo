// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo
{
     [Serializable]
    public partial class UiConfigInfoTable : ExcelTableBase
    {
        [Serializable]
        public partial class UiConfigInfoRow
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("编号")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Id")]
            public int Id ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("UI名")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Name")]
            public string Name ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("FGUI包名")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("PackageName")]
            public string PackageName ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("组件名")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("ComponentName")]
            public string ComponentName ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("排序索引")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("SortingOrder")]
            public int SortingOrder ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("模糊背景")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("BlurMask")]
            public bool BlurMask ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("忽略刘海屏")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("IgnoreNotch")]
            public bool IgnoreNotch ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("从不关闭")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("NeverClose")]
            public bool NeverClose ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("不进入堆栈")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("IgnoreStack")]
            public bool IgnoreStack ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("隐藏上层界面")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("HidePause")]
            public bool HidePause ;
        }


        [TableList( AlwaysExpanded = true)]
        [JsonMember("UiConfigInfo")]
        public List<UiConfigInfoRow> Rows ;


        /// <summary> 获取表头 </summary>
        public override string[] GetHeadNames()
        {
            return new string[]{"Id","Name","PackageName","ComponentName","SortingOrder","BlurMask","IgnoreNotch","NeverClose","IgnoreStack","HidePause"};
        }


        /// <summary> 获取类型名 </summary>
        public override string[] GetTypeNames()
        {
            return new string[]{"int","string","string","string","int","bool","bool","bool","bool","bool"};
        }


        /// <summary> 获取描述名 </summary>
        public override string[] GetDescNames()
        {
            return new string[]{"编号","UI名","FGUI包名","组件名","排序索引","模糊背景","忽略刘海屏","从不关闭","不进入堆栈","隐藏上层界面"};
        }


        /// <summary> 获取表的每行数据 </summary>
        public override List<string[]> GetTableRowDataList()
        {
            List<string[]> tmpRowDataList = new List<string[]>();
            foreach (var item in Rows)
            {
                tmpRowDataListAdd(tmpRowDataList,item);
            }
            return tmpRowDataList;
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

