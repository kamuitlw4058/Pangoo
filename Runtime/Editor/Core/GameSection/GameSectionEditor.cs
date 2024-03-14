
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using Pangoo.MetaTable;
using Sirenix.Utilities;

namespace Pangoo.Editor
{

    [ExecuteAlways]
    [DisallowMultipleComponent]
    public partial class GameSectionEditor : MonoBehaviour
    {


        [ValueDropdown("GetSectionList")]
        [OnValueChanged("OnSectionChange")]
        public string Section;


        [ReadOnly]
        public UnityGameSectionRow GameSectionRow;

        [ShowIf("@this.GameSectionRow != null")]
        [ShowInInspector]
        [LabelText("段落配置")]
        public GameSectionDetailRowWrapper DetailWrapper;


        GameSceneStaticSceneEditor m_StaticSceneEditor;


        GameSceneDynamicObjectEditor m_DynamicObjectEditor;




        public void UpdateSection()
        {

            Debug.Log($"OnSectionChange");

            GameSectionRow = GameSectionOverview.GetUnityRowByUuid(Section);
            if (GameSectionRow == null)
            {
                return;
            }

            GameSectionOverview Overview = GameSectionOverview.GetOverviewByUuid(Section);



            DetailWrapper = new GameSectionDetailRowWrapper();
            DetailWrapper.Overview = Overview;
            DetailWrapper.UnityRow = GameSectionRow;


            InitStaticSceneEditor();
            InitDynamicObjectEditor();


            if (!Application.isPlaying)
            {
                m_StaticSceneEditor.Uuids = DetailWrapper.SceneUuids.ToList();
            }
            else
            {
                m_StaticSceneEditor.ClearScene();
            }


            if (!Application.isPlaying)
            {
                var uuids = DetailWrapper.DynamicObjectUuids?.ToList();
                m_DynamicObjectEditor.Uuids = uuids;
            }
            else
            {
                m_DynamicObjectEditor.Clear();
            }
        }

        void UpdateGameObjectName()
        {
            name = "//Section";

            if (GameSectionRow != null)
            {
                name = $"{name}:{GameSectionRow.Name}";
            }

        }

        public void OnSectionChange()
        {
            UpdateSection();
            UpdateGameObjectName();
        }

        public void InitStaticSceneEditor()
        {
            if (m_StaticSceneEditor == null)
            {
                m_StaticSceneEditor = GetComponentInChildren<GameSceneStaticSceneEditor>();
                if (m_StaticSceneEditor != null)
                {
                    return;
                }
                var go = new GameObject();
                go.transform.parent = transform;
                go.ResetTransfrom();
                m_StaticSceneEditor = go.GetOrAddComponent<GameSceneStaticSceneEditor>();
            }
        }

        public void InitDynamicObjectEditor()
        {
            if (m_DynamicObjectEditor == null)
            {
                m_DynamicObjectEditor = GetComponentInChildren<GameSceneDynamicObjectEditor>();
                if (m_DynamicObjectEditor != null)
                {
                    return;
                }
                var go = new GameObject();
                go.transform.parent = transform;
                go.ResetTransfrom();
                m_DynamicObjectEditor = go.GetOrAddComponent<GameSceneDynamicObjectEditor>();
            }
        }


        private void OnEnable()
        {
            OnSectionChange();

            // m_StaticSceneEditor = GetComponentInChildren<GameSceneStaticSceneEditor>();
            // if (m_StaticSceneEditor == null)
            // {
            //     var go = new GameObject();
            //     go.transform.parent = transform;
            //     go.ResetTransfrom();
            //     m_StaticSceneEditor = go.GetOrAddComponent<GameSceneStaticSceneEditor>();
            // }

            // m_DynamicObjectEditor = GetComponentInChildren<GameSceneDynamicObjectEditor>();
            // if (m_DynamicObjectEditor == null)
            // {
            //     var go = new GameObject();
            //     go.transform.parent = transform;
            //     go.ResetTransfrom();
            //     m_DynamicObjectEditor = go.GetOrAddComponent<GameSceneDynamicObjectEditor>();
            // }


            // OnSectionChange();
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
            gameObject.ResetTransfrom();
            // GameSectionRow = GameSectionOverview.GetUnityRowByUuid(Section);
            // if (GameSectionRow == null)
            // {
            //     return;
            // }


            // if (Wrapper.OutsideNeedRefresh)
            // {
            //     UpdateSection();
            // }


        }


        public IEnumerable GetSectionList()
        {
            return GameSectionOverview.GetUuidDropdown();
        }


    }
}
#endif