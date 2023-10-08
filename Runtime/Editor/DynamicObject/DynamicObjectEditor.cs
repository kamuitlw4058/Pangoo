
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;
using Pangoo.Core.Character;
using System;

namespace Pangoo
{

    //[ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class DynamicObjectEditor : MonoBehaviour
    {
        [ReadOnly]
        public int m_DynamicObjectId;

        [ReadOnly]
        [ShowInInspector]
        [ValueDropdown("DynamicObjectIdValueDropdown")]
        [PropertyOrder(0)]
        public int DynamicObjectId
        {
            get
            {
                return m_DynamicObjectId;
            }
            set
            {
                m_DynamicObjectId = value;
                OnValueChanged();
            }
        }

        public IEnumerable DynamicObjectIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
        }

        [ReadOnly]
        DynamicObjectTableOverview Overview;



        [ReadOnly]
        [ShowInInspector]
        DynamicObjectTable.DynamicObjectRow Row;


        [HideLabel]
        [SerializeField]
        [PropertyOrder(10)]
        public DynamicObjectDetailWrapper Wrapper;

        private void OnEnable()
        {
            OnValueChanged();
        }

        void Start()
        {
            DoService = new DynamicObjectService(gameObject);
            Debug.Log($"Row1:{Row}");
            Row = GameSupportEditorUtility.GetDynamicObjectRow(m_DynamicObjectId);
            DoService.Row = Row;
            Debug.Log($"Row2:{Row}");
            DoService.Awake();
            DoService.Start();
            Debug.Log($"Row2:{DoService.TriggerEvents.Count}");
        }

        // public Func<TriggerEventParams, bool> CheckInteract;

        // public Action<TriggerEventParams> InteractEvent;

        [ShowInInspector]
        [field: NonSerialized]
        public DynamicObjectService DoService { get; set; }



        public void OnValueChanged()
        {
            if (m_DynamicObjectId == 0) return;

            Overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<DynamicObjectTableOverview>(m_DynamicObjectId);
            Row = GameSupportEditorUtility.GetDynamicObjectRow(m_DynamicObjectId);



            Debug.Log($"Row:{Row} DynamicObjectId:{m_DynamicObjectId}");

            Wrapper = new DynamicObjectDetailWrapper();
            Wrapper.Overview = Overview;
            Wrapper.Row = Row;

            transform.localPosition = Row.Position;
            transform.localRotation = Quaternion.Euler(Row.Rotation);
            Debug.Log($"Set Wrapper.{Row.Position}");
            // InteractionItemTracker tracker = null;

            // foreach (var trigger in Wrapper.Triggers)
            // {


            //     switch (trigger.TriggerEventInstance.TriggerType)
            //     {
            //         case TriggerTypeEnum.OnInteract:
            //             tracker = transform.GetOrAddComponent<InteractionItemTracker>();
            //             tracker.EventInteract += OnInteract;
            //             break;
            //         default:
            //             tracker = GetComponent<InteractionItemTracker>();
            //             if (tracker != null)
            //             {
            //                 tracker.EventInteract -= OnInteract;
            //                 DestroyImmediate(tracker);
            //             }
            //             // OnInteractEvent;
            //             break;
            //     }

            // }

            // if (tracker != null)
            // {
            //     InteractEvent += OnInteractEvent;
            // }
            // else
            // {
            //     InteractEvent -= OnInteractEvent;
            // }

        }

        // public void OnInteractEvent(TriggerEventParams eventParams)
        // {
        //     Debug.Log($"OnInteractEvent:{name}");
        //     foreach (var trigger in Wrapper.Triggers)
        //     {

        //         switch (trigger.TriggerEventInstance.TriggerType)
        //         {
        //             case TriggerTypeEnum.OnInteract:
        //                 trigger.TriggerEventInstance.OnInvoke(eventParams);
        //                 break;
        //         }

        //     }
        // }

        // public void OnInteract(CharacterService character, IInteractive interactive)
        // {
        //     if (CheckInteract != null && CheckInteract(null))
        //     {

        //     }

        //     Debug.Log($"OnInteract:{name}");
        //     if (InteractEvent != null)
        //     {
        //         InteractEvent.Invoke(null);
        //     }
        // }


        private void Update()
        {
        }

        [Button("SetTransfrom")]
        public void SetTransfrom()
        {
            Wrapper.Row.Position = transform.localPosition;
            Wrapper.Row.Rotation = transform.localRotation.eulerAngles;
            Wrapper.Save();

        }

        public void Run()
        {

        }

    }


}
#endif