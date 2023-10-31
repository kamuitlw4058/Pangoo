
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

        string[] m_Targets;

        [ShowInInspector]
        [LabelText("目标")]
        [TitleGroup("目标")]
        [ListDrawerSettings(CustomAddFunction = "AddTarget", CustomRemoveIndexFunction = "RemoveTarget")]
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


        // void UpdateTrigger()
        // {

        //     m_TriggerEventInstance = ClassUtility.CreateInstance<TriggerEvent>(Row.TriggerType);
        //     if (m_TriggerEventInstance == null)
        //     {
        //         return;
        //     }
        //     m_TriggerEventInstance.Row = Row;
        //     m_TriggerEventInstance.LoadParamsFromJson(Row.Params);
        //     m_TriggerEventInstance.RunInstructions = GetInstructionList(InstructionIds);
        //     m_TriggerEventInstance.FailInstructions = GetInstructionList(FailedInstructionIds);
        //     m_TriggerEventInstance.Conditions = GetConditionList(ConditionIds);
        // }

        public IEnumerable GetTriggerEvent()
        {
            return GameSupportEditorUtility.GetTriggerEvent();
        }

        // [ShowInInspector]
        // [ReadOnly]
        // public string Params
        // {
        //     get
        //     {
        //         return Row?.Params;
        //     }
        //     set
        //     {
        //         if (Row != null && Overview != null)
        //         {
        //             Row.Params = value;
        //             Save();
        //         }
        //     }
        // }

        // [Button("保存参数")]
        // [TableColumnWidth(80, resizable: false)]
        // public void SaveParams()
        // {
        //     Params = m_TriggerEventInstance.ParamsToJson();
        // }

        // [Button("加载参数")]
        // [TableColumnWidth(80, resizable: false)]
        // public void LoadParams()
        // {
        //     m_TriggerEventInstance.LoadParamsFromJson(Params);
        // }



        [LabelText("指令Ids")]
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(9)]
        [TitleGroup("指令系统")]
        [ShowIf("@this.ConditionType == ConditionTypeEnum.BoolCondition || this.ConditionType == ConditionTypeEnum.NoCondition")]


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
                    // UpdateTrigger();
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
                    // UpdateTrigger();
                    Save();
                }

            }
        }



        [LabelText("失败指令Ids")]
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(10)]
        [ShowIf("ConditionType", ConditionTypeEnum.BoolCondition)]
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
                    // UpdateTrigger();
                    Save();
                }

            }
        }

        Dictionary<StateKey, InstrctionIds> m_StateInstructionIds = new Dictionary<StateKey, InstrctionIds>();

        [ShowInInspector]
        [PropertyOrder(10)]
        [ShowIf("ConditionType", ConditionTypeEnum.StateCondition)]
        [OnCollectionChanged(after: "After")]
        [DictionaryDrawerSettings(KeyLabel = "状态", ValueLabel = "执行指令")]
        public Dictionary<StateKey, InstrctionIds> StateInstructionIds
        {
            get
            {
                return m_StateInstructionIds;
            }
            set
            {
                m_StateInstructionIds = value;
            }
        }


        public void After(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType == CollectionChangeType.SetKey)
            {
                var key = (StateKey)info.Key;
                if (info.Value == null)
                {
                    var ids = new InstrctionIds();
                    ids.Ids = new int[0];
                    m_StateInstructionIds[key] = ids;
                }
            }
            Debug.Log($"after info:{info} value:{value}");
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


                InstructionInstance.Load(instructionRow.Params);

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


                instance.Load(row.Params);
                items.Add(instance);
            }

            return new ConditionList(items.ToArray());
        }

        [Button("运行成功指令")]
        [PropertyOrder(10)]
        public void RunPass()
        {
            var instructionList = GetInstructionList(InstructionIds);
            instructionList?.Start(new Args());
        }

        [Button("运行失败指令")]
        [PropertyOrder(10)]
        public void RunFailed()
        {
            var instructionList = GetInstructionList(FailedInstructionIds);
            instructionList?.Start(new Args());
        }

        // Enum m_EnumType;

        // [ShowInInspector]
        // [TitleGroup("指令系统")]
        // [LabelText("参考枚举")]
        // [PropertyOrder(10)]
        // public Enum EnumType
        // {
        //     get
        //     {
        //         return m_EnumType;
        //     }
        //     set
        //     {
        //         m_EnumType = value;
        //         var tmp = new Dictionary<StateKey, InstrctionId[]>();

        //         foreach (var kv in m_StateInstructionIds)
        //         {
        //             var key = new StateKey();
        //             key.Id = kv.Key.Id;
        //             key.t = value.GetType();
        //             tmp.Add(key, kv.Value);
        //         }
        //         m_StateInstructionIds = tmp;
        //     }
        // }


        public struct InstrctionIds
        {
            [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
            public int[] Ids;

            public IEnumerable InstructionIdValueDropdown()
            {
                return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
            }
        }

        public struct StateKey
        {
            // [ValueDropdown("InstructionIdValueDropdown")]
            public int Id;

            // [HideInInspector]
            // public Type t;

            // public IEnumerable InstructionIdValueDropdown()
            // {
            //     ValueDropdownList<int> ret = new ValueDropdownList<int>();
            //     if (t != null)
            //     {
            //         foreach (var s in Enum.GetValues(t))
            //         {
            //             ret.Add(Enum.GetName(t, s), (int)s);
            //         }
            //     }
            //     else
            //     {
            //         ret.Add("9", 1);
            //         ret.Add("8", 1);
            //         ret.Add("7", 1);
            //         ret.Add("6", 1);
            //         ret.Add("5", 1);
            //         ret.Add("4", 1);
            //         ret.Add("3", 1);
            //         ret.Add("2", 1);
            //         ret.Add("1", 1);
            //         ret.Add("0", 0);
            //     }

            //     return ret;
            // }
        }

        public enum BoolEnum
        {
            False = 0,
            True = 1,
        }


    }
}
#endif