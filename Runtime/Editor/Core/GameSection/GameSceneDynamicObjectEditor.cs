
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

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

        public void UpdateObjects(string[] uuids)
        {
            ClearObjects(DynamicObjects);
            foreach (var uuid in uuids)
            {
                var row = DynamicObjectOverview.GetUnityRowByUuid(uuid);
                if (row == null)
                {
                    Debug.LogError($"staticScene Id:{uuid} is null");
                    continue;
                }

                var assetPathRow = AssetPathOverview.GetUnityRowByUuid(row.Row.AssetPathUuid);
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.name = row.Name;
                go.transform.parent = transform;
                var helper = go.AddComponent<DynamicObjectEditor>();
                helper.DynamicObjectUuid = uuid;
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
            if (!Application.isPlaying)
            {
                UpdateGameObjectName();
            }
        }

        private void OnDisable()
        {
            ClearObjects();
        }

        private void OnDestroy()
        {
            ClearObjects();
        }

        public void ClearObjects()
        {
            ClearObjects(DynamicObjects);
        }


    }
}
#endif