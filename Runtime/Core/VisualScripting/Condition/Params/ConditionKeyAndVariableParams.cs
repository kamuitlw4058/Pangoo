using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class ConditionKeyAndVariableParams : ConditionParams
    {
        [JsonMember("KeyCodeVal")]
        [LabelText("按键")]
        public KeyCode KeyCodeVal;

        [JsonMember("VariableType")]
        [LabelText("变量类型")]
        public VariableTypeEnum VariableType;

        [JsonMember("VariableUuid")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableType.ToString(),false)")]
        [LabelText("变量Uuid")]
        public string VariableUuid;

        [LabelText("状态映射表")]
        [JsonNoMember]
        [ReadOnly]
        public Dictionary<Tuple<ConditionKeyTypeEnum, bool>, int> StateMapper = new Dictionary<Tuple<ConditionKeyTypeEnum, bool>, int>()
        {
            {new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.None,false),0},
            {new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.None,true),1},

            {new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Down,false),2},
            {new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Down,true),3},

            {new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Press,false),4},
            {new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Press,true),5},


            {new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Up,false),6},
            {new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Up,true),7},
        };

    }
}

