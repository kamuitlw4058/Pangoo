using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Variable/LocalBool")]

    [Serializable]
    public class ConditionLocalVariableBool : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionLocalVariableBoolParams ParamsRaw = new ConditionLocalVariableBoolParams();

        public override IParams Params => ParamsRaw;
        protected override int Run(Args args)
        {
            var variable = args.Main.RuntimeData.GetDynamicObjectVariable<bool>(ParamsRaw.DynamicObjectUuid, ParamsRaw.VariableUuid);
            return variable == ParamsRaw.CheckBool ? 1 : 0;
        }
    }
}
