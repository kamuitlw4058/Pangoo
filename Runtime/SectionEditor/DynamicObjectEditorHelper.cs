
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;


namespace Pangoo.Editor{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class DynamicObjectEditorHelper : MonoBehaviour
    {
       public int DynamicObjectId;



        [AssetsOnly]
       [AssetSelector(FlattenTreeView =false)]
       [OnValueChanged("OnValueChanged")]
       public GameObject ArtPrefab;

        public void BuildPrefab(){
            Debug.Log("");
        }

        void OnValueChanged(){
            foreach(var trans in transform.Children()){
                DestroyImmediate(trans);
            }

            if(ArtPrefab != null){
               var go = PrefabUtility.InstantiatePrefab(ArtPrefab) as GameObject;
                go.transform.parent = transform;
                go.ResetTransfrom();
            }
        }

    }


}
#endif