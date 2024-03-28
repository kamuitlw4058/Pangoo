using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Variable/LocalBool")]

    [Serializable]
    public class ConditionLocalVariableBool : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionLocalVariableBoolParams ParamsRaw = new ConditionLocalVariableBoolParams();
        
        public override IParams Params => ParamsRaw;
        protected override int Run(Args args)
        {
            var entity = args.dynamicObject.DynamicObjectService.GetLoadedEntity(ParamsRaw.DynamicObjectUuid);
            
            if (entity.DynamicObj != null)
            {
                var variable = entity.DynamicObj.GetVariable<bool>(ParamsRaw.VariableUuid);
                return variable == ParamsRaw.CheckBool ? 1 : 0;
            }
            return 0;
        }
    }
}
