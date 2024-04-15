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
    public class InstructionDynamicObjectSetAnimatorFloatParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("@GameSupportEditorUtility.DynamicObjectUuidDropdownWithSelf()")]
        public string DynamicObjectUuid;

        [JsonMember("Path")]
        public string Path;

        [JsonMember("AnimatorParamName")]
        public string AnimatorParamName;


        [JsonMember("Val")]
        public float Val;



    }
}