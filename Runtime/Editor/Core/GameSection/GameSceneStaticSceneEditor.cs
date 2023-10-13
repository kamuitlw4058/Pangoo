
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public partial class GameSceneStaticSceneEditor : MonoBehaviour
    {
        [ReadOnly]
        [ValueDropdown("GetSectionList")]
        public int Section;



        [ReadOnly]
        [ListDrawerSettings(Expanded = true)]
        public List<GameObject> DynamicScenes = new List<GameObject>();


        [ReadOnly]
        [ListDrawerSettings(Expanded = true)]
        public List<GameObject> KeepScenes = new List<GameObject>();

        public IEnumerable GetSectionList()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<GameSectionTableOverview>();
        }
        public void ClearScene(List<GameObject> gameObjects)
        {
            if (gameObjects != null)
            {

                foreach (var scene in gameObjects)
                {
                    try
                    {
                        DestroyImmediate(scene);
                    }
                    catch
                    {
                    }
                }
                gameObjects.Clear();
            }
        }

        public void ClearScene()
        {
            ClearScene(DynamicScenes);
            ClearScene(KeepScenes);
        }

        public void UpdateScene(int[] ids, List<GameObject> gameObjects)
        {
            foreach (var id in ids)
            {
                var staticScene = GameSupportEditorUtility.GetStaticSceneRowById(id);
                if (staticScene == null)
                {
                    Debug.LogError($"staticScene Id:{id} is null");
                    continue;
                }

                var assetPathRow = GameSupportEditorUtility.GetAssetPathRowById(staticScene.AssetPathId);
                // Debug.Log($"Try Create Prefab:{staticScene},{assetPathRow.ToPrefabPath()}");
                // Debug.Log($"AssetPath:{assetPath}");
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.transform.parent = transform;
                var helper = go.AddComponent<StaticSceneEditor>();
                helper.StaticSceneId = id;
                go.ResetTransfrom();
                gameObjects.Add(go);
            }
        }


        public void UpdateDynamicSceneIds(int[] ids)
        {
            ClearScene(DynamicScenes);
            UpdateScene(ids, DynamicScenes);
        }

        public void UpdateKeepSceneIds(int[] ids)
        {
            ClearScene(KeepScenes);
            UpdateScene(ids, KeepScenes);
        }



        void UpdateGameObjectName()
        {
            name = "$Static Scene";

            if (Section != 0)
            {
                name = $"{name}-Section:{Section}";
            }

        }



        private void OnEnable()
        {
        }

        private void OnDisable()
        {
            ClearScene();
        }

        private void OnDestroy()
        {
            ClearScene();
        }

        private void Update()
        {
            UpdateGameObjectName();
            gameObject.ResetTransfrom();
        }

        public void SetSection(int id)
        {
            Section = id;
            // OnSectionChange();
        }

    }
}
#endif