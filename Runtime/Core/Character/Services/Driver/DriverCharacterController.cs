using System;
using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;


namespace Pangoo.Core.Character
{

    public class DriverCharacterController : CharacterBaseService
    {

        public Vector3 MoveDirection { get; set; }

        public DriverCharacterController(INestedService parent) : base(parent)
        {

        }

        [ShowInInspector, ReadOnly]
        CharacterController m_Controller;

        MotionActionService m_MotionActionService;


        [NonSerialized] protected float m_VerticalSpeed;

        [ShowInInspector]
        public bool ControllerEnable
        {
            get
            {
                if (m_Controller == null)
                {
                    return false;
                }
                return m_Controller.enabled;
            }
        }

        public Vector3 ControllerMoveDirection;
        public override void DoStart()
        {
            m_MotionActionService = Character.MotionAction;
        }


        public override void DoAwake(INestedService parent)
        {
            base.DoAwake(parent);

            this.m_Controller = Character.gameObject.GetComponent<CharacterController>();
            if (this.m_Controller == null)
            {
                GameObject instance = Character.gameObject;
                this.m_Controller = instance.AddComponent<CharacterController>();
                this.m_Controller.hideFlags = HideFlags.HideInInspector;
            }
        }

        public override void DoUpdate()
        {
            // Debug.Log($"Update Services:{Services}");
            // MoveDirection = Services.GetVariable<Vector3>("MoveDirection");
            MoveDirection = m_MotionActionService.MoveDirection * Character.MotionInfo.LinearSpeed * DeltaTime;
            // Debug.Log($"Update MoveDirection:{MoveDirection}");
            // if (this.Character.IsDead) return;
            // if (this.m_Controller == null) return;

            // this.UpdateProperties();
            ControllerMoveDirection = Vector3.zero;

            UpdateGravity();
            // this.UpdateGravity(this.Character.Motion);
            // this.UpdateJump(this.Character.Motion);

            // this.UpdateTranslation(this.Character.Motion);
            // this.m_Axonometry?.ProcessPosition(this, this.Transform.position);

            if (this.m_Controller.enabled)
            {
                ControllerMoveDirection = MoveDirection + new Vector3(0, m_VerticalSpeed, 0);
            }

            if (ControllerMoveDirection != Vector3.zero)
            {
                m_Controller.Move(ControllerMoveDirection);
            }
            // SetVariable<Vector3>("MoveDirection", Vector3.zero);
        }

        protected void UpdateGravity()
        {
            // TODO 相关代码保留。这边是用来区分是在上升期还是下降期用不同的中立加速度
            // float gravity = this.WorldMoveDirection.y >= 0f
            //     ? Character.MotionInfo.GravityUpwards
            //     : Character.MotionInfo.GravityDownwards;

            float gravity = Character.MotionInfo.GravityDownwards;

            // 额外的重力影响机制
            // gravity *= this.GravityInfluence;

            this.m_VerticalSpeed += gravity * this.Character.DeltaTime;

            if (this.m_Controller.isGrounded)
            {
                // if (this.Character.Time - this.m_GroundTime > COYOTE_TIME &&
                //     this.Character.Time.Frame - this.m_GroundFrame > COYOTE_FRAMES)
                // {
                //     this.Character.OnLand(this.m_VerticalSpeed);
                // }

                // this.m_GroundTime = this.Character.Time.Time;
                // this.m_GroundFrame = this.Character.Time.Frame;

                this.m_VerticalSpeed = Mathf.Max(
                    this.m_VerticalSpeed, gravity
                );
            }

            this.m_VerticalSpeed = Mathf.Max(
                this.m_VerticalSpeed,
                Character.MotionInfo.TerminalVelocity
            );
        }



        public override void DoDestroy()
        {
            UnityEngine.Object.Destroy(this.m_Controller);
            base.DoDestroy();
        }
    }

}

