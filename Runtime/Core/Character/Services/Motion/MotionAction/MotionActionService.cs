using System;
using FairyGUI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class MotionActionService : CharacterBaseService
    {
        MotionInfo m_MotionInfo;

        public MotionInfo Info
        {
            get
            {
                return m_MotionInfo;
            }
            set
            {
                m_MotionInfo = value;
            }
        }

        [ShowInInspector]
        public Vector3 MoveDirection { get; set; }


        public override int Priority
        {
            get
            {
                return 2;
            }
        }

        [ShowInInspector]
        public MotionActionBase CurrentAction { get; private set; }


        public override void DoUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (this.CurrentAction != null)
            {
                Debug.Log($"this.CurrentAction:{this.CurrentAction}");
            }
            MoveDirection = Vector3.zero;
            this.CurrentAction?.Update();

            if (this.CurrentAction != null)
            {
                if (!CurrentAction.IsRunning)
                {
                    CurrentAction = null;
                }
            }
        }




        public virtual void MoveToDirection(Vector3 velocity, Space space, int priority)
        {
            var actionParams = new MotionActionParams();
            actionParams.Velocity = velocity;
            actionParams.UseSpace = space;
            if (!this.StartMotionAction<MotionToDirection>(priority, actionParams)) return;
            Debug.Log($"MoveToDirection:{velocity}");

            // if (this.m_MotionData is MotionToDirection motion)
            // {
            //     this.m_MovementType = motion.Setup(velocity, space);
            // }
        }


        protected bool StartMotionAction<T>(int priority, MotionActionParams actionParams) where T : MotionActionBase, new()
        {
            if (this.CurrentAction != null && priority < this.CurrentAction?.Priority) return false;


            // if (this.CurrentAction is T)
            // {
            //     this.CurrentAction.Priority = priority;
            //     return true;
            // }
            CurrentAction?.Stop();
            var newAction = new T();
            newAction.Service = this;
            newAction.Priority = priority;
            newAction.Params = actionParams;
            CurrentAction = newAction;
            return true;
        }



    }

}

