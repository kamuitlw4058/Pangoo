
using System;
using System.Collections.Generic;

namespace Pangoo.Common
{
    /// <summary>
    /// 有限状态机接口。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    public interface INamedFsm<T> where T : class
    {
        /// <summary>
        /// 获取有限状态机名称。
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机完整名称。
        /// </summary>
        string FullName
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        T Owner
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机中状态的数量。
        /// </summary>
        int FsmStateCount
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        bool IsRunning
        {
            get;
        }

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        bool IsDestroyed
        {
            get;
        }

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        NamedFsmState<T> CurrentState
        {
            get;
        }

        /// <summary>
        /// 获取当前有限状态机状态持续时间。
        /// </summary>
        float CurrentStateTime
        {
            get;
        }



        void Start(string name);



        bool HasState(string stateFullName);

        NamedFsmState<T> LatestState { get; }



        NamedFsmState<T> GetState(string stateFullName);

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>有限状态机的所有状态。</returns>
        NamedFsmState<T>[] GetAllStates();

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <param name="results">有限状态机的所有状态。</param>
        void GetAllStates(List<NamedFsmState<T>> results);


        void Update();


        void Shutdown();


        void ChangeState(string stateFullName);

        float DeltaTime { get; }



    }
}
