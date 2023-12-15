using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("GameObject/GlobalGameObjectActive")]
    [Serializable]
    public class InstructionGlobalGameObjectActive : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionGlobalGameObjectActiveParams ParamsRaw = new InstructionGlobalGameObjectActiveParams();

        public override IParams Params => ParamsRaw;
        
        protected override IEnumerator Run(Args args)
        {
            yield break;

        }

        public override void RunImmediate(Args args)
        {
            var trans = GameObject.Find(ParamsRaw.Root)?.transform.Find(ParamsRaw.RootChild);
            if (trans != null)
            {
                trans.gameObject.SetActive(ParamsRaw.Val);
            }
            return;
        }
    }
}
