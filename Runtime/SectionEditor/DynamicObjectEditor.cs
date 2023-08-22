
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Editor{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class DynamicObjectEditor : MonoBehaviour
    {
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
            return GameSupportEditorUtility.GetGameSectionIds();
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
                var assetPackage = GameSupportEditorUtility.GetAssetPackageById(assetPathRow.AssetPackageId);
                var assetPath =  AssetUtility.GetStaticScene(assetPackage.AssetPackagePath,assetPathRow.AssetPath);
                // Debug.Log($"AssetPath:{assetPath}");
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPath);
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.transform.parent = transform;
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
        }

        public void SetSection(int id){
            Section = id;
            OnSectionChange();
        }

    }
}
#endif