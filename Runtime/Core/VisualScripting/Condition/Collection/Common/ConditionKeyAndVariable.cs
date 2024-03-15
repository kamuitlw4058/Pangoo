using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Condition Variable")]

    [Category("Common/KeyAndVariable")]

    [Serializable]
    public class ConditionKeyAndVariable : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionKeyAndVariableParams ParamRaw = new ConditionKeyAndVariableParams();
        public override IParams Params => ParamRaw;
        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;
        protected override int Run(Args args)
        {

            if (ParamRaw.VariableType == VariableTypeEnum.DynamicObject)
            {
                Debug.LogError("暂时不支持动态物体变量的按键和变量组合");
                return 0;
            }

            var boolVal = args.Main.RuntimeData.GetVariable<bool>(ParamRaw.VariableUuid);

            if (Input.GetKeyDown(ParamRaw.KeyCodeVal))
            {
                if (ParamRaw.StateMapper.TryGetValue(new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Down, boolVal), out int ret))
                {
                    return ret;
                }
            }
            if (Input.GetKey(ParamRaw.KeyCodeVal))
            {
                if (ParamRaw.StateMapper.TryGetValue(new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Press, boolVal), out int ret))
                {
                    return ret;
                }
            }
            if (Input.GetKeyUp(ParamRaw.KeyCodeVal))
            {
                if (ParamRaw.StateMapper.TryGetValue(new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.Down, boolVal), out int ret))
                {
                    return ret;
                }
            }

            if (ParamRaw.StateMapper.TryGetValue(new Tuple<ConditionKeyTypeEnum, bool>(ConditionKeyTypeEnum.None, boolVal), out int noneRet))
            {
                return noneRet;
            }
            return 0;
        }
    }
}

