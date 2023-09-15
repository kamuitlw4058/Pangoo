using UnityEngine;

namespace Pangoo.Core.Character
{
    public abstract class MotionActionBase
    {
        public MotionActionService Service { get; set; }

        public int Priority { get; set; }

        public MotionActionParams Params { get; set; }


        public MotionActionBase()
        {
            IsRunning = false;
        }


        // public MotionActionBase(MotionActionService service, MotionActionParams actionParams, int priority)
        // {
        //     Service = service;
        //     Priority = priority;
        //     Params = actionParams;
        //     // MovementType = MovementTypeEnum.None;
        //     IsRunning = false;
        // }


        public abstract void Update();

        public virtual void Stop()
        {
            this.Priority = -1;
            this.Service.MoveDirection = Vector3.zero;
            // this.Motion.MovePosition = this.Transform.position;
        }

        // public abstract MovementTypeEnum MovementType { get; protected set; }

        public bool IsRunning { get; protected set; }


        protected Vector3 CalculateSpeed(Vector3 direction)
        {
            direction = direction.normalized * Service.Character.MotionInfo.LinearSpeed * Service.DeltaTime;
            return direction;
        }

        // protected Vector3 CalculateAcceleration(Vector3 tarDirection)
        // {
        //     if (!Service.UseAcceleration) return tarDirection;

        //     // Vector3 curDirection = Service.Character.Motion.MoveDirection;

        //     if (tarDirection.sqrMagnitude < 0.01f) tarDirection = Vector3.zero;
        //     bool isIncreasing = curDirection.sqrMagnitude < tarDirection.sqrMagnitude;

        //     float acceleration = isIncreasing
        //         ? this.Motion.Acceleration
        //         : this.Motion.Deceleration;

        //     curDirection = Vector3.Lerp(
        //         curDirection,
        //         tarDirection,
        //         acceleration * this.Character.Time.DeltaTime
        //     );

        //     if (isIncreasing)
        //     {
        //         Vector3 projection = Vector3.Project(curDirection, tarDirection);
        //         curDirection = projection.sqrMagnitude < tarDirection.sqrMagnitude
        //             ? curDirection
        //             : tarDirection;
        //     }
        //     else
        //     {
        //         float curMagnitude = curDirection.sqrMagnitude;
        //         float tarMagnitude = tarDirection.sqrMagnitude;
        //         curDirection = Mathf.Abs(curMagnitude) > 0.01f || Mathf.Abs(tarMagnitude) > 0.01f
        //             ? curDirection
        //             : Vector3.zero;
        //     }

        //     return curDirection;
        // }

    }
}