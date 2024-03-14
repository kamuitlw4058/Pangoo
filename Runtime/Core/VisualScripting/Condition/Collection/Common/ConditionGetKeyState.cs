using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Condition Variable")]

    [Category("Common/GetKeyState")]

    [Serializable]
    public class ConditionGetKeyState : Condition
    {
        [SerializeField] [HideLabel] [HideReferenceObjectPicker]
        public ConditionGetKeyParams ParamRaw = new ConditionGetKeyParams();
        public override IParams Params => ParamRaw;
        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;
        protected override int Run(Args args)
        {
            if (Input.GetKeyDown(ParamRaw.KeyCodeParams))
            {
                return ParamRaw.StringMapper["Down"];
            }
            if (Input.GetKey(ParamRaw.KeyCodeParams))
            {
                return ParamRaw.StringMapper["Press"];
            }
            if (Input.GetKeyUp(ParamRaw.KeyCodeParams))
            {
                return ParamRaw.StringMapper["Up"];
            }
            return 0;
        }
    }
}

