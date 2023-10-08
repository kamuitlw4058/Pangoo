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
        public TriggerEventTable.TriggerEventRow Row { get; set; }

        [ShowInInspector]
        public InstructionList RunInstructions { get; set; }

        [ShowInInspector]
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
            IsRuningRunInstructions = RunInstructions.Start(args);
        }

        public virtual void OnFailedInvoke(Args args)
        {
            IsRuningFailInstructions = FailInstructions.Start(args);
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
            return string.Empty;
        }

        [Button("立即运行指令列表")]
        public void Run()
        {
            if (RunInstructions != null)
            {
                RunInstructions.Start(new Args(null));
            }
        }
    }
}