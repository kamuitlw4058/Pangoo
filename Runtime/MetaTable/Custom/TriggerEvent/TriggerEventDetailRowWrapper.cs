#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Xml.Serialization;

using Pangoo.Common;
using MetaTable;

using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class TriggerEventDetailRowWrapper : MetaTableDetailRowWrapper<TriggerEventOverview, UnityTriggerEventRow>
    {
        [ShowInInspector]
        [PropertyOrder(0)]
        [LabelText("触发类型")]
        public TriggerTypeEnum TriggerType
        {
            get
            {

                // if (m_TriggerEventInstance == null)
                // {
                //     UpdateTrigger();
                // }
                // if (Row != null)
                // {
                //     Row.TriggerType = typeof(TriggerEvent).FullName;
                // }

                return UnityRow.Row?.TriggerType.ToEnum<TriggerTypeEnum>() ?? TriggerTypeEnum.Unknown;
            }
            set
            {

                UnityRow.Row.TriggerType = value.ToString();
                Save();

            }
        }



        [ShowInInspector]
        [LabelText("是否默认打开")]
        public bool Enabled
        {
            get
            {


                return UnityRow.Row?.Enabled ?? false;
            }
            set
            {

                UnityRow.Row.Enabled = value;
                Save();

            }
        }


        [JsonNoMember]
        [HideInInspector]
        public Dictionary<GameObject, string> GoPathDict = new Dictionary<GameObject, string>();

        [ReadOnly]
        [LabelText("参考预制体")]
        [TitleGroup("目标")]
        [ShowInInspector]
        public GameObject RefPrefab { get; set; }

        [ShowInInspector]
        [LabelText("参考动态物体Uuid")]
        [ValueDropdown("OnDynamicObjectUuidDropdown")]
        [OnValueChanged("OnDynamicObjectUuidChanged")]
        [TitleGroup("目标")]
        public string RefDynamicObjectUuid { get; set; }


        void OnDynamicObjectUuidChanged()
        {
            RefPrefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(RefDynamicObjectUuid);
        }


        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }

        IEnumerable OnTargetsDropdown()
        {
            return GameSupportEditorUtility.RefPrefabStringDropdown(RefPrefab);
        }


        public IEnumerable VariableBoolUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString());
        }

        public IEnumerable VariableIntUuidValueDropdown()
        {
            return VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString());
        }


        string[] m_Targets;

        void SetTarget(string[] l)
        {
            UnityRow.Row.Targets = l.ToListString();
            if (UnityRow.Row.Targets.IsNullOrWhiteSpace())
            {
                m_Targets = new string[0];
            }
            else
            {
                m_Targets = UnityRow.Row?.Targets?.Split("|");
            }
        }


        [ShowInInspector]
        [LabelText("目标")]
        [TitleGroup("目标")]
        [ListDrawerSettings(CustomAddFunction = "AddTarget", CustomRemoveIndexFunction = "RemoveTarget")]
        [ValueDropdown("OnTargetsDropdown")]
        [OnValueChanged("UpdateTargets", includeChildren: true)]
        public string[] Targets
        {
            get
            {
                if (m_Targets == null)
                {
                    if (UnityRow.Row?.Targets.IsNullOrWhiteSpace() ?? true)
                    {
                        m_Targets = new string[0];
                    }
                    else
                    {
                        m_Targets = UnityRow.Row?.Targets?.Split("|");
                    }
                }

                return m_Targets;
            }
            set
            {
                SetTarget(value);
                Save();
            }
        }

        [ShowInInspector]
        [LabelText("目标列表操作方式")]
        [TitleGroup("目标")]
        public TriggerTargetListProcessTypeEnum TriggerListType
        {
            get
            {

                return (TriggerTargetListProcessTypeEnum)UnityRow.Row.TargetListType;


            }
            set
            {

                UnityRow.Row.TargetListType = (int)value;
                Save();
            }
        }

        public void UpdateTargets()
        {
            UnityRow.Row.Targets = m_Targets.ToListString();
            Save();
        }

        public void AddTarget()
        {
            if (UnityRow.Row.Targets.IsNullOrWhiteSpace())
            {
                UnityRow.Row.Targets = "Self";
            }
            else
            {
                var l = UnityRow.Row.Targets.ToSplitList<string>();
                l.Add("Self");
                UnityRow.Row.Targets = l.ToListString();
            }
            m_Targets = UnityRow.Row?.Targets?.Split("|");
            Save();
        }

        public void RemoveTarget(int index)
        {
            var targetList = m_Targets.ToList();
            targetList.RemoveAt(index);
            m_Targets = targetList.ToArray();
            UnityRow.Row.Targets = m_Targets.ToListString();
            Save();
        }

        [ShowInInspector]
        [LabelText("条件类型")]
        [TitleGroup("条件")]

        public ConditionTypeEnum ConditionType
        {
            get
            {
                return UnityRow.Row?.ConditionType.ToEnum<ConditionTypeEnum>() ?? ConditionTypeEnum.NoCondition;
            }
            set
            {

                UnityRow.Row.ConditionType = value.ToString();
                Save();
            }
        }

        [ShowInInspector]
        [LabelText("使用变量条件")]
        [TitleGroup("条件")]
        [ShowIf("@this.UseCondition")]

        public bool UseVariableCondition
        {
            get
            {
                return UnityRow.Row.UseVariableCondition;
            }
            set
            {

                UnityRow.Row.UseVariableCondition = value;
                Save();
            }
        }

        [ShowInInspector]
        [LabelText("Bool变量条件")]
        [TitleGroup("条件")]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition && this.UseVariableCondition")]
        [ValueDropdown("VariableBoolUuidValueDropdown", IsUniqueList = true)]
        public string[] BoolVariableUuds
        {
            get
            {
                return UnityRow.Row.BoolVariableUuds.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.BoolVariableUuds = value.ToListString();
                Save();
            }
        }


        [ShowInInspector]
        [LabelText("状态变量条件")]
        [TitleGroup("条件")]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.StateCondition && this.UseVariableCondition")]
        [ValueDropdown("VariableIntUuidValueDropdown", IsUniqueList = true)]

        public string IntVariableUuid
        {
            get
            {
                return UnityRow.Row.IntVariableUuid;
            }
            set
            {
                UnityRow.Row.IntVariableUuid = value;
                Save();
            }
        }




        bool UseCondition
        {
            get
            {
                return ConditionType != ConditionTypeEnum.NoCondition;
            }
        }




        public IEnumerable GetTriggerEvent()
        {
            return TriggerEventOverview.GetIdDropdown();
        }



        [LabelText("条件Uuid")]
        [ValueDropdown("ConditionUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(9)]
        [TitleGroup("条件")]
        [ShowIf("@this.UseCondition && !this.UseVariableCondition")]
        public string[] ConditionUuids
        {
            get
            {
                return UnityRow.Row.ConditionUuidList?.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.ConditionUuidList = value.ToListString();
                Save();
            }
        }

        DirectInstructionList m_DefaultDirectInstructions;

        [ShowInInspector]
        [PropertyOrder(10)]
        [TitleGroup("指令系统")]
        [BoxGroup("指令系统/默认指令")]
        [HideLabel]
        [HideReferenceObjectPicker]
        [OnValueChanged("OnDefaultDirectInstructionsChanged", includeChildren: true)]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition || this.ConditionType == ConditionTypeEnum.NoCondition")]


        public DirectInstructionList DefaultDirectInstructions
        {
            get
            {
                if (m_DefaultDirectInstructions == null && UnityRow.Row?.InstructionList != null)
                {
                    try
                    {
                        m_DefaultDirectInstructions = JsonMapper.ToObject<DirectInstructionList>(UnityRow.Row.InstructionList);
                        m_DefaultDirectInstructions.Init();
                    }
                    catch (Exception e)
                    {
                    }

                    if (m_DefaultDirectInstructions == null)
                    {
                        m_DefaultDirectInstructions = new DirectInstructionList();
                    }

                }
                return m_DefaultDirectInstructions;
            }
            set
            {
                UnityRow.Row.InstructionList = value.Save();
                Save();

            }
        }

        void OnDefaultDirectInstructionsChanged()
        {
            if (m_DefaultDirectInstructions != null)
            {
                UnityRow.Row.InstructionList = m_DefaultDirectInstructions.Save();
                Save();
            }

        }


        DirectInstructionList m_FailedDirectInstructions;


        [ShowInInspector]
        [PropertyOrder(11)]
        [TitleGroup("指令系统")]
        [BoxGroup("指令系统/失败指令")]
        [HideLabel]
        [HideReferenceObjectPicker]
        [OnValueChanged("OnFailedDirectInstructionsChanged", includeChildren: true)]
        [ShowIf("ConditionType", ConditionTypeEnum.BoolCondition)]
        public DirectInstructionList FailedDirectInstructions
        {
            get
            {
                if (m_FailedDirectInstructions == null && UnityRow.Row?.FailInstructionList != null)
                {
                    try
                    {
                        m_FailedDirectInstructions = JsonMapper.ToObject<DirectInstructionList>(UnityRow.Row.FailInstructionList);
                    }
                    catch (Exception e)
                    {
                    }

                    if (m_FailedDirectInstructions == null)
                    {
                        m_FailedDirectInstructions = new DirectInstructionList();
                    }

                }
                return m_FailedDirectInstructions;
            }
            set
            {
                UnityRow.Row.FailInstructionList = value.Save();
                Save();
            }
        }

        void OnFailedDirectInstructionsChanged()
        {
            UnityRow.Row.FailInstructionList = m_FailedDirectInstructions.Save();
            Save();
        }


        Dictionary<int, DirectInstructionList> m_StateInstructions;


        [ShowInInspector]
        [PropertyOrder(10)]
        [ShowIf("ConditionType", ConditionTypeEnum.StateCondition)]
        [DictionaryDrawerSettings(KeyLabel = "状态", ValueLabel = "执行指令")]
        [OnCollectionChanged(after: "After")]
        [HideReferenceObjectPicker]
        [TitleGroup("指令系统")]
        [OnValueChanged("OnStateInstructionIdsChanged", includeChildren: true)]

        public Dictionary<int, DirectInstructionList> StateInstructions
        {
            get
            {
                if (m_StateInstructions == null)
                {
                    m_StateInstructions = JsonMapper.ToObject<Dictionary<int, DirectInstructionList>>(UnityRow.Row.Params);
                }

                return m_StateInstructions;
            }
            set
            {
                m_StateInstructions = value;
            }
        }

        void OnStateInstructionIdsChanged()
        {
            Debug.Log($"OnStateInstructionIdsChanged:{JsonMapper.ToJson(StateInstructions)}");
            UnityRow.Row.Params = JsonMapper.ToJson(StateInstructions);
        }


        public void After(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType == CollectionChangeType.SetKey)
            {
                var key = (int)info.Key;
                if (m_StateInstructions[key].DirectInstructions == null)
                {
                    var directionInstructions = new DirectInstructionList();
                    m_StateInstructions[key] = directionInstructions;
                }
            }
        }



        public IEnumerable ConditionIdValueDropdown()
        {
            return ConditionOverview.GetIdDropdown();
        }

        public IEnumerable ConditionUuidValueDropdown()
        {
            return ConditionOverview.GetUuidDropdown();
        }

    }
}
#endif

