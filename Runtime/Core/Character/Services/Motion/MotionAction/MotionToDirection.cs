using UnityEngine;

namespace Pangoo.Core.Characters
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

    }
}