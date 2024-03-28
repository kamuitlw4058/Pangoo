using System;
using System.Collections;
using System.Threading.Tasks;
using GameFramework;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework;

namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    [Common.Title("Condition Variable")]

    [Category("Variable/Bool")]

    [Serializable]
    public class ConditionVariableBool : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionVariableBoolParams ParamRaw = new ConditionVariableBoolParams();

        public override IParams Params => ParamRaw;



        public ConditionVariableBool()
        { }


        protected override int Run(Args args)
        {
            var variable = args.dynamicObject.GetVariable<bool>(ParamRaw.VariableUuid);
            // Debug.Log($"Condition Id:{ParamRaw.VariableUuid} Ret:{variable} Check:{ParamRaw.CheckBool}");
            return variable == ParamRaw.CheckBool ? 1 : 0;
        }



    }
}
