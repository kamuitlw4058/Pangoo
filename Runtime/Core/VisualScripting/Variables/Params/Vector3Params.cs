using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class Vector3Params : VariableParams
    {
        [LabelText("默认值")]
        public Vector3 DefaultValue;

        public override void Load(string val)
        {
            DefaultValue = val.ToVector3();
            // DefaultValue = val.toSave;
        }

        public override string Save()
        {
            return DefaultValue.ToSave();
        }
    }
}