using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class PrefabEditorUtility 
{
    public static List<GameObject> GetAllInstancesInHierachy(string path){
            int prefabInstanceCount = 0;
            var gos = UnityEngine.Object.FindObjectsOfType<GameObject>();//場景中全部GameObject
            var ret = new List<GameObject>();//用於避免重複的列表
            var Dict = new Dictionary<GameObject,bool>();

            foreach (var gameObject in gos)//遍歷
            {

                if(!PrefabUtility.IsPartOfPrefabInstance(gameObject)){
                    continue;
                }

                
                prefabInstanceCount+=1;

                var prefabInstanceRoot = PrefabUtility.GetNearestPrefabInstanceRoot(gameObject);
                if(!Dict.ContainsKey(prefabInstanceRoot)){
                    Dict.Add(prefabInstanceRoot,true);

                    // var prefabAsset  = PrefabUtility.GetCorrespondingObjectFromSource(prefabRoot);
                    // var path = AssetDatabase.GetAssetPath(prefabRoot);
                    var instancePath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
                    if(instancePath == path){
                        ret.Add(prefabInstanceRoot);
                    }
                }
            }

            return ret;

            //Debug.Log($"prefabInstanceCount:{prefabInstanceCount} prefabAssetCount：{prefabAssetCount} Dict:{Dict.Count} DictString:{DictString.Count}");
                
        }

}
