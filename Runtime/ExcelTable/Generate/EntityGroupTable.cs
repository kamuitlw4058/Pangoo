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
    public partial class EntityGroupTable : ExcelTableBase
    {
        [Serializable]
        public partial class EntityGroupRow : ExcelNamedRowBase
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("自动释放可释放对象的间隔秒数")]
            [HideLabel]
            [JsonMember("InstanceAutoReleaseInterval")]
            [ExcelTableCol("InstanceAutoReleaseInterval","InstanceAutoReleaseInterval","double", "自动释放可释放对象的间隔秒数",3)]
            public double InstanceAutoReleaseInterval ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("实例对象池的容量")]
            [HideLabel]
            [JsonMember("InstanceCapacity")]
            [ExcelTableCol("InstanceCapacity","InstanceCapacity","int", "实例对象池的容量",4)]
            public int InstanceCapacity ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("对象池对象过期秒数")]
            [HideLabel]
            [JsonMember("InstanceExpireTime")]
            [ExcelTableCol("InstanceExpireTime","InstanceExpireTime","double", "对象池对象过期秒数",5)]
            public double InstanceExpireTime ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("实体组实例对象池的优先级")]
            [HideLabel]
            [JsonMember("InstancePriority")]
            [ExcelTableCol("InstancePriority","InstancePriority","int", "实体组实例对象池的优先级",6)]
            public int InstancePriority ;
        }


        [TableList]
        public List<EntityGroupRow> Rows = new();

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
        public Dictionary<int,EntityGroupRow> Dict = new ();

        public override void Init(){
          Dict.Clear();
          foreach(var row in Rows){
              Dict.Add(row.Id,row);
          }
        }

        public override void Merge(ExcelTableBase val){
          var table = val as EntityGroupTable;
          Rows.AddRange(table.Rows);
        }

        public EntityGroupRow GetRowById(int row_id){
          #if UNITY_EDITOR
          return GetRowById<EntityGroupRow>(row_id);
          #else
          EntityGroupRow row;
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
          Rows = LoadExcelFile<EntityGroupRow>(excelFilePath);
        }
#endif


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

