
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using Pangoo.MetaTable;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public partial class GameSceneStaticSceneEditor : MonoBehaviour
    {
        [ReadOnly]
        [ValueDropdown("GetSectionList")]
        public string Section;



        [ReadOnly]
        [ShowInInspector]
        public Dictionary<string, GameObject> Scenes = new();

        List<string> m_Uuids = new List<string>();

        public List<string> Uuids
        {
            get
            {
                return m_Uuids;
            }
            set
            {

                ClearScene();
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

        public IEnumerable GetSectionList()
        {
            return GameSectionOverview.GetUuidDropdown();
        }
        public void ClearScene()
        {
            if (Scenes != null)
            {

                foreach (var scene in Scenes.Values)
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
                Scenes.Clear();
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



        public void UpdateScene()
        {
            Scenes.SyncKey(Uuids, (uuid) =>
            {
                var staticScene = StaticSceneOverview.GetUnityRowByUuid(uuid);
                if (staticScene == null)
                {
                    Debug.LogError($"staticScene Id:{uuid} is null");
                    return null;
                }

                var assetPathRow = AssetPathOverview.GetUnityRowByUuid(staticScene.Row.AssetPathUuid);
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.transform.parent = transform;
                go.name = staticScene.Name;
                var helper = go.GetOrAddComponent<StaticSceneEditor>();
                helper.StaticSceneUuid = uuid;
                //go.ResetTransfrom();
                return go;
            });


        }



        void UpdateGameObjectName()
        {
            name = "$Static Scene";

        }



        private void OnEnable()
        {
            if (Application.isPlaying)
            {
                ClearScene();
            }
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
            if (!Application.isPlaying)
            {
                UpdateGameObjectName();
                UpdateScene();
            }
            //gameObject.ResetTransfrom();
        }

        public void SetSection(string uuid)
        {
            Section = uuid;
        }

    }
}
#endif