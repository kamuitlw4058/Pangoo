using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class Vector3Params : VariableParams
    {
        [JsonMember("DefaultValue")]
        [LabelText("默认值")]
        public Vector3 DefaultValue;
    }
}