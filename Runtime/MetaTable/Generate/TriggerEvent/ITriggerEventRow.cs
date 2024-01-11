// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    public partial interface ITriggerEventRow : IMetaTableRow
    {

        public bool Enabled{ get; set; }

        public string TriggerType{ get; set; }

        public string Targets{ get; set; }

        public int TargetListType{ get; set; }

        public string InstructionList{ get; set; }

        public string Params{ get; set; }

        public string ConditionType{ get; set; }

        public string ConditionList{ get; set; }

        public string FailInstructionList{ get; set; }

        public string ConditionUuidList{ get; set; }

        public bool UseVariableCondition{ get; set; }

        public string BoolVariableUuds{ get; set; }

        public string IntVariableUuid{ get; set; }

    }
}

