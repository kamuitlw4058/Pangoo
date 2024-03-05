
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class StaticSceneEditor : MonoBehaviour
    {
        string m_StaticSceneUuid;
        [ReadOnly]
        [ShowInInspector]
        [ValueDropdown("StatidSceneValueDropdown")]
        [PropertyOrder(0)]
        public string StaticSceneUuid
        {
            get
            {
                return m_StaticSceneUuid;
            }
            set
            {
                m_StaticSceneUuid = value;
                OnValueChanged();
            }
        }
        [ReadOnly]
        public UnityStaticSceneRow Row;

        [ReadOnly]
        public StaticSceneOverview Overview;

        [SerializeField]
        [HideLabel]
        public StaticSceneDetailRowWrapper Wrapper;


        public IEnumerable StatidSceneValueDropdown()
        {
            return StaticSceneOverview.GetUuidDropdown();
        }


        public void OnValueChanged()
        {

            Overview = StaticSceneOverview.GetOverviewByUuid(StaticSceneUuid);
            Row = StaticSceneOverview.GetUnityRowByUuid(StaticSceneUuid);

            Wrapper = new StaticSceneDetailRowWrapper();
            Wrapper.Overview = Overview;
            Wrapper.UnityRow = Row;


        }
    }
}
#endif