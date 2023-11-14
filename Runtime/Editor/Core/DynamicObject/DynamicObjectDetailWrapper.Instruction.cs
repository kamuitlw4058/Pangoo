#if UNITY_EDITOR
using System.Collections.Generic;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;



namespace Pangoo
{
    public partial class DynamicObjectDetailWrapper
    {

        [LabelText("触发器Ids")]
        [TabGroup("指令系统")]
        [ValueDropdown("TriggerIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(11)]
        public int[] TriggerIds
        {
            get
            {
                return Row?.TriggerEventIds?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.TriggerEventIds = value.ToListString();
                    Save();
                    // BuildTriggers();
                }

            }
        }

        // public void BuildTriggers()
        // {
        //     m_Triggers.Clear();
        //     foreach (var trigger in TriggerIds)
        //     {
        //         var wrapper = new TriggerDetailWrapper();
        //         wrapper.Id = trigger;
        //         var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<TriggerEventTableOverview>(trigger);
        //         var row = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>(trigger);
        //         wrapper.Overview = overview;
        //         wrapper.Row = row;
        //         Triggers.Add(wrapper);
        //     }
        // }

        // List<TriggerDetailWrapper> m_Triggers;

        // [ShowInInspector]
        // [LabelText("触发器实现")]
        // [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        // [PropertyOrder(5)]
        // [HideReferenceObjectPicker]
        // public List<TriggerDetailWrapper> Triggers
        // {
        //     get
        //     {
        //         if (m_Triggers == null)
        //         {
        //             m_Triggers = new List<TriggerDetailWrapper>();
        //             BuildTriggers();
        //         }
        //         return m_Triggers;
        //     }
        //     set
        //     {
        //         m_Triggers = value;
        //         BuildTriggers();
        //     }
        // }


        public IEnumerable TriggerIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<TriggerEventTableOverview>();
        }


        List<DirectInstructionGroup> m_DirectInstructionGroups;

        public void UpdateGroupPrefab()
        {
            if (m_DirectInstructionGroups != null)
            {
                foreach (var group in m_DirectInstructionGroups)
                {
                    if (group.DirectInstructionList != null)
                    {
                        for (int i = 0; i < group.DirectInstructionList.Length; i++)
                        {
                            group.DirectInstructionList[i].SetPrefab(AssetPrefab);
                        }
                    }
                }
            }

        }

        [ShowInInspector]

        [LabelText("直接指令")]
        [TabGroup("指令系统")]
        [PropertyOrder(10)]
        [OnValueChanged("OnDirectInstructionsChanged", includeChildren: true)]
        // [ListDrawerSettings(Expanded = true, CustomAddFunction = "AddDirectInstruction", CustomRemoveIndexFunction = "RemoveIndexDirectInstruction")]
        public List<DirectInstructionGroup> DirectInstructions
        {
            get
            {
                if (m_DirectInstructionGroups == null)
                {
                    m_DirectInstructionGroups = DirectInstructionGroup.CreateList(Row?.DirectInstructions);
                    if (m_DirectInstructionGroups == null)
                    {
                        m_DirectInstructionGroups = new List<DirectInstructionGroup>();
                    }
                    else
                    {
                        UpdateGroupPrefab();
                    }
                }

                return m_DirectInstructionGroups;
            }
            set
            {
                m_DirectInstructionGroups = value;
                // Debug.Log($"Set DirectInstructions");
            }

        }

        void OnDirectInstructionsChanged()
        {
            var currentValue = DirectInstructionGroup.Save(m_DirectInstructionGroups);
            // Debug.Log($"Try Save:{currentValue}, old:{Row?.DirectInstructions}");
            if (!currentValue.Equals(Row?.DirectInstructions))
            {
                Row.DirectInstructions = currentValue;
                Save();
            }
            UpdateGroupPrefab();
        }




    }
}
#endif