using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class TriggerEvent
    {
        public const string SelfStr = "Self";

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

        [ShowInInspector]
        [HideInEditorMode]
        public bool IsDirectInstuction { get; set; } = false;


        [ShowInInspector]
        public string[] Targets
        {
            get
            {
                return Row?.Targets?.Split("|");
            }
        }

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
                dynamicObject?.Log($"T[{Row.UuidShort}]{message}");
            }
        }



        public virtual void OnAwake()
        {

        }

        public virtual void OnInvoke(Args args)
        {
            if (Targets == null || Targets.Length == 0)
            {
                args.ChangeTarget(Parent);
            }
            else
            {
                if (Targets.Length == 1)
                {
                    if (Targets[0] == SelfStr)
                    {
                        args.ChangeTarget(Parent);
                    }
                    else
                    {
                        var trans = dynamicObject.CachedTransfrom.Find(Targets[0]);
                        if (trans != null)
                        {
                            args.ChangeTarget(trans.gameObject, path: Targets[0]);
                        }
                        else
                        {
                            args.ChangeTarget(Parent, path: SelfStr);
                        }
                    }
                }
                else
                {
                    var targetStr = Targets[TargetIndex];
                    if (targetStr == SelfStr)
                    {
                        args.ChangeTarget(Parent, path: targetStr, index: TargetIndex);
                    }
                    else
                    {
                        var trans = dynamicObject.CachedTransfrom.Find(targetStr);
                        if (trans != null)
                        {
                            args.ChangeTarget(trans.gameObject, path: targetStr, index: TargetIndex);
                        }
                        else
                        {
                            args.ChangeTarget(Parent, path: targetStr, index: TargetIndex);
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
                        case TriggerTargetListProcessTypeEnum.Loop:
                            if (TargetIndex >= Targets.Length)
                            {
                                TargetIndex = 0;
                            }
                            break;
                    }

                }
            }

            switch (ConditionType)
            {
                case ConditionTypeEnum.NoCondition:
                    Log("No Condition Invoke!");
                    OnStateInvoke(1, args);
                    break;
                case ConditionTypeEnum.BoolCondition:
                    var isPass = Conditions?.Check(args) ?? false ? 1 : 0;
                    Log($"Trigger:[{Row.UuidShort}] Check Pass:{isPass} :{Conditions}");
                    OnStateInvoke(isPass, args);
                    break;
                case ConditionTypeEnum.StateCondition:
                    var state = Conditions?.GetState(args) ?? 1;
                    Log($"Check state:{state} :{Conditions}");
                    OnStateInvoke(state, args);
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