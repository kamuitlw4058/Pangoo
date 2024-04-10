
#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;
using Pangoo.Core.Characters;
using System.Linq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using Pangoo.Common;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public partial class GameSceneCharacterEditor : MonoBehaviour
    {
        [HideInInspector]
        public GameSectionDetailRowWrapper GameSectionWrapper;

        // [Serializable]
        public class CharacterBornData
        {
            public GameObject Target;
            public CharacterBornInfo BornInfo;

            public CharacterBornEditor BornEditor;
        }



        // [ShowInInspector]
        // [ReadOnly]
        public Dictionary<int, CharacterBornData> BornDict = new();


        public void UpdateTargets()
        {
            if (GameSectionWrapper.BornDict == null || GameSectionWrapper.BornDict.Count == 0) return;


            BornDict.SyncKeyValue(GameSectionWrapper.BornDict.Keys.ToList(),
            (key, val) =>
            {
                if (!val.Target.transform.IsChildOf(transform))
                {
                    return false;
                }

                if (GameSectionWrapper.BornDict.TryGetValue(key, out CharacterBornInfo bornInfo))
                {
                    return bornInfo.PlayerUuid.NullOrWhiteSpaceEquals(val.BornInfo.PlayerUuid);
                }

                return false;
            },
            (key) =>
            {
                if (GameSectionWrapper.BornDict.TryGetValue(key, out CharacterBornInfo info))
                {
                    var unityRow = CharacterOverview.GetCharacterRowByUuid(info.PlayerUuid);
                    if (unityRow != null)
                    {
                        var assetPathRow = AssetPathOverview.GetUnityRowByUuid(unityRow.Row.AssetPathUuid);
                        if (assetPathRow == null) return null;
                        var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                        if (asset == null) return null;
                        var data = new CharacterBornData();
                        var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                        go.name = unityRow.Name + $"_{key}";
                        go.transform.parent = transform;
                        var helper = go.AddComponent<CharacterBornEditor>();
                        helper.BornInfo = info;
                        helper.SaveAction = Save;
                        helper.UpdateTransformByBornInfo();
                        data.Target = go;
                        data.BornEditor = helper;
                        data.BornInfo = info;
                        return data;
                    }

                }
                return null;
            },
            (key, val) =>
            {
                if (val.Target != null)
                {
                    DestroyImmediate(val.Target);
                }
            });

            BornDict.Values.ToList().ForEach(o =>
            {
                if (o.BornEditor != null)
                {
                    o.BornEditor.SaveAction = Save;
                }
            });

            var childern = transform.Children();
            var bornTrans = BornDict.Values.ToList().Select(o => o.Target?.transform).Where(o => o != null);

            foreach (var child in childern)
            {
                if (!bornTrans.Contains(child))
                {
                    DestroyImmediate(child.gameObject);
                }

            }
        }

        public void Save()
        {
            GameSectionWrapper?.OnBornDictChanged();
        }


        void UpdateGameObjectName()
        {
            name = "$$Player";
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                UpdateGameObjectName();
                UpdateTargets();

            }
            else
            {
                Clear();
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

        public void Clear()
        {
            var Vals = BornDict.Values.ToList();
            BornDict.Clear();
            foreach (var val in Vals)
            {
                if (val.Target != null)
                {
                    DestroyImmediate(val.Target);
                }
            }

        }

    }
}
#endif