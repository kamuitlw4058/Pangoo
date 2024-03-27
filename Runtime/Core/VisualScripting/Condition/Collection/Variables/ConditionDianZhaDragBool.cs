using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Variable/电闸拖拽状态条件")]
    [Serializable]
    public class ConditionDianZhaDragBool : Condition
    {
        public ConditionDianZhaDragBoolParams ParamsRaw = new ConditionDianZhaDragBoolParams();
        public override IParams Params => ParamsRaw;
        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;
        protected override int Run(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            var canInvokeVariable=args.dynamicObject.GetVariable<bool>(ParamsRaw.InvokeFlagVariableUuid);
            if (trans.rotation.eulerAngles==ParamsRaw.Value && canInvokeVariable)
            {
                return 1;
            }

            var currentCount=args.dynamicObject.GetVariable<int>(ParamsRaw.CountVariableUuid);
            if (currentCount==ParamsRaw.TargetCount)
            {
                return 2;
            }
            return 0;
        }
    }

}
