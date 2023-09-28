
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

                if (m_TriggerEventInstace == null)
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

        TriggerEvent m_TriggerEventInstace;

        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        public TriggerEvent TriggerEventInstace
        {
            get
            {
                return m_TriggerEventInstace;
            }
            set
            {
                m_TriggerEventInstace = value;
            }
        }

        void UpdateTrigger()
        {
            var triggerType = Utility.Assembly.GetType(Row.TriggerType);
            if (triggerType == null)
            {
                return;
            }

            m_TriggerEventInstace = Activator.CreateInstance(triggerType) as TriggerEvent;
            m_TriggerEventInstace.LoadParamsFromJson(Row.Params);
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
            Params = m_TriggerEventInstace.ParamsToJson();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            m_TriggerEventInstace.LoadParamsFromJson(Params);
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

        [Button("立即运行指令")]
        [PropertyOrder(10)]
        public void Run()
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

            var instructionList = new InstructionList(instructions.ToArray());
            instructionList.Start(new Args(Row));
        }


    }
}
#endif