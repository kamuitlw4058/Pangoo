using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class IntParams : VariableParams
    {
        [LabelText("默认值")]
        public int DefaultValue;

        public override void Load(string val)
        {
            int.TryParse(val, out DefaultValue);
        }

        public override string Save()
        {
            return DefaultValue.ToString();
        }
    }
}