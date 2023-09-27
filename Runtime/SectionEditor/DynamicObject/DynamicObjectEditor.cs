
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;
using Pangoo.Core.Character;

namespace Pangoo
{

    [ExecuteInEditMode]
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
        DynamicObjectTable.DynamicObjectRow Row;


        [HideLabel]
        [SerializeField]
        [PropertyOrder(10)]
        public DynamicObjectDetailWrapper Wrapper;

        private void OnEnable()
        {
            OnValueChanged();
        }



        public void OnValueChanged()
        {

            Overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<DynamicObjectTableOverview>(DynamicObjectId);
            Row = GameSupportEditorUtility.GetDynamicObjectRow(DynamicObjectId);

            Wrapper = new DynamicObjectDetailWrapper();
            Wrapper.Overview = Overview;
            Wrapper.Row = Row;

            transform.localPosition = Row.Position;
            transform.localRotation = Quaternion.Euler(Row.Rotation);
            Debug.Log($"Set Wrapper.{Row.Position}");
            InteractionItemTracker tracker;

            foreach (var trigger in Wrapper.Triggers)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnInteract:
                        tracker = transform.GetOrAddComponent<InteractionItemTracker>();
                        break;
                    default:
                        tracker = GetComponent<InteractionItemTracker>();
                        if (tracker != null)
                        {
                            DestroyImmediate(tracker);
                        }
                        break;
                }

            }

        }


        void Start()
        {
            // Row = GameSupportEditorUtility.GetDynamicObjectRow(DynamicObjectId);
            // Model = GameObject.Find("Model");
            // m_Wrapper = new DynamicObjectWrapper(Row, gameObject);
        }

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
            // List<Instruction> instructions = new();
            // foreach (var instruction in Wrapper.TriggerIds)
            // {
            //     instructions.Add(instruction.InstructionInstance);
            // }
            // Debug.Log($"Start Run Instruction:{instructions.Count}");
            // RunningInstructionList = new InstructionList(instructions.ToArray());
            // RunningInstructionList.Start(new Args(m_GameObject));
        }

    }


}
#endif