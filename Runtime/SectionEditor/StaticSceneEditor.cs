
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Editor{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class StaticSceneEditor : MonoBehaviour
    {
        [ReadOnly]
        [ValueDropdown("GetSectionList")]
        [OnValueChanged("OnSectionChange")]
        public int Section;

        [ReadOnly]
        public GameSectionTable.GameSectionRow SectionRow;

        [ReadOnly]
        public  List<int> SceneIds;

        [ReadOnly]
        public List<GameObject> Scenes;

        public IEnumerable GetSectionList(){
            return GameSupportEditorUtility.GetExcelTableOverviewIds<GameSectionTableOverview>();
        }

        public void ClearScene(){
            if(Scenes != null){
               
                foreach(var scene in Scenes){
                    try{
                        DestroyImmediate(scene);
                    }
                    catch{
                    }
                }
                Scenes.Clear();
            }
        }

        public void UpdateSection(){
            if(SceneIds == null){
                SceneIds = new List<int>();
            }

            if(Scenes == null){
                Scenes = new List<GameObject>();
            }
            ClearScene();
            if(Section == 0){
                return;
            }

            

            SceneIds.Clear();
            SectionRow = GameSupportEditorUtility.GetGameSectionRowById(Section);
            SceneIds.AddRange(SectionRow.DynamicSceneIds.ToArrInt());
            SceneIds.AddRange(SectionRow.KeepSceneIds.ToArrInt());

            foreach(var id in SceneIds){
                var staticScene = GameSupportEditorUtility.GetStaticSceneRowById(id);
                var assetPathRow = GameSupportEditorUtility.GetAssetPathRowById(staticScene.AssetPathId);
                // Debug.Log($"Try Create Prefab:{staticScene},{assetPathRow.ToPrefabPath()}");
                // Debug.Log($"AssetPath:{assetPath}");
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.transform.parent = transform;
                go.ResetTransfrom();
                Scenes.Add(go);
            }
        }

        void UpdateGameObjectName(){
            name = "$Static Scene";
            
            if(Section != 0){
                name = $"{name}-Section:{Section}";
            }

        }

        public void OnSectionChange(){
            UpdateSection();
            UpdateGameObjectName();
        }  

        private void OnEnable() {
            UpdateSection();
        }

        private void OnDisable() {

        }

        private void OnDestroy() {
            ClearScene();
        }

        private void Update(){
            UpdateGameObjectName();
            gameObject.ResetTransfrom();
        }

        public void SetSection(int id){
            Section = id;
            OnSectionChange();
        }

    }
}
#endif