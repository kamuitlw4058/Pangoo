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
    public partial class PangooEventsTable : ExcelTableBase
    {
        [Serializable]
        public partial class PangooEventsRow
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("事件的命名空间")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Namesapce")]
            public string Namesapce ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("事件名称")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("EventName")]
            public string EventName ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("描述")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("desc")]
            public string Desc ;
        }


        [TableList( AlwaysExpanded = true)]
        [JsonMember("PangooEvents")]
        public List<PangooEventsRow> Rows ;


        /// <summary> 获取表头 </summary>
        public override string[] GetHeadNames()
        {
            return new string[]{"Namesapce,EventName,desc"};
        }


        /// <summary> 获取类型名 </summary>
        public override string[] GetTypeNames()
        {
            return new string[]{"string,string,string"};
        }


        /// <summary> 获取描述名 </summary>
        public override string[] GetDescNames()
        {
            return new string[]{"事件的命名空间,事件名称,描述"};
        }


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "Assets/Plugins/Pangoo/StreamRes/ExcelTable/Json/cn/PangooEventsTable.json";
        }

    }
}

