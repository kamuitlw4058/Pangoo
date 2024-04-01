using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Variable/对象拖拽状态条件")]
    [Serializable]
    public class ConditionObjectDragBool : Condition
    {
        public ConditionObjectDragBoolParams ParamsRaw = new ConditionObjectDragBoolParams();
        public override IParams Params => ParamsRaw;
        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;
        protected override int Run(Args args)
        {
            var trans = args.dynamicObject?.CachedTransfrom.Find(ParamsRaw.Path);
            var canInvokeVariable=args.Main.RuntimeData.GetDynamicObjectVariable<bool>(ParamsRaw.DynamicObjectUuid,ParamsRaw.InvokeFlagVariableUuid);
            var currentCount=args.Main.RuntimeData.GetDynamicObjectVariable<int>(ParamsRaw.DynamicObjectUuid,ParamsRaw.CountVariableUuid);
            if (trans.Equals(null))
            {
                Debug.LogError("没有找到目标节点");
                return -1;
            }

            if (trans.rotation.eulerAngles==ParamsRaw.TargetValue && canInvokeVariable && currentCount!=ParamsRaw.TargetCount)
            {
                return 1;
            }
            if (currentCount==ParamsRaw.TargetCount)
            {
                return 2;
            }
            if (!canInvokeVariable)
            {
                return 3;
            }

            return 0;
        }
    }

}
