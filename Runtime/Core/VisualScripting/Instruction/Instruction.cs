using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;

namespace Pangoo.Core.VisualScripting{

    [Serializable]
    public abstract class Instruction : TPolymorphicItem<Instruction>,IEnumerator {
        private const int DEFAULT_NEXT_INSTRUCTION = 1;
        protected int NextInstruction { get; set; }


        // 使用栈来存储当前运行的迭代器
        private Stack<IEnumerator> stack = new Stack<IEnumerator>();

         public bool IsCanceled { get; set;}

        [HideInInspector]
         public InstructionResult Result;

        public Instruction(){
            NextInstruction = DEFAULT_NEXT_INSTRUCTION;
            Result = InstructionResult.Default;
        }

        public void Add(IEnumerator enumerator) {
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
 


        protected abstract IEnumerator Run(Args args);

        protected void Stop(){
            this.NextInstruction = int.MaxValue;
        }

        // 迭代器迭代一次
        public bool MoveNext() {
            while (stack.Count > 0) {
                if(this.IsCanceled || this.NextInstruction == int.MaxValue){
                    Result = InstructionResult.Stop;
                    return false;
                }

                IEnumerator enumerator = stack.Peek();
                if (enumerator.MoveNext()) {
                    if (enumerator.Current is IEnumerator) {
                        // 如果是嵌套的迭代器，那么就入栈，依次进行迭代
                        Add((IEnumerator)enumerator.Current);// 开启嵌套的协程
                    }
                    // 其他类型的都等下一帧再次进行迭代
                    return true;
                }
                stack.Pop();
            }
            
            InstructionResult.JumpTo(this.NextInstruction);
            return false; // 当迭代完所有的迭代器后则认为结束，不能再移动到下一步
        }

        /// <summary>
        /// Suspends the execution until the supplied delegate evaluates to false.
        /// </summary>
        protected IEnumerable While(Func<bool> function)
        {
            while (function.Invoke()) yield return null;
        }

        /// <summary>
        /// Suspends the execution until the supplied delegate evaluates to true.
        /// </summary>
        protected IEnumerable Until(Func<bool> function)
        {
            while ( !function.Invoke()) yield return null;
        }

        public void Reset() { }

        public abstract string ParamsString();
        public abstract void LoadParams(string instructionParams);
    }
}