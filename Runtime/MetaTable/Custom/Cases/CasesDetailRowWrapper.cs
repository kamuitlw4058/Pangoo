#if UNITY_EDITOR

using System;
using System.Collections;
using Sirenix.OdinInspector;
using MetaTable;
using UnityEngine;
using Pangoo.Common;
using Pangoo.Core.Common;
using LitJson;


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

        [LabelText("线索列表")]
        [ValueDropdown("@ClueOverview.GetUuidDropdown()")]
        [ListDrawerSettings(DefaultExpandedState = true)]
        [ShowInInspector]
        public string[] CluesUuidsList
        {
            get
            {
                return UnityRow.Row.CaseClues.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.CaseClues = value.ToListString();
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

        [ShowInInspector]
        public string ClueIntegrateString
        {
            get
            {
                return UnityRow.Row.CluesIntegrate;
            }
        }


        ClueIntegrate[] m_ClueIntegrate;

        [ShowInInspector]
        [LabelText("线索合成")]
        [HideReferenceObjectPicker]
        [OnValueChanged("OnClueIntegrateChanged", includeChildren: true)]
        public ClueIntegrate[] ClueIntegrate
        {
            get
            {
                if (m_ClueIntegrate == null)
                {
                    try
                    {
                        m_ClueIntegrate = JsonMapper.ToObject<ClueIntegrate[]>(UnityRow.Row.CluesIntegrate);
                    }
                    catch
                    {
                        m_ClueIntegrate = new ClueIntegrate[0];
                    }


                }

                return m_ClueIntegrate;
            }
            set
            {
                m_ClueIntegrate = value;
                try
                {
                    UnityRow.Row.CluesIntegrate = JsonMapper.ToJson(m_ClueIntegrate);
                }
                catch
                {

                }


                Save();
            }
        }

        public void OnClueIntegrateChanged()
        {
            Debug.Log($"asdf ");
            try
            {
                UnityRow.Row.CluesIntegrate = JsonMapper.ToJson(m_ClueIntegrate);
            }
            catch
            {
                Debug.Log($"Ser Failed!");
            }


            Save();
        }

    }
}
#endif

