
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public partial class StaticSceneEditor : MonoBehaviour
    {
        [ReadOnly]
        [ValueDropdown("GetSectionList")]
        [OnValueChanged("OnSectionChange")]
        public int Section;

        [ReadOnly]
        public GameSectionTable.GameSectionRow SectionRow;

        [LabelText("动态场景IDs")]
        [ValueDropdown("StaticSceneIdValueDropdown", IsUniqueList = true)]

        [OnValueChanged("OnDynamicSceneIdsChanged")]
        [ListDrawerSettings(Expanded = true)]


        public List<int> DynamicSceneIds = new List<int>();


        public void OnDynamicSceneIdsChanged()
        {
            Debug.Log($"OnDynamicSceneIdsChanged");
            var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<GameSectionTableOverview>(Section);
            SectionRow.DynamicObjectIds = DynamicSceneIds.ToListString();
            EditorUtility.SetDirty(overview);
        }




        [LabelText("保持场景IDs")]
        [ValueDropdown("StaticSceneIdValueDropdown")]

        public List<int> KeepSceneIds = new List<int>();


        public IEnumerable StaticSceneIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<StaticSceneTableOverview>();
        }




        [ReadOnly]
        public List<GameObject> DynamicScenes;


        [ReadOnly]
        public List<GameObject> KeepScenes;

        public IEnumerable GetSectionList()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewIds<GameSectionTableOverview>();
        }

        public void ClearScene()
        {
            if (DynamicScenes != null)
            {

                foreach (var scene in DynamicScenes)
                {
                    try
                    {
                        DestroyImmediate(scene);
                    }
                    catch
                    {
                    }
                }
                DynamicScenes.Clear();
            }

            if (KeepScenes != null)
            {
                foreach (var scene in KeepScenes)
                {
                    try
                    {
                        DestroyImmediate(scene);
                    }
                    catch
                    {
                    }
                }
                KeepScenes.Clear();
            }
        }

        public void UpdateBase()
        {
            if (DynamicSceneIds == null)
            {
                DynamicSceneIds = new List<int>();
            }

            if (KeepSceneIds == null)
            {
                KeepSceneIds = new List<int>();
            }

            if (DynamicScenes == null)
            {
                DynamicScenes = new List<GameObject>();
            }

            if (KeepScenes == null)
            {
                KeepScenes = new List<GameObject>();
            }
        }

        public void UpdateScene(List<int> ids, List<GameObject> gameObjects)
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
                go.ResetTransfrom();
                gameObjects.Add(go);
            }
        }


        public void UpdateSection()
        {
            UpdateBase();
            ClearScene();
            if (Section == 0)
            {
                return;
            }

            DynamicSceneIds.Clear();
            KeepSceneIds.Clear();

            SectionRow = GameSupportEditorUtility.GetGameSectionRowById(Section);
            DynamicSceneIds.AddRange(SectionRow.DynamicSceneIds.ToArrInt());
            KeepSceneIds.AddRange(SectionRow.KeepSceneIds.ToArrInt());

            UpdateScene(DynamicSceneIds, DynamicScenes);
            UpdateScene(KeepSceneIds, KeepScenes);
        }

        void UpdateGameObjectName()
        {
            name = "$Static Scene";

            if (Section != 0)
            {
                name = $"{name}-Section:{Section}";
            }

        }

        public void OnSectionChange()
        {
            UpdateSection();
            UpdateGameObjectName();
        }

        private void OnEnable()
        {
            UpdateSection();
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
            OnSectionChange();
        }

    }
}
#endif