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


        [ShowInInspector]
        [LabelText("案件显示类型")]
        [FoldoutGroup("案件显示", expanded: true)]
        public CaseShowType ShowType
        {
            get
            {
                return UnityRow.Row.CaseShowType.ToEnum<CaseShowType>();
            }
            set
            {
                UnityRow.Row.CaseShowType = value.ToString();
            }
        }



        [ShowInInspector]
        [FoldoutGroup("案件显示")]
        [ShowIf("@this.ShowType ==  CaseShowType.State")]
        [LabelText("案件状态显示")]
        public string CaseStatesString
        {
            get
            {
                return UnityRow.Row.CaseStates;
            }
        }

        List<CaseStateCheckItem> m_CaseStates;

        [ShowInInspector]
        [ListDrawerSettings(CustomAddFunction = "OnCaseStatesAdd", DefaultExpandedState = true)]
        [OnValueChanged("OnCaseStatesChanged", includeChildren: true)]
        [HideReferenceObjectPicker]
        [LabelText("案件状态列表")]
        [FoldoutGroup("案件显示")]
        [ShowIf("@this.ShowType ==  CaseShowType.State")]
        public List<CaseStateCheckItem> CaseStates
        {
            get
            {

                if (m_CaseStates == null)
                {
                    try
                    {
                        m_CaseStates = JsonMapper.ToObject<List<CaseStateCheckItem>>(UnityRow.Row.CaseStates);
                        foreach (var state in m_CaseStates)
                        {
                            state.OptionVariables = CaseVariables;
                        }
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



        void OnCaseStatesAdd()
        {
            var item = new CaseStateCheckItem();
            item.OptionVariables = CaseVariables;
            m_CaseStates.Add(item);
        }

        void OnCaseStatesChanged()
        {
            if (m_CaseStates != null)
            {
                foreach (var state in m_CaseStates)
                {
                    state.OptionVariables = CaseVariables;
                }
            }
            UnityRow.Row.CaseStates = JsonMapper.ToJson(m_CaseStates);
            Save();
        }

        [ShowInInspector]
        [FoldoutGroup("案件显示")]
        [ShowIf("@this.ShowType ==  CaseShowType.Variable")]
        public string CaseVariableStateString
        {
            get
            {
                return UnityRow.Row.CaseVariableState;
            }
        }
        List<CaseVariableCheckItem> m_CaseVariableState;

        [ShowInInspector]
        [ListDrawerSettings(CustomAddFunction = "OnCaseVariableStatesAdd", DefaultExpandedState = true)]
        [OnValueChanged("OnVariableCaseStateChanged", includeChildren: true)]
        [HideReferenceObjectPicker]
        [LabelText("案件状态列表")]
        [FoldoutGroup("案件显示")]
        [ShowIf("@this.ShowType ==  CaseShowType.Variable")]
        public List<CaseVariableCheckItem> CaseVariableState
        {
            get
            {

                if (m_CaseVariableState == null)
                {
                    try
                    {
                        m_CaseVariableState = JsonMapper.ToObject<List<CaseVariableCheckItem>>(UnityRow.Row.CaseVariableState);
                        foreach (var state in m_CaseVariableState)
                        {
                            state.OptionVariables = CaseVariables;
                            state.Prefab = Prefab;
                        }
                    }
                    catch { }

                    if (m_CaseVariableState == null)
                    {
                        m_CaseVariableState = new List<CaseVariableCheckItem>();
                    }

                }

                return m_CaseVariableState;
            }
            set
            {
                UnityRow.Row.CaseVariableState = JsonMapper.ToJson(m_CaseVariableState);
                Save();
            }
        }

        void OnCaseVariableStatesAdd()
        {
            var item = new CaseVariableCheckItem();
            item.OptionVariables = CaseVariables;
            item.Prefab = Prefab;
            m_CaseVariableState.Add(item);
            UnityRow.Row.CaseVariableState = JsonMapper.ToJson(m_CaseVariableState);
            Save();
        }

        void OnVariableCaseStateChanged()
        {
            if (m_CaseVariableState != null)
            {
                foreach (var state in m_CaseVariableState)
                {
                    state.OptionVariables = CaseVariables;
                    state.Prefab = Prefab;
                }
            }
            UnityRow.Row.CaseVariableState = JsonMapper.ToJson(m_CaseVariableState);
            Save();
        }

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



        [ShowInInspector]
        [LabelText("线索合成")]
        [FoldoutGroup("线索合成", expanded: true)]
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
        [FoldoutGroup("线索合成")]
        [ListDrawerSettings(DefaultExpandedState = true)]
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

