using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class PlayerDirectionalService : PlayerService
    {
        [SerializeReference]
        public InputValueVector2Base m_InputMove;

        MotionActionService m_MotionActionService;
        CharacterCameraService m_CharacterCameraService;


        public PlayerDirectionalService() : base()
        {
            m_InputMove = InputValueVector2MotionPrimary.Create();
        }

        public override void DoAwake(IServiceContainer services)
        {
            base.DoAwake(services);
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
            m_MotionActionService = Services.GetService<MotionActionService>();
            m_CharacterCameraService = Services.GetService<CharacterCameraService>();
        }

        public override void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.DoUpdate(elapseSeconds, realElapseSeconds);
            this.m_InputMove.OnUpdate();

            // this.InputDirection = Vector3.zero;

            if (!this.Character.IsPlayer) return;
            Vector3 inputMovement = this.m_IsControllable
                ? this.m_InputMove.Read()
                : Vector2.zero;

            this.InputDirection = this.GetMoveDirection(inputMovement);
            if (this.InputDirection != Vector3.zero)
            {
                Debug.Log($"InputDirection:{this.InputDirection}");
                m_MotionActionService?.MoveToDirection(this.InputDirection, Space.World, 0);
            }

        }

        protected virtual Vector3 GetMoveDirection(Vector3 input)
        {
            Vector3 direction = new Vector3(input.x, 0f, input.y);

            Vector3 moveDirection = m_CharacterCameraService.CameraTransform != null
                ? m_CharacterCameraService.CameraTransform.TransformDirection(direction)
                : Vector3.zero;

            direction.Scale(new Vector3(1, 0, 1));
            direction.Normalize();

            return direction;
        }


        public override void DoDisable()
        {
            base.DoDisable();
            this.m_InputMove.Active = true;

            // this.Character.Motion?.MoveToDirection(Vector3.zero, Space.World, 0);
        }



    }

}

