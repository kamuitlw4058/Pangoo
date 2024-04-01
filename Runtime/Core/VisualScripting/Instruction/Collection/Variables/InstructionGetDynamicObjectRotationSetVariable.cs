using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("DynamicObject/SetLocalVariable")]
    [Category("动态物体/获取动态物体本身Rotaion赋值Vector3变量")]
    [Serializable]
    public class InstructionGetDynamicObjectRotationSetVariable : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionGetDynamicObjectRotationSetVariableParams ParamsRaw =
            new InstructionGetDynamicObjectRotationSetVariableParams();
        public override IParams Params => this.ParamsRaw;

        public override void RunImmediate(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            var rotationEuler = trans.rotation.eulerAngles;
            Debug.Log($"获取欧拉角{rotationEuler}");
            args?.Main?.RuntimeData.SetDynamicObjectVariable<Vector3>(args.dynamicObject.Row.Uuid,ParamsRaw.VariableUuid, rotationEuler);
        }
    }
}

