using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Common/WaitMsg")]
    [Serializable]
    public class InstructionWaitMsg : Instruction
    {
        [SerializeField] [LabelText("参数")] [HideReferenceObjectPicker]
        public InstructionWaitMsgParams ParamsRaw = new InstructionWaitMsgParams();
        public override IParams Params { get; }
        public override InstructionType InstructionType => InstructionType.Coroutine;
        
        bool flag = false;
        protected override IEnumerator Run(Args args)
        {
            flag = false;
            PangooEntry.Event.Subscribe(EventTriggerEventArgs.EventId, OnEventTrigger);
            while (!flag)
            {
                yield return null;
            }
            
            PangooEntry.Event.Unsubscribe(EventTriggerEventArgs.EventId, OnEventTrigger);
        }

        public override void RunImmediate(Args args)
        {
            
        }

        private void OnEventTrigger(object sender, GameEventArgs e)
        {
            var args = e as EventTriggerEventArgs;
            if (args.ConditionString.Equals(ParamsRaw.ConditionString))
            {
                flag = true;
            }
        }
    }
}

