using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class ConditionIlluminationItemOpenBoolParams : ConditionParams
    {
        [JsonMember("GameSectionUuidList")]
        [ValueDropdown("OnGameSectionUuidValueDropdown")]
        [LabelText("包含的章节段落")]
        public List<string> GameSectionUuidList=new List<string>();
        
        [JsonMember("VariableType")]
        [OnValueChanged("OnVariableTypeChanged")]
        public VariableTypeEnum VariableType;
        
        [JsonMember("CanOpenVariableUuid")]
        [ValueDropdown("OnVariableUuidValueDropdown")]
        [LabelText("照明道具能否开启变量Uuid")]
        public string CanOpenVariableUuid;
        
        [JsonMember("OpenStateVariableUuid")]
        [ValueDropdown("OnVariableUuidValueDropdown")]
        [LabelText("照明道具开启状态变量Uuid")]
        public string OpenStateVariableUuid;

        
        [ValueDropdown("OnGameSectionUuidValueDropdown")]
        [LabelText("当前的章节段落")]
        [ReadOnly]
        public string CurrentGameSection;

        [LabelText("照明道具能否开启")]
        [ReadOnly]
        public bool CanOpen;
        
        [LabelText("照明道具当前是否开启")]
        [ReadOnly]
        public bool CurrentOpenState;
        
        
#if UNITY_EDITOR

        void OnVariableTypeChanged()
        {
            CanOpenVariableUuid = string.Empty;
            OpenStateVariableUuid = string.Empty;
        }
        
        IEnumerable OnGameSectionUuidValueDropdown()
        {
            return GameSectionOverview.GetUuidDropdown();
        }
        
        IEnumerable OnVariableUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableType.ToString());
        }
#endif
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<ConditionIlluminationItemOpenBoolParams>(val);
            GameSectionUuidList = par.GameSectionUuidList;
            VariableType = par.VariableType;
            CanOpenVariableUuid = par.CanOpenVariableUuid;
            OpenStateVariableUuid = par.OpenStateVariableUuid;
        }
    }
}

