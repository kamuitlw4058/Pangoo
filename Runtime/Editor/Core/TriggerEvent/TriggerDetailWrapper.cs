
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
using LitJson;



namespace Pangoo
{


    [Serializable]
    public class TriggerDetailWrapper : ExcelTableRowDetailWrapper<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>
    {

        [ShowInInspector]
        [PropertyOrder(0)]
        [LabelText("触发类型")]
        // [ValueDropdown("GetTriggerEvent")]
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

                return Row?.TriggerType.ToEnum<TriggerTypeEnum>() ?? TriggerTypeEnum.Unknown;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.TriggerType = value.ToString();
                    // Row.Params = "{}";
                    Save();
                    // UpdateTrigger();
                }

            }
        }

        // TriggerEvent m_TriggerEventInstance;

        // [ShowInInspector]
        // [HideLabel]
        // [HideReferenceObjectPicker]
        // public TriggerEvent TriggerEventInstance
        // {
        //     get
        //     {
        //         if (m_TriggerEventInstance == null)
        //         {
        //             UpdateTrigger();
        //         }

        //         return m_TriggerEventInstance;
        //     }
        //     set
        //     {
        //         m_TriggerEventInstance = value;
        //     }
        // }

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


        [JsonNoMember]
        [HideInInspector]
        public Dictionary<GameObject, string> GoPathDict = new Dictionary<GameObject, string>();

        [ReadOnly]
        [LabelText("参考预制体")]
        [TitleGroup("目标")]
        [ShowInInspector]
        public GameObject RefPrefab { get; set; }

        [ShowInInspector]
        [LabelText("参考动态物体资产Id")]
        [ValueDropdown("OnDynamicObjectIdDropdown")]
        [OnValueChanged("OnDynamicObjectIdChanged")]
        [TitleGroup("目标")]
        public int DynamicObjectAssetPathId { get; set; }


        void OnDynamicObjectIdChanged()
        {
            RefPrefab = GameSupportEditorUtility.GetPrefabByAssetPathId(DynamicObjectAssetPathId);
        }


        IEnumerable OnDynamicObjectIdDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "DynamicObject" });
        }

        IEnumerable OnTargetsDropdown()
        {
            return GameSupportEditorUtility.RefPrefabStringDropdown(RefPrefab);
        }




        string[] m_Targets;

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
                    if (Row?.Targets.IsNullOrWhiteSpace() ?? true)
                    {
                        m_Targets = new string[0];
                    }
                    else
                    {
                        m_Targets = Row?.Targets?.Split("|");
                    }
                }

                return m_Targets;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.Targets = value.ToListString();
                    Save();
                }

            }
        }

        [ShowInInspector]
        [LabelText("目标列表操作方式")]
        [TitleGroup("目标")]
        public TriggerTargetListProcessTypeEnum TriggerListType
        {
            get
            {
                if (Row != null)
                {
                    return (TriggerTargetListProcessTypeEnum)Row.TargetListType;
                }

                return TriggerTargetListProcessTypeEnum.SeqAndDisabled;

            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.TargetListType = (int)value;
                    Save();
                }
            }
        }

        public void UpdateTargets()
        {
            Row.Targets = m_Targets.ToListString();
            Save();
        }

        public void AddTarget()
        {
            if (Row.Targets.IsNullOrWhiteSpace())
            {
                Row.Targets = "Self";
            }
            else
            {
                var l = Row.Targets.Split("|").ToList();
                l.Add("Self");
                Row.Targets = l.ToListString();
            }
            m_Targets = Row?.Targets?.Split("|");
            Save();
        }

        public void RemoveTarget(int index)
        {
            var targetList = m_Targets.ToList();
            targetList.RemoveAt(index);
            m_Targets = targetList.ToArray();
            Row.Targets = m_Targets.ToListString();
            Save();
        }

        [ShowInInspector]
        [LabelText("条件类型")]
        [TitleGroup("指令系统")]

        public ConditionTypeEnum ConditionType
        {
            get
            {
                return Row?.ConditionType.ToEnum<ConditionTypeEnum>() ?? ConditionTypeEnum.NoCondition;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.ConditionType = value.ToString();
                    Save();
                }
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
            return GameSupportEditorUtility.GetTriggerEvent();
        }


        [LabelText("条件Ids")]
        [ValueDropdown("ConditionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(9)]
        [TitleGroup("指令系统")]
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
                    Save();
                }

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
                if (m_DefaultDirectInstructions == null && Row?.InstructionList != null)
                {
                    try
                    {
                        m_DefaultDirectInstructions = JsonMapper.ToObject<DirectInstructionList>(Row.InstructionList);
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

                if (Row != null && Overview != null && value != null)
                {
                    Row.InstructionList = value.Save();
                    Save();
                }

            }
        }

        void OnDefaultDirectInstructionsChanged()
        {
            Row.InstructionList = m_DefaultDirectInstructions.Save();
            Save();
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
                if (m_FailedDirectInstructions == null && Row?.FailInstructionList != null)
                {
                    try
                    {
                        m_FailedDirectInstructions = JsonMapper.ToObject<DirectInstructionList>(Row.FailInstructionList);
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

                if (Row != null && Overview != null && value != null)
                {
                    Row.FailInstructionList = value.Save();
                    Save();
                }

            }
        }

        void OnFailedDirectInstructionsChanged()
        {
            Row.FailInstructionList = m_FailedDirectInstructions.Save();
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
                    m_StateInstructions = JsonMapper.ToObject<Dictionary<int, DirectInstructionList>>(Row.Params);
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
            Row.Params = JsonMapper.ToJson(StateInstructions);
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



        public IEnumerable InstructionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
        }

        public IEnumerable ConditionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetConditionIds(ConditionType);
        }

        // public ConditionList GetConditionList(int[] ids)
        // {
        //     if (ids.Length == 0)
        //     {
        //         return null;
        //     }

        //     List<Condition> items = new();

        //     foreach (var itemId in ids)
        //     {
        //         var row = GameSupportEditorUtility.GetConditionRowById(itemId);
        //         if (row == null || row.ConditionType == null)
        //         {
        //             continue;
        //         }

        //         var instance = ClassUtility.CreateInstance<Condition>(row.ConditionType);
        //         if (instance == null)
        //         {
        //             continue;
        //         }


        //         instance.Load(row.Params);
        //         items.Add(instance);
        //     }

        //     return new ConditionList(items.ToArray());
        // }

        // [Button("运行成功指令")]
        // [PropertyOrder(10)]
        // public void RunPass()
        // {
        //     var instructionList = InstructionList.BuildInstructionList(InstructionIds);
        //     instructionList?.Start(new Args());
        // }

        // [Button("运行失败指令")]
        // [PropertyOrder(10)]
        // public void RunFailed()
        // {
        //     var instructionList = InstructionList.BuildInstructionList(FailedInstructionIds);
        //     instructionList?.Start(new Args());
        // }




    }
}
#endif