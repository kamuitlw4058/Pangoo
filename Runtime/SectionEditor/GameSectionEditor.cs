
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



        StaticSceneEditor m_StaticSceneEditor;


        public void UpdateSection(){
            m_StaticSceneEditor.SetSection(Section);
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
            m_StaticSceneEditor = GetComponentInChildren<StaticSceneEditor>();
            if(m_StaticSceneEditor == null){
                var go = new GameObject();
                go.transform.parent = transform;
                m_StaticSceneEditor = go.GetOrAddComponent<StaticSceneEditor>();
            }

            OnSectionChange();
        }

        private void OnDisable() {

        }

        private void OnDestroy() {
            DestroyImmediate(m_StaticSceneEditor);
            DestroyImmediate(gameObject);
        }


        public IEnumerable GetSectionList(){
            return GameSupportEditorUtility.GetGameSectionIds();
        }
    }
}
#endif