using System;
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
    public class InputValueVector2MotionSecondary : InputValueVector2Base
    {
        private const float MIN_MAGNITUDE = 0.2f;

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private InputAction m_InputAction;

        // PROPERTIES: ----------------------------------------------------------------------------

        public InputAction InputAction
        {
            get
            {
                if (this.m_InputAction == null)
                {
                    this.m_InputAction = new InputAction(
                       "Secondary Motion",
                       InputActionType.Value
                   );

                    this.m_InputAction.AddBinding(
                        "<Mouse>/delta",
                        processors: @"
                        invertVector2(invertX=false,invertY=true),
                        scaleVector2(x=3,y=3),
                        divideScreenSize,
                        divideDeltaTime"
                    );

                    this.m_InputAction.AddBinding(
                        "<Gamepad>/rightStick",
                        processors: @"
                        invertVector2(invertX=false,invertY=true)"
                    );
                }

                return this.m_InputAction;
            }
        }

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

        public static InputValueVector2MotionSecondary Instance;

        // INITIALIZERS: --------------------------------------------------------------------------

        public static InputValueVector2MotionSecondary Create()
        {
            return new InputValueVector2MotionSecondary();
        }

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
            Vector2 value = this.InputAction?.ReadValue<Vector2>() ?? Vector2.zero;
            return value.magnitude < MIN_MAGNITUDE ? Vector2.zero : value;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        public void Enable()
        {
            this.InputAction?.Enable();
        }

        public void Disable()
        {
            this.InputAction?.Disable();
        }
    }
}