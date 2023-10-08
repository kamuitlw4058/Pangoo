using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public abstract class Instruction : TPolymorphicItem<Instruction>, IEnumerator
    {
        private const int DEFAULT_NEXT_INSTRUCTION = 1;
        protected int NextInstruction { get; set; }

        public virtual InstructionType InstructionType => InstructionType.Immediate;


        // 使用栈来存储当前运行的迭代器
        private Stack<IEnumerator> stack = new Stack<IEnumerator>();

        public bool IsCanceled { get; set; }

        [HideInInspector]
        public InstructionResult Result;

        public Instruction()
        {
            NextInstruction = DEFAULT_NEXT_INSTRUCTION;
            Result = InstructionResult.Default;
        }

        public void Add(IEnumerator enumerator)
        {
            if (enumerator == null) return;
            stack.Push(enumerator);
        }

        public object Current // 获取当前对象，
        {
            get
            {
                if (stack.Count == 0) return null;
                return stack.Peek();
            }
        }

        public void Schedule(Args args)
        {
            stack.Clear();
            Add(Run(args));
        }




        protected virtual IEnumerator Run(Args args)
        {
            yield break;
        }

        public virtual void RunImmediate(Args args) { }

        protected void Stop()
        {
            this.NextInstruction = int.MaxValue;
        }

        // 迭代器迭代一次
        public bool MoveNext()
        {
            while (stack.Count > 0)
            {
                if (this.IsCanceled || this.NextInstruction == int.MaxValue)
                {
                    Result = InstructionResult.Stop;
                    return false;
                }

                IEnumerator enumerator = stack.Peek();
                // Debug.Log($"Current Before move IEnumerator:{enumerator.Current} :{stack.Count}");
                if (enumerator.MoveNext())
                {
                    if (enumerator.Current is IEnumerator)
                    {
                        // 如果是嵌套的迭代器，那么就入栈，依次进行迭代

                        Add((IEnumerator)enumerator.Current);// 开启嵌套的协程
                        // Debug.Log($"Add New IEnumerator:{enumerator.Current} :{stack.Count}");
                    }
                    // 其他类型的都等下一帧再次进行迭代
                    return true;
                }
                stack.Pop();
            }

            InstructionResult.JumpTo(this.NextInstruction);
            return false; // 当迭代完所有的迭代器后则认为结束，不能再移动到下一步
        }

        protected IEnumerator WaitTime(float duration, TimeMode time)
        {
            float startTime = time.Time;
            // Debug.Log($"Before While:{startTime}");
            while (!this.IsCanceled && time.Time < (startTime + duration))
            {
                // Debug.Log($"On While:{IsCanceled} time:{time.Time} :{(startTime + duration)}");
                yield return null;
            }
            // Debug.Log($"End While:{startTime}");
        }



        /// <summary>
        /// Suspends the execution until the supplied delegate evaluates to false.
        /// </summary>
        protected IEnumerator While(Func<bool> function)
        {
            while (function.Invoke()) yield return null;
        }

        /// <summary>
        /// Suspends the execution until the supplied delegate evaluates to true.
        /// </summary>
        protected IEnumerator Until(Func<bool> function)
        {
            while (!function.Invoke()) yield return null;
        }

        public void Reset() { }

        public abstract string ParamsString();
        public abstract void LoadParams(string instructionParams);
    }
}