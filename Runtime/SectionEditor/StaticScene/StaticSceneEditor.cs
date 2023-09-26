
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class StaticSceneEditor : MonoBehaviour
    {
        int m_StaticSceneId;
        [ReadOnly]
        [ShowInInspector]
        [ValueDropdown("StatidSceneValueDropdown")]
        [PropertyOrder(0)]
        public int StaticSceneId
        {
            get
            {
                return m_StaticSceneId;
            }
            set
            {
                m_StaticSceneId = value;
                OnValueChanged();
            }
        }
        [ReadOnly]
        public StaticSceneTable.StaticSceneRow Row;

        [ReadOnly]
        public StaticSceneTableOverview Overview;

        [SerializeField]
        [HideLabel]
        public StaticSceneDetailWrapper Wrapper;


        public IEnumerable StatidSceneValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<StaticSceneTableOverview>();
        }


        public void OnValueChanged()
        {

            Overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<StaticSceneTableOverview>(StaticSceneId);
            Row = GameSupportEditorUtility.GetStaticSceneRowById(StaticSceneId);

            Wrapper = new StaticSceneDetailWrapper();
            Wrapper.Overview = Overview;
            Wrapper.Row = Row;


        }
    }
}
#endif