
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pangoo.Core.Common
{
    public class PrefabBakeHelper : MonoBehaviour
    {
        [InlineEditor]
        [InlineButton("NewAsset")]
        public PrefabBakeInfoAsset Asset;

        [Button]
        void Apply()
        {
            if (Asset == null) return;
            foreach (var bakeInfo in Asset.bakeInfos)
            {
                var trans = transform.Find(bakeInfo.Path);
                if (trans != null)
                {
                    var renderer = trans.GetComponent<MeshRenderer>();
                    if (bakeInfo.lightmapIndex != -1)
                    {
                        renderer.lightmapIndex = bakeInfo.lightmapIndex;
                        renderer.lightmapScaleOffset = bakeInfo.lightmapScaleOffset;
                    }

                    if (bakeInfo.realtimeLightmapIndex != -1)
                    {
                        renderer.realtimeLightmapIndex = bakeInfo.realtimeLightmapIndex;
                        renderer.realtimeLightmapScaleOffset = bakeInfo.realtimeLightmapScaleOffset;
                    }

                }

            }
        }


#if UNITY_EDITOR

        public void NewAsset()
        {
            var so = ScriptableObject.CreateInstance<PrefabBakeInfoAsset>();
            Asset = so;
            var path = AssetUtility.GetPrefabLightmapsData(ConstString.MainPackagePath, name);
            if (!File.Exists(path))
            {
                AssetDatabase.CreateAsset(so, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogError($"Path:{path} is exists!");
            }

        }


        [Button]
        void Clear()
        {
            Asset.Clear();
        }

        [Button]
        void GenerateLightmapInfo()
        {
            if (Asset == null) return;

            var renderers = GetComponentsInChildren<MeshRenderer>();
            var path = transform.GetPath();

            foreach (MeshRenderer renderer in renderers)
            {
                var rendererPath = renderer.transform.GetPath();
                BakeInfo info = new BakeInfo();
                info.lightmapIndex = -1;
                info.realtimeLightmapIndex = -1;
                info.Path = rendererPath.Substring(path.Length + 1);

                if (renderer.lightmapIndex != -1)
                {
                    info.lightmapIndex = renderer.lightmapIndex;
                    info.lightmapScaleOffset = renderer.lightmapScaleOffset;
                }



                if (renderer.realtimeLightmapIndex != -1)
                {
                    info.realtimeLightmapIndex = renderer.realtimeLightmapIndex;
                    info.realtimeLightmapScaleOffset = renderer.realtimeLightmapScaleOffset;
                }

                if (info.lightmapIndex != -1 && info.realtimeLightmapIndex != -1)
                {
                    Asset.bakeInfos.Add(info);
                }

            }

        }

#endif
    }
}