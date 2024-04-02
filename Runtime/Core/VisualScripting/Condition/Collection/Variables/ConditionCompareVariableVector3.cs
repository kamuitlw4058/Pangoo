using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Condition Variable")]

    [Category("Variable/比较Vector3的值")]

    [Serializable]
    public class ConditionCompareVariableVector3 : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionCompareVariableVector3Params ParamsRaw = new ConditionCompareVariableVector3Params();

        public override IParams Params => ParamsRaw;
        

        protected override int Run(Args args)
        {
            Vector3 variable = new Vector3();
            if (ParamsRaw.ValueSourceType==ValueSourceTypeEnum.Variable)
            {
                if (ParamsRaw.VariableType==VariableTypeEnum.Global)
                {
                    variable = args.Main.RuntimeData.GetVariable<Vector3>(ParamsRaw.VariableUuid);
                }
                else
                {
                    variable = args.Main.RuntimeData.GetDynamicObjectVariable<Vector3>(args.dynamicObject.Row.Uuid,ParamsRaw.VariableUuid);
                }
                
            }

            if (ParamsRaw.ValueSourceType==ValueSourceTypeEnum.Path)
            {
                var trans = args.dynamicObject?.CachedTransfrom.Find(ParamsRaw.Path);
                if (trans.Equals(null))
                {
                    Debug.LogError("没有找到目标节点");
                    return 0;
                }
                
                if (ParamsRaw.DynamicObjectValueType==DynamicObjectValueTypeEnum.Rotation)
                {
                    variable = trans.rotation.eulerAngles;
                }
                
            }
            //Debug.Log($"Condition Id:{ParamRaw.VariableUuid} Ret:{variable} Check:{ParamRaw.Value}");
            return variable == ParamsRaw.Value ? 1 : 0;
        }
    }
}

