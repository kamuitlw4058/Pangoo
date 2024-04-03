using System;
using Sirenix.OdinInspector;

namespace Pangoo.Core.Characters
{

    [Serializable]
    public struct MotionInfo
    {

        public float LinearSpeed;
        public float RunSpeed;


        public float AngularSpeed;

        public float RotationSpeedX;
        public float RotationSpeedY;
        // AnimFloat StandLevel { get; }

        [HideInEditorMode]
        public float Mass;
        public float Height;
        public float Radius;
        [HideInEditorMode]
        public bool UseAcceleration;
        [HideInEditorMode]
        public float Acceleration;
        [HideInEditorMode]
        public float Deceleration;

        [HideInEditorMode]
        public bool CanJump;
        [HideInEditorMode]
        public int AirJumps;

        [HideInEditorMode]
        public bool IsJumping;

        [HideInEditorMode]
        public float IsJumpingForce;

        [HideInEditorMode]
        public float GravityUpwards;
        public float GravityDownwards;
        public float TerminalVelocity;

        [HideInEditorMode]
        public float JumpForce;

        [HideInEditorMode]
        public float JumpCooldown;

        [HideInEditorMode]
        public int DashInSuccession;

        [HideInEditorMode]
        public bool DashInAir;

        [HideInEditorMode]
        public float DashCooldown;

        public float InteractionRadius;

        public void SetLinearSpeed(float val)
        {
            LinearSpeed = val;
        }
        
        public void SetRunSpeed(float val)
        {
            RunSpeed = val;
        }

        // InteractionMode InteractionMode { get; set; }
    }
}