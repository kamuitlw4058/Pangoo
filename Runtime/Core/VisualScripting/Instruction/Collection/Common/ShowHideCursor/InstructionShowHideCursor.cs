using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pangoo.Core.VisualScripting
{
    [Category("UI/ImageFade")]
    [Serializable]
    public class InstructionShowHideCursor : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionShowHideCursorParams ParamsRaw = new InstructionShowHideCursorParams();
        public override IParams Params => this.ParamsRaw;
        
        protected override IEnumerator Run(Args args)
        {
            Cursor.visible = this.ParamsRaw.IsShow;
            Cursor.lockState = CursorLockMode.Confined;
            return null;
        }

        public override void RunImmediate(Args args)
        {
            Cursor.visible = this.ParamsRaw.IsShow;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}

