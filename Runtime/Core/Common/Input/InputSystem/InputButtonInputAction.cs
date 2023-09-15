using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pangoo.Core.Common
{
    [Serializable]
    public abstract class InputButtonInputAction : InputButtonBase
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        public abstract InputAction InputAction { get; }

        public override bool Active
        {
            get => this.InputAction?.enabled ?? false;
            set
            {
                switch (value)
                {
                    case true: this.Enable(); break;
                    case false: this.Disable(); break;
                }
            }
        }

        // INITIALIZERS: --------------------------------------------------------------------------

        public override void OnAwake()
        {
            this.Enable();
        }

        public override void OnDestroy()
        {
            this.Disable();
            this.InputAction?.Dispose();
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        protected virtual void Enable()
        {
            if (this.InputAction == null) return;
            if (this.InputAction.enabled)
            {
                this.InputAction.started -= this.ExecuteEventStart;
                this.InputAction.canceled -= this.ExecuteEventCancel;
                this.InputAction.performed -= this.ExecuteEventPerform;
            }
            else
            {
                this.InputAction.Enable();
            }

            this.InputAction.started += this.ExecuteEventStart;
            this.InputAction.canceled += this.ExecuteEventCancel;
            this.InputAction.performed += this.ExecuteEventPerform;
        }

        protected virtual void Disable()
        {
            if (this.InputAction == null) return;
            if (this.InputAction.enabled) this.InputAction.Disable();

            this.InputAction.started -= this.ExecuteEventStart;
            this.InputAction.canceled -= this.ExecuteEventCancel;
            this.InputAction.performed -= this.ExecuteEventPerform;
        }

        protected void ExecuteEventStart(InputAction.CallbackContext context)
        {
            this.ExecuteEventStart();
        }

        protected void ExecuteEventCancel(InputAction.CallbackContext context)
        {
            this.ExecuteEventCancel();
        }

        protected void ExecuteEventPerform(InputAction.CallbackContext context)
        {
            this.ExecuteEventPerform();
        }


        public override bool WasPressedThisFrame()
        {
            return InputAction?.WasPressedThisFrame() ?? false;
        }


        public override bool IsPressed()
        {
            return InputAction?.IsPressed() ?? false;
        }

        public override bool WasReleasedThisFrame()
        {
            return InputAction?.WasReleasedThisFrame() ?? false;
        }
    }
}