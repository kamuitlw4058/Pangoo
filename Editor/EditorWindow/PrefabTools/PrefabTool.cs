
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace Pangoo.Editor
{
    public class PrefabTool
    {
        [AssetsOnly]
        [AssetSelector(Paths = "Assets")]
        [OnValueChanged("Find")]
        public GameObject Prefab;

        
        public GameObject ChangeTargetObject;

        [ShowInInspector]
        [TableList]
        public List<Tuple<string,GameObject>> List = new List<Tuple<string,GameObject>>();

        //[ShowInInspector]
        public Dictionary<UnityEngine.Object,bool> Dict = new Dictionary<UnityEngine.Object, bool>();

        //[ShowInInspector]
        public Dictionary<string,bool> DictString = new Dictionary<string, bool>();



        public bool ChangeInactive;


        [Button("Find")]
        public void Find(){
            // var basePrefabAsset = UnityEditor.PrefabUtility.GetCorrespondingObjectFromSource(Prefab);
            // var basePath = AssetDatabase.GetAssetPath(basePrefabAsset);
            var basePath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(Prefab);
            Debug.Log($"basePath:{basePath}");
            List.Clear();
            // List = PrefabUtility.FindAllInstancesOfPrefab(Prefab).ToList();
            // PrefabUtility.prefab
                // List = PrefabUtility.GetAddedGameObjects(Prefab).ToList();
            int prefabInstanceCount = 0;
            int prefabAssetCount = 0;
            var prefabs = UnityEngine.Object.FindObjectsOfType<GameObject>();//場景中全部GameObject
            Debug.Log($"Prefabs:{prefabs.Count()}");
            Dict.Clear();
            var lst = new List<GameObject>();//用於避免重複的列表
            foreach (var gameObject in prefabs)//遍歷
            {

                if(!PrefabUtility.IsPartOfPrefabInstance(gameObject)){
                    continue;
                }
                prefabInstanceCount+=1;


                var prefab = PrefabUtility.GetPrefabParent(gameObject);//沒有對應的Prefab（可能被刪除掉了）
                if (prefab == null)
                    continue;


                var prefabRoot = PrefabUtility.GetNearestPrefabInstanceRoot(gameObject);

                if(!Dict.ContainsKey(prefabRoot)){
                    Dict.Add(prefabRoot,true);

                    // var prefabAsset  = PrefabUtility.GetCorrespondingObjectFromSource(prefabRoot);
                    // var path = AssetDatabase.GetAssetPath(prefabRoot);
                    var path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
                    if(!DictString.ContainsKey(path)){
                        DictString.Add(path,true);
                    }


                    if(path == basePath){
                        List.Add(new Tuple<string, GameObject>(prefabRoot.GetPath()  ,prefabRoot));
                    }

                }

                
                // var isprefab = PrefabUtility.GetPrefabType(gameObject);//物體的PrefabType
                // if (isprefab != PrefabType.PrefabInstance)//不是Prefab實例
                //     continue;

                


                // //Debug.Log(prefab);
            
                // if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(gameObject))
                // {
                //     // 获取预制体资源
                //     prefabAssetCount += 1;

                //     var root = PrefabUtility.FindPrefabRoot(gameObject);

                //     var prefabAsset = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);


                // if(!Dict.ContainsKey(root)){
                //     Dict.Add(root,true);
                // }
                //     var path = AssetDatabase.GetAssetPath(prefabAsset);

                //       if(!DictString.ContainsKey(path)){
                //         DictString.Add(path,true);
                //       }


                //     if(path == AssetDatabase.GetAssetPath(Prefab)){
                //     List.Add(prefabAsset);
                // }
                //     //   UnityEditor.AssetDatabase.GetAssetPath(prefabAsset);
                // }


                // var rt = PrefabUtility.FindPrefabRoot(gameObject);//物體的Prefab根節點
                // if (rt == null || lst.Contains(rt))//存在且不重複
                //     continue;
                // lst.Add(rt);//增長到列表中
                // //Debug.Log(rt.name);
                // PrefabUtility.ReplacePrefab(rt, prefab);//實現替換達到Apply的目的
            }

            Debug.Log($"prefabInstanceCount:{prefabInstanceCount} prefabAssetCount：{prefabAssetCount} Dict:{Dict.Count} DictString:{DictString.Count}");
                
        }

        [Button("Change")]
        public void ChangeObject()
        {
            if(ChangeTargetObject == null){
                return;
            }

            foreach(Tuple<string,GameObject> obj in List){
                var go = obj.Item2;
                if(ChangeInactive &&  !go.activeInHierarchy){
                    continue;
                }
                var parent =  go.transform.parent;
                var changed = (GameObject)PrefabUtility.InstantiatePrefab(ChangeTargetObject);
                changed.transform.SetParent(parent);
                changed.transform.localPosition = go.transform.localPosition;
                changed.transform.localRotation = go.transform.localRotation;
                changed.transform.localScale = go.transform.localScale;
                go.SetActive(false);
            }
        }

    }

}
