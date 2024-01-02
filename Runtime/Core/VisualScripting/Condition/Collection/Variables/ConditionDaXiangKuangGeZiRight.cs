using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Variable/大相框格子右键条件判断")]
    [Serializable]
    public class ConditionDaXiangKuangGeZiRight : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        private ConditionDaXiangKuangGeZiRightParams ParamsRaw = new ConditionDaXiangKuangGeZiRightParams();
        public override IParams Params => ParamsRaw;
        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;
        protected override int Run(Args args)
        {
            bool isPlaying = args.dynamicObject.GetVariable<bool>(ParamsRaw.PlayingVariableUuid);
            bool isTipShow = args.dynamicObject.GetVariable<bool>(ParamsRaw.TipShowVariableUuid);

            if (isPlaying)
            {
                return 0;
            }
            else
            {
                if (isTipShow)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
    }
}
