
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public partial class GameSectionEditor : MonoBehaviour
    {


        [ValueDropdown("GetSectionList")]
        [OnValueChanged("OnSectionChange")]
        public int Section;



        [ReadOnly]
        public GameSectionTable.GameSectionRow SectionRow;

        [ReadOnly]
        public GameSectionTableOverview Overview;

        [SerializeField]
        [HideLabel]
        public GameSectionDetailWrapper Wrapper;


        GameSceneStaticSceneEditor m_StaticSceneEditor;
        GameSceneDynamicObjectEditor m_DynamicObjectEditor;


        public void UpdateSection()
        {

            Debug.Log($"OnSectionChange");

            Overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<GameSectionTableOverview>(Section);
            SectionRow = GameSupportEditorUtility.GetGameSectionRowById(Section);

            Wrapper = new GameSectionDetailWrapper();
            Wrapper.Overview = Overview;
            Wrapper.Row = SectionRow;

            Debug.Log($"DynamicSceneIds:{Wrapper.DynamicSceneIds.ToList().ToListString()}");
            Debug.Log($"KeepSceneIds:{Wrapper.KeepSceneIds.ToList().ToListString()}");

            m_StaticSceneEditor.SetSection(Section);
            m_StaticSceneEditor.UpdateDynamicSceneIds(Wrapper.DynamicSceneIds);
            m_StaticSceneEditor.UpdateKeepSceneIds(Wrapper.KeepSceneIds);


            m_DynamicObjectEditor.SetSection(Section);
            m_DynamicObjectEditor.UpdateObjects(Wrapper.DynamicObjectIds);
        }

        void UpdateGameObjectName()
        {
            name = "//Section";

            if (Section != 0)
            {
                name = $"{name}:{Section}";
            }

        }

        public void OnSectionChange()
        {
            UpdateSection();
            UpdateGameObjectName();
        }

        private void OnEnable()
        {
            SectionRow = GameSupportEditorUtility.GetGameSectionRowById(Section);

            m_StaticSceneEditor = GetComponentInChildren<GameSceneStaticSceneEditor>();
            if (m_StaticSceneEditor == null)
            {
                var go = new GameObject();
                go.transform.parent = transform;
                go.ResetTransfrom();
                m_StaticSceneEditor = go.GetOrAddComponent<GameSceneStaticSceneEditor>();
            }

            m_DynamicObjectEditor = GetComponentInChildren<GameSceneDynamicObjectEditor>();
            if (m_DynamicObjectEditor == null)
            {
                var go = new GameObject();
                go.transform.parent = transform;
                go.ResetTransfrom();
                m_DynamicObjectEditor = go.GetOrAddComponent<GameSceneDynamicObjectEditor>();
            }


            OnSectionChange();
        }

        private void OnDisable()
        {

        }

        private void OnDestroy()
        {
            // DestroyImmediate(m_StaticSceneEditor);
            // UnityEditor.EditorApplication.delayCall += () =>
            // {
            //     if (gameObject != null)
            //     {
            //         DestroyImmediate(gameObject);
            //     }

            // };
        }

        void Update()
        {
            if (Wrapper.OutsideNeedRefresh)
            {
                UpdateSection();
            }
            gameObject.ResetTransfrom();

        }


        public IEnumerable GetSectionList()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<GameSectionTableOverview>();
        }


    }
}
#endif