
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using GameFramework;


using UnityEditor;
using Sirenix.OdinInspector.Editor;



namespace Pangoo
{
    [Serializable]
    public class TriggerDetailWrapper : ExcelTableRowDetailWrapper<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>
    {

        [ShowInInspector]
        [PropertyOrder(0)]
        [LabelText("触发类型")]
        [ValueDropdown("GetTriggerEvent")]
        public string TriggerType
        {
            get
            {

                if (m_TriggerEventInstance == null)
                {
                    UpdateTrigger();
                }

                return Row?.TriggerType;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.TriggerType = value;
                    Row.Params = "{}";
                    Save();
                    UpdateTrigger();
                }

            }
        }

        TriggerEvent m_TriggerEventInstance;

        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        public TriggerEvent TriggerEventInstance
        {
            get
            {
                if (m_TriggerEventInstance == null)
                {
                    UpdateTrigger();
                }

                return m_TriggerEventInstance;
            }
            set
            {
                m_TriggerEventInstance = value;
            }
        }

        [ShowInInspector]
        [LabelText("是否默认打开")]
        public bool Enabled
        {
            get
            {


                return Row?.Enabled ?? false;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.Enabled = value;
                    Save();
                }

            }
        }

        bool UseCondition
        {
            get
            {
                return Row?.UseCondition ?? false;
            }
        }


        void UpdateTrigger()
        {

            m_TriggerEventInstance = ClassUtility.CreateInstance<TriggerEvent>(Row.TriggerType);
            if (m_TriggerEventInstance == null)
            {
                return;
            }
            m_TriggerEventInstance.Row = Row;
            m_TriggerEventInstance.LoadParamsFromJson(Row.Params);
            m_TriggerEventInstance.RunInstructions = GetInstructionList(InstructionIds);
            m_TriggerEventInstance.FailInstructions = GetInstructionList(FailedInstructionIds);
            m_TriggerEventInstance.Conditions = GetConditionList(ConditionIds);
        }

        public IEnumerable GetTriggerEvent()
        {
            return GameSupportEditorUtility.GetTriggerEvent();
        }

        [ShowInInspector]
        [ReadOnly]
        public string Params
        {
            get
            {
                return Row?.Params;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.Params = value;
                    Save();
                }
            }
        }

        [Button("保存参数")]
        [TableColumnWidth(80, resizable: false)]
        public void SaveParams()
        {
            Params = m_TriggerEventInstance.ParamsToJson();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            m_TriggerEventInstance.LoadParamsFromJson(Params);
        }



        [LabelText("指令Ids")]
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(9)]
        public int[] InstructionIds
        {
            get
            {
                return Row?.InstructionList?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.InstructionList = value.ToList().ToListString();
                    UpdateTrigger();
                    Save();
                }

            }
        }

        [LabelText("条件Ids")]
        [ValueDropdown("ConditionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(10)]
        [ShowIf("@this.UseCondition")]
        public int[] ConditionIds
        {
            get
            {
                return Row?.ConditionList?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.ConditionList = value.ToList().ToListString();
                    UpdateTrigger();
                    Save();
                }

            }
        }



        [LabelText("失败指令Ids")]
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(10)]
        [ShowIf("@this.UseCondition")]
        public int[] FailedInstructionIds
        {
            get
            {
                return Row?.FailInstructionList?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.FailInstructionList = value.ToList().ToListString();
                    UpdateTrigger();
                    Save();
                }

            }
        }


        public IEnumerable InstructionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
        }

        public IEnumerable ConditionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<ConditionTableOverview>();
        }

        public InstructionList GetInstructionList(int[] ids)
        {
            if (ids.Length == 0)
            {
                return null;
            }

            List<Instruction> instructions = new();

            foreach (var instructionId in ids)
            {
                var instructionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(instructionId);
                if (instructionRow == null || instructionRow.InstructionType == null)
                {
                    continue;
                }

                var InstructionInstance = ClassUtility.CreateInstance<Instruction>(instructionRow.InstructionType);
                if (InstructionInstance == null)
                {
                    continue;
                }


                InstructionInstance.LoadParams(instructionRow.Params);

                instructions.Add(InstructionInstance);
            }

            return new InstructionList(instructions.ToArray());
        }

        public ConditionList GetConditionList(int[] ids)
        {
            if (ids.Length == 0)
            {
                return null;
            }

            List<Condition> items = new();

            foreach (var itemId in ids)
            {
                var row = GameSupportEditorUtility.GetConditionRowById(itemId);
                if (row == null || row.ConditionType == null)
                {
                    continue;
                }

                var instance = ClassUtility.CreateInstance<Condition>(row.ConditionType);
                if (instance == null)
                {
                    continue;
                }


                instance.LoadParams(row.Params);
                items.Add(instance);
            }

            return new ConditionList(items.ToArray());
        }

        [Button("运行成功指令")]
        [PropertyOrder(10)]
        public void RunPass()
        {
            var instructionList = GetInstructionList(InstructionIds);
            instructionList.Start(new Args());
        }

        [Button("运行失败指令")]
        [PropertyOrder(10)]
        public void RunFailed()
        {
            var instructionList = GetInstructionList(FailedInstructionIds);
            instructionList.Start(new Args());
        }

        [Button("运行指令")]
        [PropertyOrder(10)]
        public void Run()
        {
            m_TriggerEventInstance?.OnInvoke(new Args());
        }



    }
}
#endif