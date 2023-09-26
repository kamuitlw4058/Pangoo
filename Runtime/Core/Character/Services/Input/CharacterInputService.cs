using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Character
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


        [ShowInInspector]
        public Vector2 InputMove { get; private set; }

        [ShowInInspector]
        public Vector2 InputRotation { get; private set; }


        [ShowInInspector]
        public bool InputInteraction { get; private set; }



        [ShowInInspector]
        public bool InputJump { get; private set; }


        public CharacterInputService(INestedService parent) : base(parent)
        {
        }

        public override void DoAwake()
        {
            m_InputMove.OnAwake();
            m_InputRotation.OnAwake();
            m_InputInteraction.OnAwake();
            m_InputJump.OnAwake();
        }

        public override void DoUpdate()
        {
            InputMove = m_InputMove?.Read() ?? Vector2.zero;

            InputRotation = m_InputRotation?.Read() ?? Vector2.zero;

            InputInteraction = m_InputInteraction?.WasPressedThisFrame() ?? false;
            if (InputInteraction)
            {
                Debug.Log($"InputTineraction.Down");
            }

            InputJump = m_InputJump?.WasPressedThisFrame() ?? false;
            // Debug.Log($"InputMove:{InputMove}, InputRotation:{InputRotation} InputInteraction:{InputInteraction} InputJump:{InputJump}");
        }


        public override void DoDestroy()
        {
            m_InputMove.OnDestroy();
            m_InputRotation.OnDestroy();
            m_InputInteraction.OnDestroy();
            m_InputJump.OnDestroy();
        }



    }

}

