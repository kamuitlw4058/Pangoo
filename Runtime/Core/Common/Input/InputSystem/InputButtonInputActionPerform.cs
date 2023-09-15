using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pangoo.Core.Common
{
    // [Title("Input Action Perform (Button)")]
    // [Category("Input System/Input Action Perform (Button)")]

    // [Description("When an Input Action asset of Button type runs the Performed phase")]
    // [Image(typeof(IconBoltOutline), ColorTheme.Type.Blue, typeof(OverlayArrowRight))]

    // [Keywords("Unity", "Asset", "Map", "Release")]

    [Serializable]
    public class InputButtonInputActionPerform : InputButtonInputAction
    {

        [SerializeField] private InputActionFromAssetButton m_Input = new InputActionFromAssetButton();


        public override InputAction InputAction => m_Input?.InputAction;

        public override void OnDestroy()
        {
            this.Disable();
            this.m_Input?.InputAction?.Dispose();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        protected override void Enable()
        {
            if (this.m_Input.InputAction == null) return;
            if (this.m_Input.InputAction.enabled) return;

            this.m_Input.InputAction.Enable();
            this.m_Input.InputAction.started += this.ExecuteEventStart;
            this.m_Input.InputAction.canceled += this.ExecuteEventCancel;
            this.m_Input.InputAction.performed += this.ExecuteEventPerform;
        }

        protected override void Disable()
        {
            if (this.m_Input.InputAction is not { enabled: true }) return;

            this.m_Input.InputAction.Disable();
            this.m_Input.InputAction.started -= this.ExecuteEventStart;
            this.m_Input.InputAction.canceled -= this.ExecuteEventCancel;
            this.m_Input.InputAction.performed -= this.ExecuteEventPerform;
        }




    }
}