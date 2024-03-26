using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Condition Variable")]

    [Category("Variable/比较Vector3的值")]

    [Serializable]
    public class ConditionCompareVariableVector3 : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionCompareVariableVector3Params ParamRaw = new ConditionCompareVariableVector3Params();

        public override IParams Params => ParamRaw;
        

        protected override int Run(Args args)
        {
            var variable = args.dynamicObject.GetVariable<Vector3>(ParamRaw.VariableUuid);
            //Debug.Log($"Condition Id:{ParamRaw.VariableUuid} Ret:{variable} Check:{ParamRaw.Value}");
            return variable == ParamRaw.Value ? 1 : 0;
        }
    }
}

