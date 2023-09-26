
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo;
using System;

namespace Pangoo.Editor
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public partial class DynamicObjectEditor : MonoBehaviour
    {

        [ReadOnly]
        [ValueDropdown("GetSectionList")]
        [OnValueChanged("OnSectionChange")]
        public int Section;

        private GameSectionTable.GameSectionRow SectionRow;

        private List<int> DynamicObjectIds;

        // [TableList(AlwaysExpanded =true)]
        [Searchable]
        [ListDrawerSettings(HideAddButton = true, DraggableItems = false, Expanded = true, HideRemoveButton = true)]
        [TableList(AlwaysExpanded = true)]
        public List<GameSectionDynamicObjectWrapper> DyncObjectList = new List<GameSectionDynamicObjectWrapper>();
        public IEnumerable GetSectionList()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewIds<GameSectionTableOverview>();
        }



        public void ClearPrefabs()
        {
            foreach (var objects in DyncObjectList)
            {
                try
                {
                    DestroyImmediate(objects.Go);
                }
                catch
                {
                }
            }
            DyncObjectList.Clear();
        }

        public void UpdateSection()
        {
            if (DynamicObjectIds == null)
            {
                DynamicObjectIds = new List<int>();
            }

            ClearPrefabs();
            foreach (var go in transform.Children())
            {
                DestroyImmediate(go.gameObject);
            }

            if (Section == 0)
            {
                return;
            }

            DynamicObjectIds.Clear();
            SectionRow = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<GameSectionTableOverview, GameSectionTable.GameSectionRow>(Section);
            DynamicObjectIds.AddRange(SectionRow.DynamicObjectIds.ToArrInt());

            foreach (var id in DynamicObjectIds)
            {
                var dynamicObjectRow = GameSupportEditorUtility.GetDynamicObjectRow(id);
                if (dynamicObjectRow == null)
                {
                    Debug.LogError($"动态物体:{id} 没有对应的配置相关配置。请检查！！");
                    SectionRow.RemoveDynamicObjectId(id);
                    continue;
                }

                var assetPathRow = GameSupportEditorUtility.GetAssetPathRowById(dynamicObjectRow.AssetPathId);
                if (assetPathRow == null)
                {
                    Debug.LogError($"动态物体:{id} 资源路径无效。请检查！！");
                    continue;
                }

                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPathRow.ToPrefabPath());
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.transform.parent = transform;
                go.transform.localPosition = dynamicObjectRow.Position;
                go.transform.localRotation = Quaternion.Euler(dynamicObjectRow.Rotation);
                var helper = go.AddComponent<DynamicObjectEditorHelper>();
                helper.DynamicObjectId = id;

                // go.ResetTransfrom();
                DyncObjectList.Add(new GameSectionDynamicObjectWrapper(this, SectionRow, dynamicObjectRow, go));
            }
        }

        void UpdateGameObjectName()
        {
            name = "///DynamicObject";

            if (Section != 0)
            {
                name = $"{name}-Section:{Section}";
            }

        }

        public void OnSectionChange()
        {
            UpdateSection();
            UpdateGameObjectName();
        }

        private void OnEnable()
        {
            UpdateSection();
        }

        private void OnDisable()
        {

        }

        private void OnDestroy()
        {
            // ClearScene();
        }

        private void Update()
        {
            UpdateGameObjectName();
            gameObject.ResetTransfrom();

        }

        public void SetSection(int id)
        {
            Section = id;
            OnSectionChange();
        }

        [Serializable]
        public class GameSectionDynamicObjectWrapper
        {



            [ShowInInspector]
            [TableTitleGroup("动态物体")]
            [HideLabel]
            public DynamicObjectWrapper m_Wrapper;


            DynamicObjectTable.DynamicObjectRow m_Row;
            GameSectionTable.GameSectionRow m_SectionRow;
            DynamicObjectEditor m_Editor;


            GameObject m_Go;


            [ReadOnly]
            [ShowInInspector]
            [HideLabel]
            [TableTitleGroup("对象")]
            [TableColumnWidth(80, resizable: false)]
            public GameObject Go
            {
                get
                {
                    return m_Go;
                }

            }

            public GameSectionDynamicObjectWrapper(DynamicObjectEditor editor, GameSectionTable.GameSectionRow SectionRow, DynamicObjectTable.DynamicObjectRow row, GameObject go)
            {
                m_SectionRow = SectionRow;
                m_Row = row;
                m_Go = go;
                m_Editor = editor;
                m_Wrapper = new DynamicObjectWrapper(row);
            }


            [Button("删除引用")]
            [TableTitleGroup("操作")]
            [TableColumnWidth(80, resizable: false)]
            [PropertyOrder(10)]
            public virtual void Remove()
            {
                var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<GameSectionTableOverview>(m_SectionRow.Id);
                if (overview == null)
                {
                    Debug.LogError($"No Found overview:{m_SectionRow.Id}");
                    return;
                }
                m_SectionRow.RemoveDynamicObjectId(m_Row.Id);
                m_Editor.OnSectionChange();
                EditorUtility.SetDirty(overview);
                AssetDatabase.SaveAssets();

            }




        }


    }
}
#endif