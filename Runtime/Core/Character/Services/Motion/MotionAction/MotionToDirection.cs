using UnityEngine;

namespace Pangoo.Core.Character
{
    public class MotionToDirection : MotionActionBase
    {

        public override void Update()
        {
            this.Service.MoveDirection = Params.UseSpace switch
            {
                Space.World => Params.Velocity,
                Space.Self => Service.Transform.TransformDirection(Params.Velocity),
                _ => Service.MoveDirection
            };


            IsRunning = false;
        }

        // public MotionToDirection(MotionActionService service, MotionActionParams actionParams, int priority) : base(service, actionParams, priority)
        // {

        // }
    }
}