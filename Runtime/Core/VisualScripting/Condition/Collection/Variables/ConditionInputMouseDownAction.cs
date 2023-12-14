using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Condition Input")]

    [Category("Input/MouseDown")]

    [Serializable]
    public class ConditionInputMouseDownAction : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        ConditionInputMouseParams m_Params = new ConditionInputMouseParams();
        public override IParams Params => m_Params;
        
        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;

        protected override int Run(Args args)
        {
            Mouse mouse = Mouse.current;
            if (mouse == null) return 0;

            m_Params.CheckBool= m_Params.m_Button switch
            {
                ConditionInputMouseParams.Button.Left => mouse.leftButton.wasPressedThisFrame,
                ConditionInputMouseParams.Button.Right => mouse.rightButton.wasPressedThisFrame,
                ConditionInputMouseParams.Button.Middle => mouse.middleButton.wasPressedThisFrame,
                ConditionInputMouseParams.Button.Forward => mouse.forwardButton.wasPressedThisFrame,
                ConditionInputMouseParams.Button.Back => mouse.backButton.wasPressedThisFrame,
            };
            return m_Params.CheckBool ? 1 : 0;
        }
    }
}

