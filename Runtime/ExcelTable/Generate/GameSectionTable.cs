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
    public partial class GameSectionTable : ExcelTableBase
    {
        [Serializable]
        public partial class GameSectionRow
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
            [TableTitleGroup("名字")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Name")]
            public string Name ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("动态加载场景")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("DynamicSceneIds")]
            public string DynamicSceneIds ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("持续加载场景")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("KeepSceneIds")]
            public string KeepSceneIds ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("章节跳转的场景")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("SectionJumpByScene")]
            public string SectionJumpByScene ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("进入章节默认加载的场景")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("FirstDynamicSceneIds")]
            public string FirstDynamicSceneIds ;
        }


        [TableList( AlwaysExpanded = true)]
        [JsonMember("GameSection")]
        public List<GameSectionRow> Rows ;


        /// <summary> 获取表头 </summary>
        public override string[] GetHeadNames()
        {
            return new string[]{"Id","Name","DynamicSceneIds","KeepSceneIds","SectionJumpByScene","FirstDynamicSceneIds"};
        }


        /// <summary> 获取类型名 </summary>
        public override string[] GetTypeNames()
        {
            return new string[]{"int","string","string","string","string","string"};
        }


        /// <summary> 获取描述名 </summary>
        public override string[] GetDescNames()
        {
            return new string[]{"编号","名字","动态加载场景","持续加载场景","章节跳转的场景","进入章节默认加载的场景"};
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
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
#if UNITY_EDITOR
            Rows = LoadExcelFile<GameSectionRow>(excelFilePath);
#endif
        }


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

