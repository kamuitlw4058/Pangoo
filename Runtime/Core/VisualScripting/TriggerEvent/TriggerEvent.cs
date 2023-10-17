using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public abstract class TriggerEvent : TPolymorphicItem<TriggerEvent>
    {
        public event Action EventRunInstructionsStart;

        public event Action EventRunInstructionsEnd;


        public TriggerEventTable.TriggerEventRow Row { get; set; }

        [ShowInInspector]
        public InstructionList RunInstructions { get; set; }

        public ConditionList Conditions { get; set; }

        [ShowInInspector]
        [LabelText("是否条件触发")]
        public bool UseCondition
        {
            get
            {
                return Row?.UseCondition ?? false;
            }
            set
            {
                Row.UseCondition = value;
            }
        }

        [ShowInInspector]
        [ShowIf("@this.UseCondition")]
        public InstructionList FailInstructions { get; set; }

        public bool IsRuningRunInstructions { get; private set; }
        public bool IsRuningFailInstructions { get; private set; }

        public bool IsRunning
        {
            get
            {
                return IsRuningRunInstructions || IsRuningFailInstructions;
            }
        }

        [ShowInInspector]
        [LabelText("触发点类型")]
        public virtual TriggerTypeEnum TriggerType => TriggerTypeEnum.Unknown;

        public virtual void OnAwake()
        {

        }
        // public virtual void OnEnable() { }
        // public virtual void OnDisable() { }

        public virtual bool CheckCondition(Args args)
        {
            return true;
        }

        public virtual void OnInvoke(Args args)
        {
            if (UseCondition && Conditions != null)
            {
                var isPass = Conditions.Check(args);
                Debug.Log($"Check Pass:{isPass}");
                if (isPass)
                {
                    OnPassInvoke(args);
                }
                else
                {
                    OnFailedInvoke(args);
                }
            }
            else
            {
                OnPassInvoke(args);
            }
        }

        void OnRunInstructionsStart()
        {
            Debug.Log("Start RunInstructions");
            EventRunInstructionsStart?.Invoke();
        }

        void OnRunInstructionsEnd()
        {
            Debug.Log("End RunInstructions");
            EventRunInstructionsEnd?.Invoke();
        }


        public void OnPassInvoke(Args args)
        {
            if (RunInstructions != null)
            {
                RunInstructions.EventStartRunning -= OnRunInstructionsStart;
                RunInstructions.EventStartRunning += OnRunInstructionsStart;


                RunInstructions.EventEndRunning -= OnRunInstructionsEnd;
                RunInstructions.EventEndRunning += OnRunInstructionsEnd;

                IsRuningRunInstructions = RunInstructions.Start(args);
            }
            else
            {
                IsRuningRunInstructions = false;
            }
        }

        public void OnFailedInvoke(Args args)
        {
            if (FailInstructions != null)
            {
                IsRuningFailInstructions = FailInstructions.Start(args);
            }
            else
            {
                IsRuningFailInstructions = false;
            }
        }

        public virtual void OnUpdate()
        {

            RunInstructions?.OnUpdate();
            FailInstructions?.OnUpdate();
            IsRuningRunInstructions = RunInstructions?.IsRunning ?? false;
            IsRuningFailInstructions = FailInstructions?.IsRunning ?? false;
        }

        public virtual void LoadParamsFromJson(string val) { }
        public virtual string ParamsToJson()
        {
            return "{}";
        }

        [Button("立即运行指令列表")]
        public void Run()
        {
            OnInvoke(new Args());
        }
    }
}