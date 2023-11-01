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
        ConditionVariableBoolParams m_Params = new ConditionVariableBoolParams();

        public override IParams Params => m_Params;



        public ConditionVariableBool()
        { }


        protected override int Run(Args args)
        {
            var runtimeService = args.Main.GetService<RuntimeDataService>();
            var variable = runtimeService.GetVariable<bool>(m_Params.VariableId);
            return variable == m_Params.CheckBool ? 1 : 0;
        }



    }
}
