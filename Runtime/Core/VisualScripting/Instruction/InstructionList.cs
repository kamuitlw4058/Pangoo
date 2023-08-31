using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using Sirenix.Utilities;
using System.Linq;

namespace Pangoo.Core.VisualScripting{

    public class InstructionList : TPolymorphicList<Instruction> {

        private Instruction[] m_Instructions = Array.Empty<Instruction>();

        public bool IsRunning { get; private set; }
        public bool IsStopped { get; private set; }

        public bool IsFinished{get;private set;}

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
            this.IsStopped = false;
            this.IsFinished = false;
            
        }

        public InstructionList(params Instruction[] instructions) : this()
        {
            this.m_Instructions = instructions;
        }


        public void Start(Args args,int fromIndex = 0){

             this.IsRunning = true;
             this.IsStopped = false;
             this.RunningIndex = Math.Max(0, fromIndex);
             this.LastestRunningIndex = -1;
             for(int i =0;i < this.m_Instructions.Length ;i++){
                this.m_Instructions[i].Schedule(args);
             }
             
             this.EventStartRunning?.Invoke();
        }

        public void Finish(){
            Debug.Log("Finish");
            this.IsRunning = false;
            this.EventEndRunning?.Invoke();
            this.IsFinished = true;
        }



        public void OnUpdate(){
           if (!this.IsRunning) return;

            if (this.IsCancelled)
            {
                this.IsStopped = true;
                Finish();
                return;
            }

            if(this.m_Instructions.Length == 0){
                this.IsStopped = true;
                Finish();
                return;
            }
            while(this.m_Instructions[this.RunningIndex] == null){
                this.RunningIndex += 1;
            }
            if(this.LastestRunningIndex != RunningIndex){
                EventRunInstruction?.Invoke(this.RunningIndex);
            }
            Instruction instruction = this.m_Instructions[this.RunningIndex];
           
            bool nextInstruction = !instruction.MoveNext();
            InstructionResult result = instruction.Result;
            if (result.DontContinue)
            {
               Finish();
                return;
            }

            if(nextInstruction){
                this.RunningIndex += result.NextInstruction;
                if(this.RunningIndex >= this.m_Instructions.Length){
                    Finish();
                    return;
                }
            }



            this.LastestRunningIndex = this.RunningIndex;
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