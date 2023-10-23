using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public abstract class TriggerEvent
    {
        public const string SelfStr = "Self";

        [ShowInInspector]
        [HideInEditorMode]
        public bool Enabled { get; set; }

        public GameObject Parent { get; set; }


        public string[] Targets { get; set; }

        public TriggerTargetListProcessTypeEnum TargetType { get; set; }

        [ShowInInspector]
        public int TargetIndex { get; set; }

        public DynamicObject dynamicObject { get; set; }

        public event Action EventRunInstructionsStart;

        public event Action EventRunInstructionsEnd;


        public TriggerEventTable.TriggerEventRow Row { get; set; }

        [ShowInInspector]
        [HideInEditorMode]
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

        // [ShowInInspector]
        [ShowIf("@this.UseCondition")]
        [HideInEditorMode]
        public InstructionList FailInstructions { get; set; }

        public bool IsRuningRunInstructions
        {
            get
            {
                return RunInstructions?.IsRunning ?? false;
            }
        }
        public bool IsRuningFailInstructions
        {
            get
            {
                return FailInstructions?.IsRunning ?? false;
            }
        }

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

                RunInstructions.Start(args);
            }

        }

        public void OnFailedInvoke(Args args)
        {
            if (FailInstructions != null)
            {
                FailInstructions.Start(args);
            }

        }

        public virtual void OnUpdate()
        {

            RunInstructions?.OnUpdate();
            FailInstructions?.OnUpdate();
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