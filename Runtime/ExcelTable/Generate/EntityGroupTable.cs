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
    public partial class EntityGroupTable : ExcelTableBase
    {
        [Serializable]
        public partial class EntityGroupRow
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
            [TableTitleGroup("组名字")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Name")]
            public string Name ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("自动释放可释放对象的间隔秒数")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("InstanceAutoReleaseInterval")]
            public double InstanceAutoReleaseInterval ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("实例对象池的容量")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("InstanceCapacity")]
            public int InstanceCapacity ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("对象池对象过期秒数")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("InstanceExpireTime")]
            public double InstanceExpireTime ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("实体组实例对象池的优先级")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("InstancePriority")]
            public int InstancePriority ;
        }


        [TableList( AlwaysExpanded = true)]
        [JsonMember("EntityGroup")]
        public List<EntityGroupRow> Rows ;


        /// <summary> 获取表头 </summary>
        public override string[] GetHeadNames()
        {
            return new string[]{"Id","Name","InstanceAutoReleaseInterval","InstanceCapacity","InstanceExpireTime","InstancePriority"};
        }


        /// <summary> 获取类型名 </summary>
        public override string[] GetTypeNames()
        {
            return new string[]{"int","string","double","int","double","int"};
        }


        /// <summary> 获取描述名 </summary>
        public override string[] GetDescNames()
        {
            return new string[]{"编号","组名字","自动释放可释放对象的间隔秒数","实例对象池的容量","对象池对象过期秒数","实体组实例对象池的优先级"};
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
               EntityGroupRow  eventsRow = new EntityGroupRow();
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
            return "Assets/Plugins/Pangoo/StreamRes/ExcelTable/Json/cn/EntityGroupTable.json";
        }

    }
}

