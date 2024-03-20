using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionDynamicObjectBoolParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("@GameSupportEditorUtility.DynamicObjectUuidDropdownWithSelf()")]
        public string DynamicObjectUuid;

        [JsonMember("Val")]
        public bool Val;



    }
}