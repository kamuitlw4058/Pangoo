using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class PlayerDirectionalService : CharacterBaseService
    {
        [SerializeReference]
        public InputValueVector2Base m_InputMove;

        MotionActionService m_MotionActionService;
        CharacterCameraService m_CharacterCameraService;

        PlayerService m_PlayerService;


        [ShowInInspector]
        public CharacterCameraService CameraService
        {
            get
            {
                return m_CharacterCameraService;
            }
        }



        public PlayerDirectionalService(INestedService parent) : base(parent)
        {
            m_InputMove = InputValueVector2MotionPrimary.Create();
        }

        public override void DoAwake(INestedService parent)
        {
            base.DoAwake(parent);
            m_PlayerService = parent as PlayerService;
            m_InputMove.OnAwake();
        }

        public override void DoDestroy()
        {
            m_InputMove.OnDestroy();
            base.DoDestroy();
        }


        public override void DoEnable()
        {
            base.DoEnable();
            this.m_InputMove.Active = true;

        }

        public override void DoStart()
        {
            m_MotionActionService = Character.GetService<MotionActionService>();
            m_CharacterCameraService = Character.GetService<CharacterCameraService>();
            // Debug.Log($"PlayerDirection Start! m_CharacterCameraService:{m_CharacterCameraService} Parent:{Parent}");
        }

        public override void DoUpdate()
        {
            base.DoUpdate();
            this.m_InputMove.OnUpdate();


            if (!this.Character.IsPlayer) return;
            Vector3 inputMovement = m_PlayerService.IsControllable
                ? this.m_InputMove.Read()
                : Vector2.zero;

            if (inputMovement == Vector3.zero) return;

            m_PlayerService.InputDirection = this.GetMoveDirection(inputMovement);
            if (m_PlayerService.InputDirection != Vector3.zero)
            {
                // Debug.Log($"InputDirection:{m_PlayerService.InputDirection}");
                m_MotionActionService?.MoveToDirection(m_PlayerService.InputDirection, Space.World, 0);
            }

        }

        protected virtual Vector3 GetMoveDirection(Vector3 input)
        {
            Vector3 direction = new Vector3(input.x, 0f, input.y);
            // Debug.Log($"direction:{direction}");

            Vector3 moveDirection = m_CharacterCameraService.CameraTransform != null
                ? m_CharacterCameraService.CameraTransform.TransformDirection(direction)
                : Vector3.zero;

            moveDirection.Scale(new Vector3(1, 0, 1));
            moveDirection.Normalize();
            // Debug.Log($"end direction:{moveDirection}");

            return moveDirection;
        }


        public override void DoDisable()
        {
            base.DoDisable();
            this.m_InputMove.Active = true;

            // this.Character.Motion?.MoveToDirection(Vector3.zero, Space.World, 0);
        }



    }

}

