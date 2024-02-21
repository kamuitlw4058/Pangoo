
using System;
using System.Collections.Generic;
using System.Linq;
using Pangoo.Common.Utility;
using Pangoo.Common.Alloc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Common
{
    /// <summary>
    /// 有限状态机。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    public class NamedFsm<T> : NamedFsmBase, IReference, INamedFsm<T> where T : class
    {
        private T m_Owner;
        private readonly Dictionary<string, NamedFsmState<T>> m_States;
        // private Dictionary<string, Variable> m_Datas;
        private NamedFsmState<T> m_CurrentState;

        private NamedFsmState<T> m_LatestState;
        private float m_CurrentStateTime;
        private bool m_IsDestroyed;

        /// <summary>
        /// 初始化有限状态机的新实例。
        /// </summary>
        public NamedFsm()
        {
            m_Owner = null;
            m_States = new Dictionary<string, NamedFsmState<T>>();
            // m_Datas = null;
            m_CurrentState = null;
            m_LatestState = null;
            m_CurrentStateTime = 0f;
            m_IsDestroyed = true;
        }

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        [ShowInInspector]
        public T Owner
        {
            get
            {
                return m_Owner;
            }
        }

        /// <summary>
        /// 获取有限状态机持有者类型。
        /// </summary>
        [ShowInInspector]
        public override Type OwnerType
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// 获取有限状态机中状态的数量。
        /// </summary>
        [ShowInInspector]
        public override int FsmStateCount
        {
            get
            {
                return m_States.Count;
            }
        }

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        [ShowInInspector]
        public override bool IsRunning
        {
            get
            {
                return m_CurrentState != null;
            }
        }

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        [ShowInInspector]

        public override bool IsDestroyed
        {
            get
            {
                return m_IsDestroyed;
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        [ShowInInspector]
        public NamedFsmState<T> CurrentState
        {
            get
            {
                return m_CurrentState;
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态名称。
        /// </summary>
        [ShowInInspector]
        public override string CurrentStateName
        {
            get
            {
                return m_CurrentState != null ? m_CurrentState.FullName : null;
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态持续时间。
        /// </summary>
        [ShowInInspector]
        public override float CurrentStateTime
        {
            get
            {
                return m_CurrentStateTime;
            }
        }

        public override float DeltaTime => Time.deltaTime;

        public NamedFsmState<T> LatestState => m_LatestState;

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="name">有限状态机名称。</param>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static NamedFsm<T> Create(string name, T owner, params NamedFsmState<T>[] states)
        {
            if (owner == null)
            {
                throw new PangooCommonException("FSM owner is invalid.");
            }

            if (states == null || states.Length < 1)
            {
                throw new PangooCommonException("FSM states is invalid.");
            }

            NamedFsm<T> fsm = ReferencePool.Acquire<NamedFsm<T>>();
            fsm.Name = name;
            fsm.m_Owner = owner;
            fsm.m_IsDestroyed = false;
            foreach (NamedFsmState<T> state in states)
            {
                if (state == null || state.FullName.IsNullOrWhiteSpace())
                {
                    throw new Exception("FSM states is invalid.");
                }

                if (fsm.m_States.ContainsKey(state.FullName))
                {
                    throw new Exception($"FSM '{new TypeNamePair(typeof(T), name)}' state '{state.FullName}' is already exist.");
                }

                fsm.m_States.Add(state.FullName, state);
                state.OnInit(fsm);
            }

            return fsm;
        }




        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="name">有限状态机名称。</param>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static NamedFsm<T> Create(string name, T owner, IEnumerable<NamedFsmState<T>> states)
        {
            return Create(name, owner, states.ToList().ToArray());
        }

        /// <summary>
        /// 清理有限状态机。
        /// </summary>
        public void Clear()
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.OnLeave(this, true);
            }

            foreach (var state in m_States)
            {
                state.Value.OnDestroy(this);
            }

            Name = null;
            m_Owner = null;
            m_States.Clear();

            // if (m_Datas != null)
            // {
            //     foreach (KeyValuePair<string, Variable> data in m_Datas)
            //     {
            //         if (data.Value == null)
            //         {
            //             continue;
            //         }

            //         ReferencePool.Release(data.Value);
            //     }

            //     m_Datas.Clear();
            // }

            m_CurrentState = null;
            m_CurrentStateTime = 0f;
            m_IsDestroyed = true;
        }


        public void Start(string stateFullName)
        {
            if (IsRunning)
            {
                throw new Exception("FSM is running, can not start again.");
            }


            NamedFsmState<T> state = GetState(stateFullName);
            if (state == null)
            {
                throw new Exception($"FSM '{new TypeNamePair(typeof(T), Name)}' can not start state '{state.FullName}' which is not exist.");
            }

            m_CurrentStateTime = 0f;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }

        public bool HasState(string stateFullName)
        {
            return m_States.ContainsKey(stateFullName);
        }



        public NamedFsmState<T> GetState(string stateName)
        {

            NamedFsmState<T> state = null;
            if (m_States.TryGetValue(stateName, out state))
            {
                return state;
            }

            return null;
        }

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>有限状态机的所有状态。</returns>
        public NamedFsmState<T>[] GetAllStates()
        {
            int index = 0;
            NamedFsmState<T>[] results = new NamedFsmState<T>[m_States.Count];
            foreach (var state in m_States)
            {
                results[index++] = state.Value;
            }

            return results;
        }

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <param name="results">有限状态机的所有状态。</param>
        public void GetAllStates(List<NamedFsmState<T>> results)
        {
            if (results == null)
            {
                throw new PangooCommonException("Results is invalid.");
            }

            results.Clear();
            foreach (var state in m_States)
            {
                results.Add(state.Value);
            }
        }



        /// <summary>
        /// 有限状态机轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public override void Update()
        {
            if (m_CurrentState == null)
            {
                return;
            }

            m_CurrentStateTime += DeltaTime;
            m_CurrentState.OnUpdate(this);
        }

        /// <summary>
        /// 关闭并清理有限状态机。
        /// </summary>
        public override void Shutdown()
        {
            ReferencePool.Release(this);
        }


        public void ChangeState(string stateFullName)
        {
            if (m_CurrentState == null)
            {
                throw new Exception("Current state is invalid.");
            }

            NamedFsmState<T> state = GetState(stateFullName);
            if (state == null)
            {
                throw new Exception($"FSM '{new TypeNamePair(typeof(T), Name)}' can not change state to '{state.FullName}' which is not exist.");
            }

            m_CurrentState.OnLeave(this, false);
            m_CurrentStateTime = 0f;
            m_LatestState = m_CurrentState;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }


    }
}
