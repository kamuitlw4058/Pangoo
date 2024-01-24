using System;
using UnityEngine;
using Pangoo.Core.Services;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Characters
{

    [Serializable]
    public class CharacterInputService : CharacterBaseService
    {
        [SerializeReference]
        public InputValueVector2Base m_InputMove = InputValueVector2MotionPrimary.Create();


        [SerializeReference]
        public InputValueVector2Base m_InputRotation = InputValueVector2MotionSecondary.Create();


        [SerializeReference]
        public InputButtonBase m_InputJump = InputButtonJump.Create();


        [SerializeReference]
        public InputButtonBase m_InputInteraction = InputButtonInteraction.Create();


        public bool IsMoveInputDown
        {
            get
            {
                return InputMove != Vector2.zero;
            }
        }


        [ShowInInspector]
        public Vector2 InputMove { get; private set; }

        [ShowInInspector]
        public Vector2 InputRotation { get; private set; }


        [ShowInInspector]
        public bool InputInteraction { get; private set; }



        [ShowInInspector]
        public bool InputJump { get; private set; }


        public CharacterInputService(NestedBaseService parent) : base(parent)
        {
        }

        protected override void DoAwake()
        {
            m_InputMove.OnAwake();
            m_InputRotation.OnAwake();
            m_InputInteraction.OnAwake();
            m_InputJump.OnAwake();
        }

        protected override void DoUpdate()
        {
            if (!Character.IsControllable)
            {
                InputMove = Vector3.zero;
                InputRotation = Vector3.zero;
                InputInteraction = false;
                InputJump = false;
            }
            else
            {
                InputMove = m_InputMove?.Read() ?? Vector2.zero;

                InputRotation = m_InputRotation?.Read() ?? Vector2.zero;

                InputInteraction = m_InputInteraction?.WasPressedThisFrame() ?? false;
                InputJump = m_InputJump?.WasPressedThisFrame() ?? false;
            }

            if (InputInteraction && Character.IsInteractive)
            {
                Debug.Log($"InputTineraction.Down");
                Character.Interact();
            }


            // Debug.Log($"InputMove:{InputMove}, InputRotation:{InputRotation} InputInteraction:{InputInteraction} InputJump:{InputJump}");
        }


        protected override void DoDestroy()
        {
            m_InputMove.OnDestroy();
            m_InputRotation.OnDestroy();
            m_InputInteraction.OnDestroy();
            m_InputJump.OnDestroy();
        }



    }

}

