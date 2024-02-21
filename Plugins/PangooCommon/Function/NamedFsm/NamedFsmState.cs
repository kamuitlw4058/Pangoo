
using System;
using Sirenix.OdinInspector;

using UnityEngine;

namespace Pangoo.Common
{
    /// <summary>
    /// 有限状态机状态基类。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    public abstract class NamedFsmState<T> where T : class
    {
        public string Name;


        /// <summary>
        /// 获取有限状态机完整名称。
        /// </summary>

        [ShowInInspector]
        public string FullName
        {
            get
            {
                return new TypeNamePair(GetType(), Name).ToString();
            }
        }

        /// <summary>
        /// 初始化有限状态机状态基类的新实例。
        /// </summary>
        public NamedFsmState()
        {
        }

        /// <summary>
        /// 有限状态机状态初始化时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnInit(INamedFsm<T> fsm)
        {
        }

        /// <summary>
        /// 有限状态机状态进入时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnEnter(INamedFsm<T> fsm)
        {
        }

        /// <summary>
        /// 有限状态机状态轮询时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal virtual void OnUpdate(INamedFsm<T> fsm)
        {
        }

        /// <summary>
        /// 有限状态机状态离开时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        /// <param name="isShutdown">是否是关闭有限状态机时触发。</param>
        protected internal virtual void OnLeave(INamedFsm<T> fsm, bool isShutdown)
        {
        }

        /// <summary>
        /// 有限状态机状态销毁时调用。
        /// </summary>
        /// <param name="fsm">有限状态机引用。</param>
        protected internal virtual void OnDestroy(INamedFsm<T> fsm)
        {
        }



        protected void ChangedState(INamedFsm<T> fsm, string stateName)
        {
            NamedFsm<T> fsmImplement = (NamedFsm<T>)fsm;
            if (fsmImplement == null)
            {
                throw new Exception("FSM is invalid.");
            }


            fsmImplement.ChangeState(stateName);
        }
    }
}
