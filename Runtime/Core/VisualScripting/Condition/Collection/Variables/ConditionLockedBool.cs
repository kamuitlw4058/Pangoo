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

    // [Common.Title("Condition Variable")]

    [Category("Variable/带锁的可开关物体")]

    [Serializable]
    public class ConditionLockedBool : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        ConditionLockedBoolParams m_Params = new ConditionLockedBoolParams();

        public override IParams Params => m_Params;


        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;



        protected override int Run(Args args)
        {
            var openedVariable = args.dynamicObject.GetVariable<bool>(m_Params.OpenedVariableUuid);
            var LockedVariable = args.dynamicObject.GetVariable<bool>(m_Params.LockedVariableUuid);


            return LockedVariable == m_Params.LockedCheck ? openedVariable == m_Params.OpenedChecked ? 2 : 1 : 0;
        }



    }
}
