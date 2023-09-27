#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using GameFramework;

namespace Pangoo
{
    [Serializable]
    public class TriggerDetailWrapper : ExcelTableRowDetailWrapper<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>
    {

        [ShowInInspector]
        [PropertyOrder(0)]
        public TriggerTypeEnum TriggerType
        {
            get
            {
                if (Row == null)
                {
                    return TriggerTypeEnum.Unknown;
                }

                if (Row.TriggerType == null)
                {
                    Row.TriggerType = string.Empty;
                }

                return Row.TriggerType.ToEnum<TriggerTypeEnum>(TriggerTypeEnum.Unknown);
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    switch (value)
                    {
                        case TriggerTypeEnum.Unknown:
                            Row.TriggerType = string.Empty;
                            break;
                        case TriggerTypeEnum.OnInteract:
                            Row.TriggerType = TriggerTypeEnum.OnInteract.ToString();
                            break;
                        default:
                            Row.TriggerType = value.ToString();
                            break;
                    }
                    Save();
                }

            }
        }


        [LabelText("指令Ids")]
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(1)]
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

        [Button("立即运行")]
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



            // foreach (var instructionId in InstructionIds)
            // {
            //     var instructionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(instructionId);
            //     instructions.Add(instruction.InstructionInstance);
            // }
            // Debug.Log($"Start Run Instruction:{instructions.Count}");
            var instructionList = new InstructionList(instructions.ToArray());
            instructionList.Start(new Args(Row));
        }


    }
}
#endif