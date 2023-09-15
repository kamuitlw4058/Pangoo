using System;

namespace Pangoo.Core.Character
{

    [Serializable]
    public struct MotionInfo
    {
        public float LinearSpeed;
        public float AngularSpeed;
        // AnimFloat StandLevel { get; }

        public float Mass;
        public float Height;
        public float Radius;

        public bool UseAcceleration;
        public float Acceleration;
        public float Deceleration;

        public bool CanJump;
        public int AirJumps;
        public bool IsJumping;
        public float IsJumpingForce;

        public float GravityUpwards;
        public float GravityDownwards;
        public float TerminalVelocity;

        public float JumpForce;
        public float JumpCooldown;

        public int DashInSuccession;
        public bool DashInAir;
        public float DashCooldown;

        public float InteractionRadius;

        // InteractionMode InteractionMode { get; set; }
    }
}