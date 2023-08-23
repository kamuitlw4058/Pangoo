
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Editor{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class GameSectionEditor : MonoBehaviour
    {
        [ValueDropdown("GetSectionList")]
        [OnValueChanged("OnSectionChange")]
        public int Section;



        [ReadOnly]
        public GameSectionTable.GameSectionRow SectionRow;

        StaticSceneEditor m_StaticSceneEditor;
        DynamicObjectEditor m_DynamicObjectEditor;


        public void UpdateSection(){
            m_StaticSceneEditor.SetSection(Section);
            m_DynamicObjectEditor.SetSection(Section);
        }

        void UpdateGameObjectName(){
            name = "//Section";

            if(Section != 0){
                name = $"{name}:{Section}";
            }

        }

        public void OnSectionChange(){
            UpdateSection();
            UpdateGameObjectName();
        }  

        private void OnEnable() {
            SectionRow = GameSupportEditorUtility.GetGameSectionRowById(Section);

            m_StaticSceneEditor = GetComponentInChildren<StaticSceneEditor>();
            if(m_StaticSceneEditor == null){
                var go = new GameObject();
                go.transform.parent = transform;
                go.ResetTransfrom();
                m_StaticSceneEditor = go.GetOrAddComponent<StaticSceneEditor>();
            }

            m_DynamicObjectEditor = GetComponentInChildren<DynamicObjectEditor>();
            if(m_DynamicObjectEditor == null){
                var go = new GameObject();
                go.transform.parent = transform;
                go.ResetTransfrom();
                m_DynamicObjectEditor = go.GetOrAddComponent<DynamicObjectEditor>();
            }



            OnSectionChange();
        }

        private void OnDisable() {

        }

        private void OnDestroy() {
            // DestroyImmediate(m_StaticSceneEditor);
            UnityEditor.EditorApplication.delayCall += () =>
            {
                DestroyImmediate(gameObject);
            };
        }

        void Update(){
            gameObject.ResetTransfrom();

        }


        public IEnumerable GetSectionList(){
            return GameSupportEditorUtility.GetGameSectionIds();
        }
    }
}
#endif