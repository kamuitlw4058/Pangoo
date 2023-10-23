using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class BoolParams : VariableParams
    {
        [LabelText("默认值")]
        public bool DefaultValue;

        public override void Load(string val)
        {
            bool.TryParse(val, out DefaultValue);
        }

        public override string Save()
        {
            return DefaultValue.ToString();
        }
    }
}