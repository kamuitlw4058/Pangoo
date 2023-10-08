using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;
using Pangoo.Core.Service;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Character;


namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public partial class DynamicObjectService : MonoMasterService
    {
        [ShowInInspector]
        public DynamicObjectTable.DynamicObjectRow Row { get; set; }

        public List<TriggerEventTable.TriggerEventRow> TriggerEventRows = new();

        public ExcelTableService TableService { get; set; }


        TriggerEventTable m_TriggerEventTable;
        InstructionTable m_InstructionTable;

        public List<TriggerEvent> TriggerEvents = new List<TriggerEvent>();


        public InteractionItemTracker m_Tracker = null;


        public Func<TriggerEventParams, bool> CheckInteract;

        public Action<TriggerEventParams> InteractEvent;



        public DynamicObjectService(GameObject gameObject) : base(gameObject)
        {
        }


        public override void DoAwake()
        {
            var triggerIds = Row.GetTriggerEventIdList();
#if !UNITY_EDITOR
             m_TriggerEventTable = TableService.GetExcelTable<TriggerEventTable>();
             m_InstructionTable = TableService.GetExcelTable<InstructionTable>();
             
#endif
            Debug.Log($"Init");
            TriggerEventRows.Clear();
            foreach (var triggerId in triggerIds)
            {
                Debug.Log($"Create TriggerId:{triggerId}");
                // var wrapper = new TriggerDetailWrapper();
                // wrapper.Id = trigger;
                // var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<TriggerEventTableOverview>(trigger);
                TriggerEventTable.TriggerEventRow row = null;
#if UNITY_EDITOR
                row = GameSupportEditorUtility.GetTriggerEventRowById(triggerId);
#else
                row = m_TriggerEventTable.GetRowById(triggerId);

#endif

                if (row != null)
                {
                    TriggerEventRows.Add(row);
                }
            }

            TriggerEvents.Clear();
            foreach (var triggerRow in TriggerEventRows)
            {
                var triggerInstance = ClassUtility.CreateInstance<TriggerEvent>(triggerRow.TriggerType);
                if (triggerInstance == null)
                {
                    return;
                }
                triggerInstance.Row = triggerRow;
                triggerInstance.LoadParamsFromJson(triggerRow.Params);
                triggerInstance.Instructions = GetInstructionList(triggerRow.GetInstructionList());
                TriggerEvents.Add(triggerInstance);
            }

            UpdateTracker();
        }

        public void UpdateTracker()
        {

            foreach (var trigger in TriggerEvents)
            {

                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnInteract:
                        m_Tracker = CachedTransfrom.GetOrAddComponent<InteractionItemTracker>();
                        m_Tracker.EventInteract += OnInteract;
                        break;
                }

            }

            if (m_Tracker != null)
            {
                InteractEvent += OnInteractEvent;
            }
        }



        public void OnInteractEvent(TriggerEventParams eventParams)
        {
            Debug.Log($"OnInteractEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents)
            {

                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnInteract:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }

        public void OnInteract(CharacterService character, IInteractive interactive)
        {
            if (CheckInteract != null && CheckInteract(null))
            {

            }

            Debug.Log($"OnInteract:{gameObject.name}");
            if (InteractEvent != null)
            {
                InteractEvent.Invoke(null);
            }
        }


        public override void DoDestroy()
        {
            base.DoDestroy();
            if (m_Tracker != null)
            {
                m_Tracker.EventInteract -= OnInteract;
                InteractEvent -= OnInteractEvent;
            }
        }




        public InstructionList GetInstructionList(List<int> ids)
        {
            List<Instruction> instructions = new();


            foreach (var instructionId in ids)
            {
                InstructionTable.InstructionRow instructionRow = null;

#if UNITY_EDITOR
                instructionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(instructionId);
#else
                instructionRow = m_InstructionTable.GetRowById(instructionId);
#endif
                if (instructionRow == null || instructionRow.InstructionType == null)
                {
                    continue;
                }

                var InstructionInstance = ClassUtility.CreateInstance<Instruction>(instructionRow.InstructionType);
                InstructionInstance.LoadParams(instructionRow.Params);

                instructions.Add(InstructionInstance);
            }

            return new InstructionList(instructions.ToArray());
        }


    }
}