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

    [Category("Variable/Int")]

    [Serializable]
    public class ConditionVariableInt : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionVariableIntParams ParamRaw = new ConditionVariableIntParams();

        public override IParams Params => ParamRaw;



        public ConditionVariableInt()
        { }


        protected override int Run(Args args)
        {
            var variable = args.dynamicObject.GetVariable<int>(ParamRaw.VariableUuid);
            Debug.Log($"Condition Id:{ParamRaw.VariableUuid} Ret:{variable} ret:{variable}");
            return variable;
        }



    }
}
