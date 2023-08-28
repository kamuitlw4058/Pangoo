
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo;
using System;

namespace Pangoo.Editor{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public partial class DynamicObjectEditor : MonoBehaviour
    {
       public const string DynamicObjectAssetTypeName = "DynamicObject";
        public const int DynamicObjectAssetPathIdBase = 100000;

        [ReadOnly]
        [ValueDropdown("GetSectionList")]
        [OnValueChanged("OnSectionChange")]
        public int Section;

        private GameSectionTable.GameSectionRow SectionRow;

        private  List<int> DynamicObjectIds;

        [TableList(AlwaysExpanded =true)]
        public List<DynamicObjectWrapper> DyncObjectList = new List<DynamicObjectWrapper>();
        public IEnumerable GetSectionList(){
            return GameSupportEditorUtility.GetExcelTableOverviewIds<GameSectionTableOverview>();
        }



        public void ClearPrefabs(){
            foreach(var objects in DyncObjectList){
                try{
                    DestroyImmediate(objects.Go);
                }
                catch{
                }
            }
            DyncObjectList.Clear();
        }

        public void UpdateSection(){
            if(DynamicObjectIds == null){
                DynamicObjectIds = new List<int>();
            }

            ClearPrefabs();
            foreach(var go in transform.Children()){
                DestroyImmediate(go.gameObject);
            }

            if(Section == 0){
                return;
            }

            DynamicObjectIds.Clear();
            SectionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<GameSectionTableOverview,GameSectionTable.GameSectionRow>(Section);
            DynamicObjectIds.AddRange(SectionRow.DynamicObjectIds.ToArrInt());

            foreach(var id in DynamicObjectIds){
                var dynamicObjectRow = GameSupportEditorUtility.GetDynamicObjectRow(id);
                if(dynamicObjectRow == null){
                    Debug.LogError($"动态物体:{id} 没有对应的配置相关配置。请检查！！");
                    continue;
                }
                
                var assetPathRow = GameSupportEditorUtility.GetAssetPathRowById(dynamicObjectRow.AssetPathId);
                if(assetPathRow == null){
                    Debug.LogError($"动态物体:{id} 资源路径无效。请检查！！");
                    continue;
                }

                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.transform.parent = transform;
                go.transform.localPosition = dynamicObjectRow.Position;
                go.transform.localRotation = Quaternion.Euler(dynamicObjectRow.Rotation);
                // go.ResetTransfrom();
                DyncObjectList.Add(new DynamicObjectWrapper(this,SectionRow,dynamicObjectRow,go));
            }
        }

        void UpdateGameObjectName(){
            name = "///DynamicObject";
            
            if(Section != 0){
                name = $"{name}-Section:{Section}";
            }

        }

        public void OnSectionChange(){
            UpdateSection();
            UpdateGameObjectName();
        }  

        private void OnEnable() {
            UpdateSection();
        }

        private void OnDisable() {

        }

        private void OnDestroy() {
            // ClearScene();
        }

        private void Update(){
            UpdateGameObjectName();
            gameObject.ResetTransfrom();

        }

        public void SetSection(int id){
            Section = id;
            OnSectionChange();
        }

        [Serializable]
        public class DynamicObjectWrapper{
            [ShowInInspector]
            [TableTitleGroup("Id",Order =1)]
            [HideLabel]
            public int Id{
                get{
                    return m_Row.Id;
                }
            }


            DynamicObjectTable.DynamicObjectRow m_Row;
            GameSectionTable.GameSectionRow m_SectionRow;
            DynamicObjectEditor m_Editor;


            GameObject m_Go;


            [ReadOnly]
            [TableTitleGroup("对象",Order =2)]
            [HideLabel]
            [ShowInInspector]
            public GameObject Go{
                get{
                    return m_Go;
                }

            }

            public DynamicObjectWrapper(DynamicObjectEditor editor, GameSectionTable.GameSectionRow SectionRow, DynamicObjectTable.DynamicObjectRow row,GameObject go){
                m_SectionRow = SectionRow;
                m_Row = row;
                m_Go = go;
                m_Editor = editor;
            }


            [Button("删除引用")]
            [TableTitleGroup("删除",Order =3)]
            [TableColumnWidth(120,resizable:false)]
            public void Remove(){
                // var dynamicObjectRow = GameSupportEditorUtility.GetDynamicObjectRow(Id);
                // var assetPathRow = GameSupportEditorUtility.GetAssetPathRowById(dynamicObjectRow.AssetPathId);
                var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<GameSectionTableOverview>(m_SectionRow.Id);
                if(overview == null){
                    Debug.LogError($"No Found overview:{m_SectionRow.Id}");
                    return;
                }
                m_SectionRow.RemoveDynamicObjectId(Id);
                m_Editor.OnSectionChange();
                EditorUtility.SetDirty(overview);
                AssetDatabase.SaveAssets();
                
            }

        }

       
    }
}
#endif