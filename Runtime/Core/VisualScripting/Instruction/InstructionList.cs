using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using Sirenix.Utilities;
using System.Linq;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionList : TPolymorphicList<Instruction>
    {

        Args LastestArgs { get; set; }

        [ShowInInspector]

        private Instruction[] m_Instructions = Array.Empty<Instruction>();

        bool m_IsStopped = false;

        [ShowInInspector]
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


        public int FinishedCount { get; private set; }

        [ShowInInspector]
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
            FinishedCount = 0;
        }

        public InstructionList(params Instruction[] instructions) : this()
        {
            this.m_Instructions = instructions;
        }


        public bool Start(Args args, int fromIndex = 0)
        {
            this.IsRunning = true;
            this.IsStopped = false;
            this.RunningIndex = Math.Max(0, fromIndex);
            this.LastestRunningIndex = -1;

            this.EventStartRunning?.Invoke();
            LastestArgs = args;

            var hasCoroutine = false;
            for (int i = 0; i < this.m_Instructions.Length; i++)
            {
                var instruction = m_Instructions[i];
                switch (instruction.InstructionType)
                {
                    case InstructionType.Immediate:
                        this.m_Instructions[i].RunImmediate(args);
                        break;
                    case InstructionType.Coroutine:
                        hasCoroutine = true;
                        this.RunningIndex = i;
                        // this.m_Instructions[i].Schedule(args);
                        break;
                }

                if (hasCoroutine)
                {
                    break;
                }

            }

            if (!hasCoroutine)
            {
                Finish();
            }

            return !IsRunning;
        }

        public void Finish()
        {
            this.IsRunning = false;
            this.FinishedCount += 1;
            this.EventEndRunning?.Invoke();
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
            while (this.m_Instructions[this.RunningIndex] == null
                 && this.RunningIndex < this.m_Instructions.Length)
            {
                this.RunningIndex += 1;
            }

            // 异常处理。当上面人物已经都执行完了。并且没有可以执行的指令了。返回
            if (this.RunningIndex >= this.m_Instructions.Length)
            {
                Finish();
                return false;
            }
            Instruction instruction = this.m_Instructions[this.RunningIndex];
            if (this.LastestRunningIndex != RunningIndex)
            {
                this.LastestRunningIndex = RunningIndex;
                EventRunInstruction?.Invoke(this.RunningIndex);
                if (instruction.InstructionType == InstructionType.Coroutine)
                {
                    instruction.Schedule(LastestArgs);
                }
            }

            bool waitNextFrame = false;


            switch (instruction.InstructionType)
            {
                case InstructionType.Immediate:
                    instruction.RunImmediate(LastestArgs);
                    break;
                case InstructionType.Coroutine:
                    waitNextFrame = instruction.MoveNext();
                    break;
                default:
                    Debug.LogError($"Unknonw InstructionType:{instruction.InstructionType}");
                    break;
            }

            InstructionResult result = instruction.Result;
            if (result.DontContinue)
            {
                Finish();
                return false;
            }

            if (waitNextFrame)
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

        public static InstructionList BuildInstructionList(TriggerEvent trigger, List<int> ids, InstructionTable table = null)
        {
            List<Instruction> instructions = new();

            foreach (var instructionId in ids)
            {
                InstructionTable.InstructionRow instructionRow = InstructionRowExtension.GetById(instructionId, table);
                if (instructionRow == null || instructionRow.InstructionType == null)
                {
                    continue;
                }

                var InstructionInstance = ClassUtility.CreateInstance<Instruction>(instructionRow.InstructionType);
                InstructionInstance.LoadParams(instructionRow.Params);
                InstructionInstance.Trigger = trigger;

                instructions.Add(InstructionInstance);
            }

            return new InstructionList(instructions.ToArray());
        }


    }
}