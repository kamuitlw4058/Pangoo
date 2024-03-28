#if UNITY_EDITOR

using System;
using System.Collections;
using Sirenix.OdinInspector;
using MetaTable;
using UnityEngine;


namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class CasesDetailRowWrapper : MetaTableDetailRowWrapper<CasesOverview, UnityCasesRow>
    {


        [LabelText("案件标题")]
        [ShowInInspector]
        public string CaseTitle
        {
            get
            {
                return UnityRow.Row.CaseTitle;
            }
            set
            {
                UnityRow.Row.CaseTitle = value;
                Save();
            }
        }



        [LabelText("显示的动态物体")]
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        [ShowInInspector]
        public string DynamicObjectUuid
        {
            get
            {
                return UnityRow.Row.DynamicObjectUuid;
            }
            set
            {
                UnityRow.Row.DynamicObjectUuid = value;
                UpdatePrefab();
                Save();
            }
        }


        GameObject m_Prefab;


        [ShowInInspector]
        [LabelText("资源预制体")]
        [ReadOnly]
        public GameObject Prefab
        {
            get
            {
                if (m_Prefab == null)
                {
                    m_Prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(UnityRow.Row.DynamicObjectUuid);
                }
                return m_Prefab;
            }
        }

        public void UpdatePrefab()
        {
            m_Prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(UnityRow.Row.DynamicObjectUuid);
        }

    }
}
#endif

