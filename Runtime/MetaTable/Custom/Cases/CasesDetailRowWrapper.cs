#if UNITY_EDITOR

using System;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using MetaTable;
using UnityEngine;
using Pangoo.Common;
using Pangoo.Core.Common;
using LitJson;
using Pangoo.Core.VisualScripting;
using System.Collections.Generic;


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
        [InlineButton("ToMenu", SdfIconType.EyeFill, Label = "")]
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

        public void ToMenu()
        {
            var objectTarget = Editor?.GetDetailWrapper(DynamicObjectUuid);
            if (objectTarget != null)
            {
                MenuWindow?.TrySelectMenuItemWithObject(objectTarget);
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
        [LabelText("案件变量")]
        [ValueDropdown("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString(), false)")]
        public string[] CaseVariables
        {
            get
            {
                return UnityRow.Row.CaseVariables.ToSplitArr<string>();
            }
            set
            {
                //VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Bool.ToString(), VariableTypeEnum.Global.ToString(), false);
                UnityRow.Row.CaseVariables = value.ToListString();
                Save();
            }
        }


        List<CaseStateCheckItem> m_CaseStates;

        [ShowInInspector]
        public string CaseStatesString
        {
            get
            {
                return UnityRow.Row.CaseStates;
            }
        }

        [ShowInInspector]
        [ListDrawerSettings(CustomAddFunction = "OnCaseStatesAdd")]
        [OnValueChanged("OnCaseStatesChanged", includeChildren: true)]
        [HideReferenceObjectPicker]
        [LabelText("案件状态列表")]
        public List<CaseStateCheckItem> CaseStates
        {
            get
            {

                if (m_CaseStates == null)
                {
                    try
                    {
                        m_CaseStates = JsonMapper.ToObject<List<CaseStateCheckItem>>(UnityRow.Row.CaseStates);
                    }
                    catch { }

                    if (m_CaseStates == null)
                    {
                        m_CaseStates = new List<CaseStateCheckItem>();
                    }

                }

                return m_CaseStates;
            }
            set
            {
                UnityRow.Row.CaseStates = JsonMapper.ToJson(m_CaseStates);
                Save();
            }
        }

        [Serializable]
        public class CaseStateModelOnOff
        {

            [ValueDropdown("GetOptionVariables")]
            [JsonMember("VariableUuids")]
            [LabelText("案件变量")]
            public string Path;

            [JsonNoMember]
            [HideInInspector]
            public GameObject Prefab;

        }

        // Dictionary<int,CaseStateModelOnOff>



        public void UpdateOptionVariables()
        {
            if (m_CaseStates != null)
            {
                foreach (var state in m_CaseStates)
                {
                    state.OptionVariables = CaseVariables;
                }
            }
        }

        void OnCaseStatesAdd()
        {
            var item = new CaseStateCheckItem();
            item.OptionVariables = CaseVariables;
            m_CaseStates.Add(item);
        }

        void OnCaseStatesChanged()
        {
            UnityRow.Row.CaseStates = JsonMapper.ToJson(m_CaseStates);
            Save();
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

