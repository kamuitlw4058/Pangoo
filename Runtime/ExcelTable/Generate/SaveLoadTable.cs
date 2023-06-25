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
    public partial class SaveLoadTable : ExcelTableBase
    {
        [Serializable]
        public partial class SaveLoadRow
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
            [TableTitleGroup("Int类型的键")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("int_key")]
            public string IntKey ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("Int类型的值")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("int_value")]
            public int IntValue ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("Flaot类型的键")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("float_key")]
            public string FloatKey ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("Float类型的值")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("float_value")]
            public float FloatValue ;
        }


        [TableList( AlwaysExpanded = true)]
        [JsonMember("SaveLoad")]
        public List<SaveLoadRow> Rows ;


        /// <summary> 获取表头 </summary>
        public override string[] GetHeadNames()
        {
            return new string[]{"ID","int_key","int_value","float_key","float_value"};
        }


        /// <summary> 获取类型名 </summary>
        public override string[] GetTypeNames()
        {
            return new string[]{"int","string","int","string","float"};
        }


        /// <summary> 获取描述名 </summary>
        public override string[] GetDescNames()
        {
            return new string[]{"编号","Int类型的键","Int类型的值","Flaot类型的键","Float类型的值"};
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
          Rows = LoadExcelFile<SaveLoadRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

