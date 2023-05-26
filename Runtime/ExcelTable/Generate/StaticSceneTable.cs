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
    public partial class StaticSceneTable : ExcelTableBase
    {
        [Serializable]
        public partial class StaticSceneRow
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
            [TableTitleGroup("资源路径")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("AssetPathId")]
            public int AssetPathId ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("实体组")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("EntityGroupId")]
            public int EntityGroupId ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("加载场景")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("LoadSceneIds")]
            public string LoadSceneIds ;
        }


        [TableList( AlwaysExpanded = true)]
        [JsonMember("StaticScene")]
        public List<StaticSceneRow> Rows ;


        /// <summary> 获取表头 </summary>
        public override string[] GetHeadNames()
        {
            return new string[]{"Id","Name","AssetPathId","EntityGroupId","LoadSceneIds"};
        }


        /// <summary> 获取类型名 </summary>
        public override string[] GetTypeNames()
        {
            return new string[]{"int","string","int","int","string"};
        }


        /// <summary> 获取描述名 </summary>
        public override string[] GetDescNames()
        {
            return new string[]{"编号","名字","资源路径","实体组","加载场景"};
        }


        /// <summary> 获取表的每行数据 </summary>
        public override List<string[]> GetTableRowDataList()
        {
            List<string[]> tmpRowDataList = new List<string[]>();
            foreach (var item in Rows)
            {
                string[] texts = new string[item.GetType().GetFields().Length];
                for (int i = 0; i < texts.Length; i++)
                {
                  string valueText = item.GetType().GetFields()[i].GetValue(item).ToString();
                  texts[i] = item.GetType().GetFields()[i].GetValue(item) != null ?valueText: "";
                }
                tmpRowDataList.Add(texts);
            }
            return tmpRowDataList;
        }
        /// <summary> 从CSV文件重新构建数据 </summary>
        public virtual void LoadCSVFile(string csvFilePath)
        {
            Rows=new ();
            var result = CSVHelper.ParseCSV(File.ReadAllText(csvFilePath));
            for (int i = GetTableHeadList().Count; i < result.Count; i++)
            {
               StaticSceneRow  eventsRow = new StaticSceneRow();
                var eventRowFieldInfos = eventsRow.GetType().GetFields();
                for (int j = 0; j < eventRowFieldInfos.Length; j++)
                {
                       var value = StringConvert.ToValue(eventRowFieldInfos[j].FieldType, result[i][j].ToString());
                       eventRowFieldInfos[j].SetValue(eventsRow,value);
                }
                Rows.Add(eventsRow);
            }
        }


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "Assets/Plugins/Pangoo/StreamRes/ExcelTable/Json/cn/StaticSceneTable.json";
        }

    }
}

