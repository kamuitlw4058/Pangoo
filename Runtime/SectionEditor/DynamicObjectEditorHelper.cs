
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine.PlayerLoop;

namespace Pangoo.Editor{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class DynamicObjectEditorHelper : MonoBehaviour
    {
    [ReadOnly]
       public int DynamicObjectId;
    [ReadOnly]
       public DynamicObjectTable.DynamicObjectRow Row;


    [ReadOnly]
    [AssetsOnly]
       [AssetSelector(FlattenTreeView =false)]
    //    [OnValueChanged("OnValueChanged")]
       public GameObject ArtPrefab;


       void Start(){
            Row = GameSupportEditorUtility.GetDynamicObjectRow(DynamicObjectId);
       }
        // void OnValueChanged(){
        //     foreach(var trans in transform.Children()){
        //         DestroyImmediate(trans);
        //     }

        //     if(ArtPrefab != null){
        //        var go = PrefabUtility.InstantiatePrefab(ArtPrefab) as GameObject;
        //         go.transform.parent = transform;
        //         go.ResetTransfrom();
        //     }
        // }

        private void Update() {
            Row.Position = transform.localPosition;
            Row.Rotation = transform.localRotation.eulerAngles;
        }

    }


}
#endif