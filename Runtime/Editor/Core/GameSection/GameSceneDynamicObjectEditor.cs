
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
        List<string> m_Uuids = new List<string>();
        public List<string> Uuids
        {
            get
            {
                return m_Uuids;
            }
            set
            {

                Clear();
                if (value == null)
                {
                    m_Uuids.Clear();
                }
                else
                {
                    m_Uuids = value;
                }
            }
        }

        public void Clear()
        {
            if (DynamicObjects != null)
            {

                foreach (var scene in DynamicObjects.Values)
                {
                    try
                    {
                        if (scene != null)
                        {
                            DestroyImmediate(scene);
                        }
                    }
                    catch
                    {
                    }
                }
                DynamicObjects.Clear();
            }
            foreach (var child in transform.Children())
            {
                try
                {
                    DestroyImmediate(child.gameObject);
                }
                catch
                {
                }
            }

        }





        [ReadOnly]
        [ListDrawerSettings(Expanded = true)]
        public Dictionary<string, GameObject> DynamicObjects = new();

        public void UpdateObjects()
        {

            DynamicObjects.SyncKey(Uuids, (uuid) =>
            {
                var row = DynamicObjectOverview.GetUnityRowByUuid(uuid);
                if (row == null)
                {
                    Debug.LogError($"DynmaicObject Uuid:{uuid} is null");
                    return null;
                }

                var assetPathRow = AssetPathOverview.GetUnityRowByUuid(row.Row.AssetPathUuid);
                if(assetPathRow == null) return null;
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                if (asset == null) return null;
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.name = row.Name;
                go.transform.parent = transform;
                var helper = go.AddComponent<DynamicObjectEditor>();
                helper.DynamicObjectUuid = uuid;
                return go;
            });


        }


        void UpdateGameObjectName()
        {
            name = "///DynamicObject";
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                UpdateGameObjectName();
                UpdateObjects();
            }
        }

        private void OnDisable()
        {
            Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }

    }
}
#endif