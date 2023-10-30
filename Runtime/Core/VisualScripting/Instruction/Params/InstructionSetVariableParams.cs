using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionSetVariableParams : InstructionParams
    {
        [JsonMember("VariableId")]
        [ValueDropdown("OnVariableIdValueDropdown")]
        public int VariableId;


        [JsonMember("Val")]
        public bool Val;

#if UNITY_EDITOR
        IEnumerable OnVariableIdValueDropdown()
        {
            return GameSupportEditorUtility.GetVariableIds(VariableValueTypeEnum.Bool.ToString());
        }
#endif


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionSetVariableParams>(val);
            VariableId = par.VariableId;
            Val = par.Val;
        }
    }
}