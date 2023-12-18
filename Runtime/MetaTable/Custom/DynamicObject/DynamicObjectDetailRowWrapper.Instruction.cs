#if UNITY_EDITOR

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    public partial class DynamicObjectDetailRowWrapper : MetaTableDetailRowWrapper<DynamicObjectOverview, UnityDynamicObjectRow>
    {
        [LabelText("触发器Ids")]
        [TabGroup("指令系统")]
        [ValueDropdown("TriggerEventIdDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(11)]
        public int[] TriggerIds
        {
            get
            {
                return UnityRow.Row.TriggerEventIds.ToSplitArr<int>();
            }
            set
            {


                UnityRow.Row.TriggerEventIds = value.ToListString();
                Save();

            }
        }

        [LabelText("触发器Uuids")]
        [TabGroup("指令系统")]
        [ValueDropdown("TriggerEventUuidDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(11)]

        public string[] TriggerEventUuids
        {
            get
            {
                return UnityRow.Row.TriggerEventUuids.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.TriggerEventUuids = value.ToListString();
                Save();
            }
        }




        public IEnumerable TriggerEventIdDropdown()
        {
            return GameSupportEditorUtility.GetTriggerEventIds();
        }

        public IEnumerable TriggerEventUuidDropdown()
        {
            return GameSupportEditorUtility.GetTriggerEventUuids();
        }

        [ListDrawerSettings(Expanded = true)]
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
                            group.DirectInstructionList[i].SetPrefab(Prefab);
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
                    m_DirectInstructionGroups = DirectInstructionGroup.CreateList(UnityRow.Row?.DirectInstructions);
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
            if (!currentValue.Equals(UnityRow.Row?.DirectInstructions))
            {
                UnityRow.Row.DirectInstructions = currentValue;
                Save();
            }
            UpdateGroupPrefab();
        }





    }
}
#endif

