using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    // [Serializable]
    public class TriggerEvent
    {
        public const string SelfStr = ConstString.Self;

        [ShowInInspector]
        public TriggerEventFilter Filter { get; set; }

        bool m_Enabled;

        [ShowInInspector]
        [HideInEditorMode]
        public bool Enabled
        {
            get
            {
                return m_Enabled;
            }
            set
            {
                Log($"Set Trigger:{Row?.Uuid} {value}");
                dynamicObject.Variables.SetTriggerEnabled(Row.Uuid, value);
                m_Enabled = value;
            }
        }

        [ShowInInspector]
        public string Name
        {
            get
            {
                return Row.Name;
            }
        }

        public void SetEnabled(bool val)
        {
            m_Enabled = val;
        }

        public GameObject Parent { get; set; }


        public int TriggerCount;


        [ShowInInspector]
        public string[] Targets
        {
            get
            {
                if (Row?.Targets?.IsNullOrWhiteSpace() ?? true)
                {
                    return null;
                }

                return Row?.Targets?.Split("|");
            }
        }

        [ShowInInspector]
        public string CurrentTargetPath { get; set; }

        [ShowInInspector]
        public TriggerTargetListProcessTypeEnum TargetType
        {
            get
            {
                return (TriggerTargetListProcessTypeEnum)Row?.TargetListType;
            }
        }

        public int m_TargetIndex;

        [ShowInInspector]
        public int TargetIndex
        {
            get
            {
                return m_TargetIndex;
            }
            set
            {
                Debug.Log($"Set TargetIndex:{value}");
                dynamicObject.Variables.SetTriggerIndex(Row.Uuid, value);
                m_TargetIndex = value;
            }
        }

        public void SetTargetIndex(int index)
        {
            m_TargetIndex = index;
        }

        public DynamicObject dynamicObject { get; set; }

        public event Action EventRunInstructionsStart;

        public event Action EventRunInstructionsEnd;


        public ITriggerEventRow Row { get; set; }


        [ShowInInspector]
        [HideInEditorMode]
        public Dictionary<int, InstructionList> ConditionInstructions { get; private set; } = new Dictionary<int, InstructionList>();

        public bool UseCondition
        {
            get
            {
                return ConditionType != ConditionTypeEnum.NoCondition;
            }
        }

        [ShowInInspector]
        [ShowIf("@this.UseCondition")]
        public ConditionList Conditions { get; set; }

        [ShowInInspector]
        [LabelText("是否条件触发")]
        public ConditionTypeEnum ConditionType
        {
            get
            {
                return Row?.ConditionType.ToEnum<ConditionTypeEnum>() ?? ConditionTypeEnum.NoCondition;
            }
            set
            {
                Row.ConditionType = value.ToString();
            }
        }



        public bool IsRunning
        {
            get
            {
                foreach (var kv in ConditionInstructions)
                {
                    if (kv.Value.IsRunning)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool IsStoped
        {
            get
            {
                foreach (var kv in ConditionInstructions)
                {
                    if (kv.Value.IsStopped)
                    {
                        return true;
                    }
                }
                return false;
            }
            set
            {
                foreach (var kv in ConditionInstructions)
                {
                    kv.Value.IsStopped = value;
                }
            }
        }

        [ShowInInspector]
        [LabelText("触发点类型")]
        public virtual TriggerTypeEnum TriggerType
        {
            get
            {
                return Row?.TriggerType.ToEnum<TriggerTypeEnum>() ?? TriggerTypeEnum.Unknown;
            }
        }

        public void Log(string message)
        {
            if (TriggerType != TriggerTypeEnum.OnUpdate)
            {
                dynamicObject?.Log($"T:{Row.Name}[{Row.UuidShort}]{message}");
            }
        }

        public void LogError(string message)
        {
            if (TriggerType != TriggerTypeEnum.OnUpdate)
            {
                dynamicObject?.LogError($"T[{Row.UuidShort}]{message}");
            }
        }




        public virtual void OnAwake()
        {

        }

        public virtual void OnInvoke(Args args)
        {
            var invokeArgs = args.Clone;
            TriggerCount += 1;
            if (Targets == null || Targets.Length == 0)
            {
                invokeArgs.ChangeTarget(Parent);
            }
            else
            {
                TargetIndex = TargetIndex % Targets.Length;
                CurrentTargetPath = Targets[TargetIndex];
                Log($"Run Conditon: TargetIndex:{TargetIndex} CurrentTargetPath:{CurrentTargetPath}");
                if (CurrentTargetPath == SelfStr)
                {
                    invokeArgs.ChangeTarget(Parent, path: CurrentTargetPath, index: TargetIndex);
                }
                else
                {
                    var trans = dynamicObject.CachedTransfrom.Find(CurrentTargetPath);
                    if (trans != null)
                    {
                        invokeArgs.ChangeTarget(trans.gameObject, path: CurrentTargetPath, index: TargetIndex);
                    }
                    else
                    {
                        invokeArgs.ChangeTarget(Parent, path: CurrentTargetPath, index: TargetIndex);
                    }
                }

                TargetIndex += 1;
                switch (TargetType)
                {
                    case TriggerTargetListProcessTypeEnum.SeqAndDisabled:
                        if (TargetIndex >= Targets.Length)
                        {
                            Enabled = false;
                        }
                        break;
                }

            }

            switch (ConditionType)
            {
                case ConditionTypeEnum.NoCondition:
                    Log("No Condition Invoke!");
                    OnStateInvoke(1, invokeArgs);
                    break;
                case ConditionTypeEnum.BoolCondition:
                    var isPass = Conditions?.Check(invokeArgs) ?? false ? 1 : 0;
                    Log($"Check Pass:{isPass}");
                    OnStateInvoke(isPass, invokeArgs);
                    break;
                case ConditionTypeEnum.StateCondition:
                    var state = Conditions?.GetState(invokeArgs) ?? 1;
                    Log($"Check State:{state}");
                    OnStateInvoke(state, invokeArgs);
                    break;

            }




        }

        void OnRunInstructionsStart()
        {
            //Debug.Log($"Start RunInstructions:{EventRunInstructionsStart}. {EventRunInstructionsStart?.GetInvocationList()?.Length}");
            EventRunInstructionsStart?.Invoke();
        }

        void OnRunInstructionsEnd()
        {
            //  Debug.Log("End RunInstructions");
            EventRunInstructionsEnd?.Invoke();
        }

        public void OnStateInvoke(int state, Args args)
        {
            if (ConditionInstructions.TryGetValue(state, out InstructionList instructionList))
            {
                Log($"state on invoke:{state}");
                instructionList.EventStartRunning -= OnRunInstructionsStart;
                instructionList.EventStartRunning += OnRunInstructionsStart;


                instructionList.EventEndRunning -= OnRunInstructionsEnd;
                instructionList.EventEndRunning += OnRunInstructionsEnd;

                instructionList.Start(args);
            }
            else
            {
                if (TriggerType == TriggerTypeEnum.OnInteract)
                {
                    OnRunInstructionsEnd();
                }
                //LogError($"Invaild State on invoke:{state}");
            }

        }



        public virtual void OnUpdate()
        {
            foreach (var kv in ConditionInstructions)
            {
                kv.Value?.OnUpdate();
            }
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