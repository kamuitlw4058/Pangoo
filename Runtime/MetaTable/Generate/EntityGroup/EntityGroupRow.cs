// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class EntityGroupRow : MetaTableRow,IEntityGroupRow
    {

        [JsonMember("InstanceAutoReleaseInterval")]
        [MetaTableRowColumn("InstanceAutoReleaseInterval","double", "自动释放可释放对象的间隔秒数",3)]
        [LabelText("自动释放可释放对象的间隔秒数")]
        public double InstanceAutoReleaseInterval ;

        double IEntityGroupRow.InstanceAutoReleaseInterval {get => InstanceAutoReleaseInterval; set => InstanceAutoReleaseInterval = value;}

        [JsonMember("InstanceCapacity")]
        [MetaTableRowColumn("InstanceCapacity","int", "实例对象池的容量",4)]
        [LabelText("实例对象池的容量")]
        public int InstanceCapacity ;

        int IEntityGroupRow.InstanceCapacity {get => InstanceCapacity; set => InstanceCapacity = value;}

        [JsonMember("InstanceExpireTime")]
        [MetaTableRowColumn("InstanceExpireTime","double", "对象池对象过期秒数",5)]
        [LabelText("对象池对象过期秒数")]
        public double InstanceExpireTime ;

        double IEntityGroupRow.InstanceExpireTime {get => InstanceExpireTime; set => InstanceExpireTime = value;}

        [JsonMember("InstancePriority")]
        [MetaTableRowColumn("InstancePriority","int", "实体组实例对象池的优先级",6)]
        [LabelText("实体组实例对象池的优先级")]
        public int InstancePriority ;

        int IEntityGroupRow.InstancePriority {get => InstancePriority; set => InstancePriority = value;}

    }
}

