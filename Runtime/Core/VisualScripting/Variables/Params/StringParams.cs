using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class StringParams : VariableParams
    {
        [LabelText("默认值")]
        public string DefaultValue;

        public override void Load(string val)
        {
            DefaultValue = val;
        }

        public override string Save()
        {
            return DefaultValue;
        }
    }
}