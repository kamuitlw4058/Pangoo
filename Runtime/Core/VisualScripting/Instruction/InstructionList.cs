using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using Sirenix.Utilities;
using System.Linq;

namespace Pangoo.Core.VisualScripting
{

    public class InstructionList : TPolymorphicList<Instruction>
    {

        private Instruction[] m_Instructions = Array.Empty<Instruction>();

        bool m_IsStopped = false;

        public bool IsRunning { get; private set; }
        public bool IsStopped
        {
            get
            {
                return m_IsStopped;
            }
            private set
            {
                m_IsStopped = value;
                if (m_IsStopped)
                {
                    for (int i = 0; i < m_Instructions.Length; i++)
                    {
                        if (m_Instructions[i] != null)
                        {
                            m_Instructions[i].IsCanceled = true;
                        }
                    }
                }
            }
        }

        public bool IsFinished { get; private set; }

        public int FinishedCount { get; private set; }

        public int RunningIndex { get; private set; }
        public int LastestRunningIndex { get; private set; }

        public bool IsCancelled => this.IsStopped;

        public override int Length => this.m_Instructions.Length;

        public event Action EventStartRunning;
        public event Action EventEndRunning;


        public event Action<int> EventRunInstruction;



        public InstructionList()
        {
            this.IsRunning = false;
            this.m_IsStopped = false;
            this.IsFinished = false;
            FinishedCount = 0;
        }

        public InstructionList(params Instruction[] instructions) : this()
        {
            this.m_Instructions = instructions;
        }


        public void Start(Args args, int fromIndex = 0)
        {

            this.IsRunning = true;
            this.IsStopped = false;
            this.RunningIndex = Math.Max(0, fromIndex);
            this.LastestRunningIndex = -1;

            this.EventStartRunning?.Invoke();

            var hasCoroutine = false;
            for (int i = 0; i < this.m_Instructions.Length; i++)
            {
                var instruction = m_Instructions[i];
                switch (instruction.InstructionType)
                {
                    case InstructionType.Immediate:
                        if (hasCoroutine)
                        {
                            Debug.LogError($"Immediate Instruction Is After Coroutine!!! Please Check InstructionList Config!");
                            this.IsStopped = true;
                            Finish();
                            return;
                        }
                        this.m_Instructions[i].RunImmediate(args);
                        break;
                    case InstructionType.Coroutine:
                        hasCoroutine = true;
                        this.m_Instructions[i].Schedule(args);
                        break;
                }

            }

            if (!hasCoroutine)
            {
                Finish();
            }
        }

        public void Finish()
        {
            this.IsRunning = false;
            this.EventEndRunning?.Invoke();
            this.IsFinished = true;
            this.FinishedCount += 1;
        }



        public void OnUpdate()
        {
            if (!this.IsRunning) return;

            if (this.IsCancelled)
            {
                this.IsStopped = true;
                Finish();
                return;
            }

            if (this.m_Instructions.Length == 0)
            {
                this.IsStopped = true;
                Finish();
                return;
            }

            while (RunInstruction()) { }
        }

        //运行Coroutin.
        public bool RunInstruction()
        {
            while ((this.m_Instructions[this.RunningIndex] == null
                || this.m_Instructions[RunningIndex].InstructionType == InstructionType.Immediate
                 && this.RunningIndex < this.m_Instructions.Length))
            {
                this.RunningIndex += 1;
            }

            // 异常处理。当上面人物已经都执行完了。并且没有可以执行的指令了。返回
            if (this.RunningIndex >= this.m_Instructions.Length)
            {
                Finish();
                return false;
            }

            if (this.LastestRunningIndex != RunningIndex)
            {
                EventRunInstruction?.Invoke(this.RunningIndex);
            }

            Instruction instruction = this.m_Instructions[this.RunningIndex];
            bool instructionFinished = !instruction.MoveNext();
            this.LastestRunningIndex = this.RunningIndex;
            InstructionResult result = instruction.Result;

            if (result.DontContinue)
            {
                Finish();
                return false;
            }

            if (!instructionFinished)
            {
                return false;
            }

            this.RunningIndex += result.NextInstruction;
            if (this.RunningIndex >= this.m_Instructions.Length)
            {
                Finish();
                return false;
            }

            return true;


        }

        public void Cancel()
        {
            this.IsStopped = true;
        }

        public Instruction Get(int index)
        {
            index = Mathf.Clamp(index, 0, this.Length - 1);
            return this.m_Instructions[index];
        }

    }
}