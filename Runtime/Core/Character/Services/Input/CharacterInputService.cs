using System;
using UnityEngine;
using Pangoo.Core.Services;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Characters
{
    public enum InputMotionType
    {
        Default,
        OnlyW,
    }
    // [Serializable]
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
                return MoveValue != Vector2.zero;
            }
        }

        Vector2 m_LatestMoveValue;
        Vector2 m_MoveValue;

        public Vector2 LatestMoveValue
        {
            get
            {
                return m_LatestMoveValue;
            }
        }

        public bool AxisChanged(float latest, float current)
        {
            if (current == 0)
            {
                return false;
            }

            if (latest == 0)
            {
                return true;
            }

            if (Mathf.Sign(current) != Mathf.Sign(latest))
            {
                return true;
            }

            return false;
        }

        public bool MoveStepChanged
        {
            get
            {
                if (!IsMoveInputDown) return false;


                if (AxisChanged(m_LatestMoveValue.x, m_MoveValue.x))
                {
                    return true;
                }

                if (AxisChanged(m_LatestMoveValue.y, m_MoveValue.y))
                {
                    return true;
                }

                return false;
            }
        }

        [ShowInInspector]
        public Vector2 MoveValue
        {
            get { return m_MoveValue; }
            set
            {
                m_LatestMoveValue = m_MoveValue;
                m_MoveValue = value;
            }
        }

        [ShowInInspector]
        public Vector2 InputRotation { get; private set; }


        [ShowInInspector]
        public bool InputInteraction { get; private set; }

        public InputMotionType InputMotionType;

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
                MoveValue = Vector3.zero;
                InputRotation = Vector3.zero;
                InputInteraction = false;
                InputJump = false;
            }
            else
            {
                switch (InputMotionType)
                {
                    case InputMotionType.Default:
                        MoveValue = m_InputMove?.Read() ?? Vector2.zero;
                        InputRotation = m_InputRotation?.Read() ?? Vector2.zero;
                        break;
                    case InputMotionType.OnlyW:
                        //Debug.Log("当前只能按W前进");
                        var yVal = Math.Clamp(m_InputMove.Read().y, 0, 1);
                        MoveValue = new Vector2(0, yVal);
                        InputRotation = Vector2.zero;
                        break;
                    default:
                        MoveValue = m_InputMove?.Read() ?? Vector2.zero;
                        InputRotation = m_InputRotation?.Read() ?? Vector2.zero;
                        break;
                }

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

