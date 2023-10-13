
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

        void UpdateTrigger()
        {

            m_TriggerEventInstance = ClassUtility.CreateInstance<TriggerEvent>(Row.TriggerType);
            if (m_TriggerEventInstance == null)
            {
                return;
            }
            m_TriggerEventInstance.Row = Row;
            m_TriggerEventInstance.LoadParamsFromJson(Row.Params);
            m_TriggerEventInstance.Instructions = GetInstructionList();
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
                    Save();
                }

            }
        }

        public IEnumerable InstructionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
        }

        public InstructionList GetInstructionList()
        {
            List<Instruction> instructions = new();


            foreach (var instructionId in InstructionIds)
            {
                var instructionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(instructionId);
                if (instructionRow == null || instructionRow.InstructionType == null)
                {
                    continue;
                }
                var instructionType = Utility.Assembly.GetType(instructionRow.InstructionType);
                if (instructionType == null)
                {
                    continue;
                }

                var InstructionInstance = Activator.CreateInstance(instructionType) as Instruction;
                InstructionInstance.LoadParams(instructionRow.Params);

                instructions.Add(InstructionInstance);
            }

            return new InstructionList(instructions.ToArray());
        }

        [Button("立即运行指令")]
        [PropertyOrder(10)]
        public void Run()
        {
            var instructionList = GetInstructionList();
            instructionList.Start(new Args(Row));
        }


    }
}
#endif