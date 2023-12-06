using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System;

namespace Pangoo.Editor
{
    [Serializable]
    public class HierarchyIdEditor
    {
        public struct PrefabData
        {
            public GameObject gameObject;
            public String Path;

        }

        public string findFid;
        public string findPath;


        [ShowInInspector]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
        public Dictionary<GameObject, PrefabData> PrefabDatas = new Dictionary<GameObject, PrefabData>();

        [ShowInInspector]
        public HashSet<GameObject> Gos = new HashSet<GameObject>();

        void SearchPrefab(GameObject obj)
        {
            var path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(obj);
            // PrefabDatas.Add(obj, new PrefabData { gameObject = obj, Path = path });
            var assets = AssetDatabase.LoadAllAssetsAtPath(path);
            string guid;
            long fid;
            // Debug.Log($"assets:{assets.Length}");

            for (int i = 0; i < assets.Length; i++)
            {
                var asset = assets[i];
                // AssetDatabase
                var s = AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out guid, out fid);
                // Debug.Log($"assets:{obj.name}, fuid:{guid} , fid:{fid}");


                if (findFid != null && s && (fid.ToString().StartsWith(findFid) || guid.ToString().StartsWith(findFid)) && !Gos.Contains(obj))
                {
                    Gos.Add(obj);
                }

            }
        }


        [Button("查找")]
        void Search()
        {
            Gos.Clear();
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            // Debug.Log(allObjects.Length);
            // foreach (GameObject obj in allObjects)
            // {
            //     if (PrefabUtility.IsAnyPrefabInstanceRoot(obj) && !PrefabDatas.ContainsKey(obj))
            //     {
            //         SearchPrefab(obj);
            //     }
            // }
            foreach (GameObject obj in allObjects)
            {
                // if (!PrefabUtility.IsPartOfAnyPrefab(obj))
                // {
                Component[] components = obj.GetComponents<Component>();

                foreach (Component component in components)
                {
                    if (component == null) continue;
                    // 检查组件属性是否引用了目标 GameObject
                    SerializedObject serializedObject = new SerializedObject(component);
                    SerializedProperty property = serializedObject.GetIterator();
                    // Debug.Log(property.propertyPath);

                    while (property.Next(true))
                    {
                        // try
                        // {
                        //     Debug.Log($"managedReferenceId:{property.managedReferenceId}");
                        //     if (findFid != null && property.managedReferenceId.ToString().StartsWith(findFid))
                        //     {
                        //         Gos.Add(obj);
                        //     }
                        // }
                        // catch (Exception e)
                        // {

                        // }



                        if (property.propertyType == SerializedPropertyType.ManagedReference)
                        {
                            // var instranceId = property.objectReferenceInstanceIDValue;
                            // Debug.Log($"instranceId:{instranceId}");
                            // if (findFid != null && instranceId.ToString().StartsWith(findFid))
                            // {
                            //     Gos.Add(obj);
                            // }


                            if (findFid != null && property.managedReferenceId.ToString().StartsWith(findFid) && (findPath.IsNullOrWhiteSpace() || (!findPath.IsNullOrWhiteSpace() && property.propertyPath.StartsWith(findPath))))
                            {
                                Debug.Log($"managedReferenceId:{property.managedReferenceId},path:{property.propertyPath},pro:{property.managedReferenceFullTypename},{property.managedReferenceFieldTypename},{property.managedReferenceValue}");
                                Gos.Add(obj);
                            }
                        }
                        // if (property.propertyType == SerializedPropertyType.ObjectReference &&
                        //     property.objectReferenceValue == gameObject)
                        // {
                        //     referencingObjects.Add(obj);
                        // }

                        // if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == gameObject.transform)
                        // {
                        //     referencingTransform.Add(obj.transform);
                        // }
                    }
                }
                // }

            }

        }
    }
}