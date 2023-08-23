
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using UnityEditor.VersionControl;

namespace Pangoo.Editor{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class DynamicObjectEditor : MonoBehaviour
    {
        public string AssetTypeName = "DynamicObject";
        [ReadOnly]
        [ValueDropdown("GetSectionList")]
        [OnValueChanged("OnSectionChange")]
        public int Section;

        [ReadOnly]
        public GameSectionTable.GameSectionRow SectionRow;

        [ReadOnly]
        public  List<int> DynamicObjectIds;


        [ReadOnly]
        public  List<GameObject> Prefabs;
        public IEnumerable GetSectionList(){
            return GameSupportEditorUtility.GetGameSectionIds();
        }

        private  OdinEditorWindow m_CreateWindow;

        public void ClearPrefabs(){
            if(Prefabs != null){
               
                foreach(var go in Prefabs){
                    try{
                        DestroyImmediate(go);
                    }
                    catch{
                    }
                }
                Prefabs.Clear();
            }
        }

        public void UpdateSection(){
            if(DynamicObjectIds == null){
                DynamicObjectIds = new List<int>();
            }

            if(Prefabs == null){
                Prefabs = new List<GameObject>();
            }

            ClearPrefabs();
            if(Section == 0){
                return;
            }

            DynamicObjectIds.Clear();
            SectionRow = GameSupportEditorUtility.GetGameSectionRowById(Section);
            DynamicObjectIds.AddRange(SectionRow.DynamicObjectIds.ToArrInt());
            // SceneIds.AddRange(SectionRow.KeepSceneIds.ToArrInt());

            foreach(var id in DynamicObjectIds){
                var dynamicObjectRow = GameSupportEditorUtility.GetDynamicObjectRow(id);
                if(dynamicObjectRow == null){
                    Debug.LogError($"动态物体:{id} 没有对应的配置相关配置。请检查！！");
                    continue;
                }
                
                var assetPathRow = GameSupportEditorUtility.GetAssetPathRowById(dynamicObjectRow.AssetPathId);
                // var assetPackage = GameSupportEditorUtility.GetAssetPackageById(assetPathRow.AssetPackageId);
                var assetPath =  AssetUtility.GetAssetPath("",assetPathRow.AssetType, assetPathRow.AssetPath);
                // Debug.Log($"AssetPath:{assetPath}");
                var asset = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(assetPath);
                var go = PrefabUtility.InstantiatePrefab(asset) as GameObject;
                go.transform.parent = transform;
                go.ResetTransfrom();
                Prefabs.Add(go);
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

        [Button("新建")]
        public void BuildNewObject(){
             m_CreateWindow = OdinEditorWindow.InspectObject(new DynamicObjectCreateWindow(this));
            
            // m_CreateWindow = OdinEditorWindow.InspectObject(new VolumeCreateWindow());
            // var go = new GameObject();
            // go.transform.parent = transform;
            // var helper = go.AddComponent<DynamicObjectEditorHelper>();
            // helper.DynamicObjectId = 0;
            // Prefabs.Add(go);
        }

        public void ConfirmCreate(PackageConfig space,int id,string name,string name_cn,GameObject prefab){
            DynamicObjectTableOverview overview = AssetDatabaseUtility.FindAssetFirst<DynamicObjectTableOverview>(space.PackageDir);
            Debug.Log($"overview:{overview}, overview.{overview.Config.PackageDir}");
            AssetPathTableOverview assetPathTableOverview = AssetDatabaseUtility.FindAssetFirst<AssetPathTableOverview>(space.PackageDir);
            // assetPackageTableOverview.GetAssetPackageIdByConfig








             var row = new DynamicObjectTable.DynamicObjectRow();
            row.Id = id;
            row.Name = name;
            row.NameCn = name_cn;
            // row.AssetPathId = desc;
            overview.Data.Rows.Add(row);

            var prefab_name = $"{id}_{name}";
             var prefab_file_path = AssetUtility.GetPrefabPath(space.PackageDir,AssetTypeName,prefab_name);

            var assetPathRow = new AssetPathTable.AssetPathRow();
            assetPathRow.Id = id;
            assetPathRow.AssetPackageDir = space.PackageDir;
            assetPathRow.AssetPath = prefab_file_path;
            assetPathRow.AssetType = AssetTypeName;

            var go = new GameObject(prefab_name);
            go.transform.localPosition = Vector3.zero;

            var prefab_go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            prefab_go.transform.parent = go.transform;
            prefab_go.ResetTransfrom(false);

            // 设置GameObject作为预制体
           
            // string prefabPath = "Assets/Prefabs/MyPrefab.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, prefab_file_path);

        }


        public class DynamicObjectCreateWindow{
            
            [ValueDropdown("GetPackageConfig", ExpandAllMenuItems = true)]
            public PackageConfig Namespace;


            public IEnumerable GetPackageConfig(){
                return GameSupportEditorUtility.GetPackageConfig();
            }


            public int Id = 0;

            [LabelText("名字")]
            public string Name = "";

            [LabelText("中文名")]
            public string NameCn = "";

            
            [LabelText("美术资源")]
            [AssetsOnly]
            [AssetSelector]
            public GameObject ArtPrefab;




            DynamicObjectEditor m_Editor;

            public DynamicObjectCreateWindow(DynamicObjectEditor editor){
                m_Editor = editor;
            }

        


            public DynamicObjectCreateWindow(){
            }

            [Button("新建", ButtonSizes.Large)]
            public void Create(){

                if ( Id == 0 || Name.IsNullOrWhiteSpace() || ArtPrefab == null)
                {
                    EditorUtility.DisplayDialog("错误", "Id, Name, 命名空间必须填写", "确定");
                    // GUIUtility.ExitGUI();
                    return;
                }


                if(StringUtility.ContainsChinese(Name)){
                    EditorUtility.DisplayDialog("错误", "Name不能包含中文", "确定");
                    // GUIUtility.ExitGUI();
                    return;
                }

                if(StringUtility.IsOnlyDigit(Name)){
                    EditorUtility.DisplayDialog("错误", "Name不能全是数字", "确定");
                    return;
                }

                if(char.IsDigit(Name[0])){
                    EditorUtility.DisplayDialog("错误", "Name开头不能是数字", "确定");
                    return;
                }

                if(!GameSupportEditorUtility.ExistsExcelTableOverviewId<DynamicObjectTableOverview>(Id)){
                    EditorUtility.DisplayDialog("错误", "Id已经存在", "确定");
                    return;  
                }

                if(!GameSupportEditorUtility.ExistsExcelTableOverviewName<DynamicObjectTableOverview>(Name)){
                    EditorUtility.DisplayDialog("错误", "Name已经存在", "确定");
                    return;  
                }

                m_Editor.ConfirmCreate(Namespace,Id, Name,NameCn,ArtPrefab);
            }
        }

    }
}
#endif