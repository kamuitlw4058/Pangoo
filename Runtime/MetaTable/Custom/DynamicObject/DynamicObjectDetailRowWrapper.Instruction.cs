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

        [LabelText("碰撞体触发停留超时")]
        [FoldoutGroup("指令系统")]
        [PropertyOrder(10)]
        [ShowInInspector]
        [InfoBox("停留超时事件只会在停留超时设置在0.1以上发生", InfoMessageType.Warning, "@ColliderTriggerStayTimeout < 0.1f")]
        public float ColliderTriggerStayTimeout
        {
            get
            {

                return UnityRow.Row.ColliderTriggerStayTimeout;
            }
            set
            {
                UnityRow.Row.ColliderTriggerStayTimeout = value;
                Save();
            }
        }

        [LabelText("碰撞体触发停留退出延迟")]
        [FoldoutGroup("指令系统")]
        [PropertyOrder(10)]
        [InfoBox("停留超时退出时间只会在停留退出延迟设置在0.1以上发生", InfoMessageType.Warning, "@ColliderTriggerStayExitDelay < 0.1f")]
        [ShowInInspector]
        public float ColliderTriggerStayExitDelay
        {
            get
            {

                return UnityRow.Row.ColliderTriggerStayExitDelay;
            }
            set
            {
                UnityRow.Row.ColliderTriggerStayExitDelay = value;
                Save();
            }
        }







        public IEnumerable TriggerEventUuidDropdown()
        {
            return GameSupportEditorUtility.GetTriggerEventUuids();
        }

        DirectInstructionGroup[] m_DirectInstructionGroups;

        public void UpdateGroupPrefab()
        {
            if (m_DirectInstructionGroups != null)
            {
                for (int i = 0; i < m_DirectInstructionGroups.Length; i++)
                // foreach (var group in m_DirectInstructionGroups)
                {
                    var group = m_DirectInstructionGroups[i];
                    group.SetPrefab(Prefab);


                    if (group.DirectInstructionList != null)
                    {
                        for (int j = 0; j < group.DirectInstructionList.Length; j++)
                        {
                            group.DirectInstructionList[j].SetPrefab(Prefab);
                        }
                    }

                    if (group.FailedDirectInstructionList != null)
                    {
                        for (int j = 0; j < group.FailedDirectInstructionList.Length; j++)
                        {
                            group.FailedDirectInstructionList[j].SetPrefab(Prefab);
                        }
                    }

                    if (group.StateDirectInstructionDict != null)
                    {
                        foreach (var kv in group.StateDirectInstructionDict)
                        {
                            for (int j = 0; j < kv.Value.Length; j++)
                            {
                                kv.Value[j].SetPrefab(Prefab);
                            }
                        }
                    }

                }
            }

        }

        [ShowInInspector]

        [LabelText("直接指令")]
        [FoldoutGroup("指令系统")]
        [PropertyOrder(10)]
        [OnValueChanged("OnDirectInstructionsChanged", includeChildren: true)]
        // [ListDrawerSettings(Expanded = true, CustomAddFunction = "AddDirectInstruction", CustomRemoveIndexFunction = "RemoveIndexDirectInstruction")]
        public DirectInstructionGroup[] DirectInstructions
        {
            get
            {
                if (m_DirectInstructionGroups == null)
                {
                    m_DirectInstructionGroups = DirectInstructionGroup.CreateList(UnityRow.Row?.DirectInstructions).ToArray();
                    if (m_DirectInstructionGroups == null)
                    {
                        m_DirectInstructionGroups = new DirectInstructionGroup[0];
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

        [LabelText("触发器Uuids")]
        [FoldoutGroup("指令系统")]
        [ValueDropdown("TriggerEventUuidDropdown", IsUniqueList = true)]
        [ListDrawerSettings(ShowFoldout = true)]
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



        void OnDirectInstructionsChanged()
        {
            for (int i = 0; i < m_DirectInstructionGroups.Length; i++)
            {
                if (m_DirectInstructionGroups[i].Targets == null)
                {
                    m_DirectInstructionGroups[i].Targets = new string[0];
                }

                if (m_DirectInstructionGroups[i].StringTargets == null)
                {
                    m_DirectInstructionGroups[i].StringTargets = new string[0];
                }

                if (m_DirectInstructionGroups[i].Uuid.IsNullOrWhiteSpace())
                {
                    m_DirectInstructionGroups[i].Uuid = UuidUtility.GetNewUuid();
                }

                if (m_DirectInstructionGroups[i].DirectInstructionList == null)
                {
                    m_DirectInstructionGroups[i].DirectInstructionList = new DirectInstruction[0];
                }

                if (m_DirectInstructionGroups[i].FailedDirectInstructionList == null)
                {
                    m_DirectInstructionGroups[i].FailedDirectInstructionList = new DirectInstruction[0];
                }

                if (m_DirectInstructionGroups[i].StateDirectInstructionDict == null)
                {
                    m_DirectInstructionGroups[i].StateDirectInstructionDict = new();
                }
                if (m_DirectInstructionGroups[i].ConditionUuids == null)
                {
                    m_DirectInstructionGroups[i].ConditionUuids = new string[0];
                }

                if (m_DirectInstructionGroups[i].BoolVariableUuds == null)
                {
                    m_DirectInstructionGroups[i].BoolVariableUuds = new string[0];
                }
            }

            var currentValue = DirectInstructionGroup.Save(m_DirectInstructionGroups);
            // Debug.Log($"Try Save:{currentValue}, old:{UnityRow.Row?.DirectInstructions}");
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

