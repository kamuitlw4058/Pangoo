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
    public partial class GlobalDataTable : ExcelTableBase
    {
        [Serializable]
        public partial class GlobalDataRow
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("编号")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("ID")]
            public int ID ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("键")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Key")]
            public string Key ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("类型")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Type")]
            public string Type ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("值")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Value")]
            public string Value ;
        }


        [TableList( AlwaysExpanded = true)]
        [JsonMember("GlobalData")]
        public List<GlobalDataRow> Rows ;


        /// <summary> 获取表头 </summary>
        public override string[] GetHeadNames()
        {
            return new string[]{"ID","Key","Type","Value"};
        }


        /// <summary> 获取类型名 </summary>
        public override string[] GetTypeNames()
        {
            return new string[]{"int","string","string","string"};
        }


        /// <summary> 获取描述名 </summary>
        public override string[] GetDescNames()
        {
            return new string[]{"编号","键","类型","值"};
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
          Rows = LoadExcelFile<GlobalDataRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

