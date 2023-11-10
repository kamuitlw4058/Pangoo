
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
    public partial class GameSceneDynamicObjectEditor : MonoBehaviour
    {
        [ReadOnly]
        [ValueDropdown("GetSectionList")]
        public int Section;
        public void SetSection(int id)
        {
            Section = id;
        }

        public IEnumerable GetSectionList()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<GameSectionTableOverview>();
        }


        public void ClearObjects(List<GameObject> gameObjects)
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



        [ReadOnly]
        [ListDrawerSettings(Expanded = true)]
        public List<GameObject> DynamicObjects = new List<GameObject>();

        public void UpdateObjects(int[] ids)
        {
            ClearObjects(DynamicObjects);
            foreach (var id in ids)
            {
                var row = GameSupportEditorUtility.GetDynamicObjectRow(id);
                if (row == null)
                {
                    Debug.LogError($"staticScene Id:{id} is null");
                    continue;
                }

                var assetPathRow = GameSupportEditorUtility.GetAssetPathRowById(row.AssetPathId);
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.name = row.Name;
                go.transform.parent = transform;
                var helper = go.AddComponent<DynamicObjectEditor>();
                helper.DynamicObjectId = id;
                // go.ResetTransfrom();
                DynamicObjects.Add(go);
            }
        }


        void UpdateGameObjectName()
        {
            name = "///DynamicObject";

            if (Section != 0)
            {
                name = $"{name}-Section:{Section}";
            }

        }

        private void Update()
        {
            UpdateGameObjectName();
        }

        private void OnDisable()
        {
            ClearObjects(DynamicObjects);
        }

        private void OnDestroy()
        {
            ClearObjects(DynamicObjects);
        }


    }
}
#endif