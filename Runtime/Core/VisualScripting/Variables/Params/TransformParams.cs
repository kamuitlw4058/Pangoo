using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class TransformParams : VariableParams
    {
        [LabelText("默认值")]
        public TransformValue DefaultValue;

        public override void Load(string val)
        {
            DefaultValue = val.ToTransformValue();
        }

        public override string Save()
        {
            return DefaultValue.ToSave();
        }
    }
}