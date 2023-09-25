
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class DynamicObjectEditorHelper : MonoBehaviour
    {
        [ReadOnly]
        public int DynamicObjectId;

        DynamicObjectTable.DynamicObjectRow Row;


        [ShowInInspector]
        [HideLabel]
        [PropertyOrder(100)]
        public DynamicObjectWrapper m_Wrapper;



        [ReadOnly]
        [SerializeField]
        public GameObject Model;



        void Start()
        {
            Row = GameSupportEditorUtility.GetDynamicObjectRow(DynamicObjectId);
            Model = GameObject.Find("Model");
            m_Wrapper = new DynamicObjectWrapper(Row, gameObject);
        }

        private void Update()
        {
            m_Wrapper.OnUpdate();
        }

        [Button("SetTransfrom")]
        public void SetTransfrom()
        {
            Row.Position = transform.localPosition;
            Row.Rotation = transform.localRotation.eulerAngles;
        }

    }


}
#endif