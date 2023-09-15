using System;
using UnityEngine.InputSystem;

namespace Pangoo.Core.Common
{
    [Title("Jump")]
    [Category("Usage/Jump")]

    // [Description("Cross-device support for the 'Jump' skill: Space key on Keyboards and the South Button on Gamepads")]
    // [Image(typeof(IconCharacterJump), ColorTheme.Type.Green)]

    [Serializable]
    public class InputButtonInteraction : InputButtonInputAction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private InputAction m_InputAction;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override InputAction InputAction
        {
            get
            {
                if (this.m_InputAction == null)
                {
                    this.m_InputAction = new InputAction(
                        "Interaction",
                        InputActionType.Button
                    );

                    // this.m_InputAction.AddBinding("<Gamepad>/buttonSouth");
                    this.m_InputAction.AddBinding("<Keyboard>/E");
                }

                return this.m_InputAction;
            }
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static InputButtonInteraction Create()
        {
            return new InputButtonInteraction();
        }
    }
}