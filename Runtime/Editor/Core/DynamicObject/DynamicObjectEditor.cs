
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;
using Pangoo.Core.Characters;
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
            DoService = new DynamicObject(gameObject);
            DoService.Row = Row;
            DoService.Awake();
            DoService.Start();
        }

        // public Func<TriggerEventParams, bool> CheckInteract;

        // public Action<TriggerEventParams> InteractEvent;

        [ShowInInspector]
        [field: NonSerialized]
        [LabelText("动态物体")]
        [HideReferenceObjectPicker]
        public DynamicObject DoService { get; private set; }



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


        }


        private void Update()
        {
            DoService?.Update();
        }

        [Button("SetTransfrom")]
        public void SetTransfrom()
        {
            Wrapper.Row.Position = transform.localPosition;
            Wrapper.Row.Rotation = transform.localRotation.eulerAngles;
            Wrapper.Save();

        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"DynamicObjectEditor OnTriggerEnter");
            DoService?.TriggerEnter3d(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"DynamicObjectEditor OnTriggerExit");
            DoService?.TriggerExit3d(other);
        }

    }


}
#endif