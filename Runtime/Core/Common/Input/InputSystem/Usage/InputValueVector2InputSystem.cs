using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pangoo.Core.Common
{

    [Title("Primary Motion")]
    // [Category("Usage/Primary Motion")]

    // [Description("Primary motion commonly used to move the main character: WASD keys on Keyboard and Left Stick on Gamepads")]
    // [Image(typeof(IconGamepadCross), ColorTheme.Type.Yellow)]

    // [Keywords("Move", "Joystick", "WASD", "Arrows")]

    [Serializable]
    public class InputValueVector2InputSystem : InputValueVector2Base
    {
        [HideLabel]
        [SerializeField] private InputActionFromAsset m_Input = new InputActionFromAsset();

        // PROPERTIES: ----------------------------------------------------------------------------

        public InputAction InputAction => this.m_Input.InputAction;

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

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public override Vector2 Read()
        {
            return this.InputAction?.ReadValue<Vector2>() ?? Vector2.zero;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void Enable()
        {
            this.InputAction?.Enable();
        }

        private void Disable()
        {
            this.InputAction?.Disable();
        }
    }
}