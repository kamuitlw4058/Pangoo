using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    public enum ConditionTypeEnum
    {
        [LabelText("无条件")]
        NoCondition,
        [LabelText("Bool条件")]
        BoolCondition,

        [LabelText("状态条件")]
        StateCondition,
        /*[LabelText("输入条件")]
        InputCondition,*/
    }
}


 