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

namespace Pangoo.Editor
{
    public class AssetPathOverviewEditor
    {
        public const string TagName = "GlobalVolume";

        [ReadOnly]
        [ShowInInspector]
        public static VolumeComponent m_VolumeComponent;



        [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
        private readonly List<VolumeWrapper> m_AllWrappers = new List<VolumeWrapper>();

        private static OdinMenuEditorWindow m_Window;

        private static OdinEditorWindow m_CreateWindow;

        // private static List<VolumeTable.VolumeRow> m_Rows = new List<VolumeTable.VolumeRow>();

        private static List<VolumeTableOverview> m_Overviews;

        [ShowInInspector]
        public List<Volume> m_Volumes;

        public AssetPathOverviewEditor(List<VolumeTableOverview> overviews, OdinMenuEditorWindow window)
        {
            m_Overviews = overviews;
            foreach (var overview in m_Overviews)
            {
                m_AllWrappers.AddRange(overview.Data.Rows.Select(x => new VolumeWrapper(overview, x)).ToList());
            }
            // m_AllWrappers = rows.Select(x => new VolumeWrapper(x)).ToList();
            m_Window = window;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
            m_Volumes = GameObject.FindObjectsByType<Volume>(FindObjectsSortMode.None).ToList();
            foreach (var volume in m_Volumes)
            {
                if (volume.tag != TagName)
                {
                    GameObject.DestroyImmediate(volume.gameObject);
                }
            }
        }

        ~AssetPathOverviewEditor()
        {
        }

        private void OnHierarchyChanged()
        {
            // 当Hierarchy面板发生改动时，该方法将被调用
            Debug.Log("Hierarchy changed");
            m_Volumes = GameObject.FindObjectsByType<Volume>(FindObjectsSortMode.None).ToList();


            foreach (var volume in m_Volumes)
            {
                if (volume.tag != TagName)
                {
                    GameObject.DestroyImmediate(volume);
                }
            }
            // 在这里可以执行相应的逻辑操作
        }

        [Button("新建", ButtonSizes.Large)]
        public void ShowCreateWindow()
        {
            m_CreateWindow = OdinEditorWindow.InspectObject(new VolumeCreateWindow());
        }

        public class VolumeWrapper
        {

            VolumeTable.VolumeRow m_Row;


            VolumeTableOverview m_Overview;


            public VolumeWrapper(VolumeTableOverview overview, VolumeTable.VolumeRow row)
            {
                m_Row = row;
                m_Overview = overview;

                var path = AssetUtility.GetVolumeProfile(m_Overview.Config.PackageDir, Name);
                m_VolumeProfile = AssetDatabaseUtility.LoadAssetAtPath<VolumeProfile>(path);

                var prefab_path = AssetUtility.GetVolumePrefab(m_Overview.Config.PackageDir, Name);
                m_Prefab = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(prefab_path);

            }

            [ShowInInspector]
            [TableColumnWidth(60, resizable: false)]
            [TableTitleGroup("命令空间")]
            [HideLabel]
            public string Namespace
            {
                get
                {
                    return m_Overview.Config.MainNamespace;
                }
            }

            [ShowInInspector]
            [TableColumnWidth(60, resizable: false)]
            public int Id
            {
                get { return m_Row.Id; }
            }

            [ShowInInspector]
            public string Name
            {
                get { return m_Row.Name; }
            }

            [ShowInInspector]
            public string NameCn
            {
                get { return m_Row.NameCn; }
                set
                {
                    m_Row.NameCn = value;
                    EditorUtility.SetDirty(m_Overview);
                    // m_TimelineInfo.TimelineName = value;
                    // EditorUtility.SetDirty(m_UnityTimelineInfo);
                }
            }

            [ShowInInspector]
            public string Desc
            {
                get { return m_Row.Desc; }

            }


            [ShowInInspector]
            [TableTitleGroup("使用组件")]
            [HideLabel]
            public string OverrideComponents
            {
                get
                {
                    string ret = null;
                    // Debug.Log($"OverrideComponents:{m_VolumeProfile.components.Count}");
                    foreach (var com in m_VolumeProfile.components)
                    {
                        if (ret == null)
                        {
                            ret = com.name;
                        }
                        else
                        {
                            ret = $"{ret},{com.name}";
                        }
                    }
                    return ret;
                }
            }

            [Button("编辑")]
            [TableColumnWidth(60, resizable: false)]
            public void Profile()
            {
                m_Window.TrySelectMenuItemWithObject(m_VolumeProfile);

            }

            [Button("删除")]
            [TableColumnWidth(60, resizable: false)]
            public void Delete()
            {
                AssetDatabaseUtility.DeleteAsset(m_VolumeProfile);
                var prefab_path = AssetUtility.GetVolumePrefab(m_Overview.Config.PackageDir, m_Row.Name);
                var prefab = AssetDatabaseUtility.LoadAssetAtPath<GameObject>(prefab_path);
                AssetDatabaseUtility.DeleteAsset(prefab);
                m_Overview.Data.Rows.Remove(m_Row);
                EditorUtility.SetDirty(m_Overview);
            }

            [Button("应用")]
            [TableColumnWidth(60, resizable: false)]
            public void Apply()
            {

                GameMainConfigEditorUtility.SwitchDefaultScene();
                m_VolumeComponent = GameObject.FindObjectOfType<VolumeComponent>();
                GameObject instance = PrefabUtility.InstantiatePrefab(m_Prefab) as GameObject;
                if (m_VolumeComponent != null)
                {
                    instance.transform.parent = m_VolumeComponent.transform;
                }
                instance.transform.localPosition = Vector3.zero;
            }
            [ReadOnly]
            [TableTitleGroup("配置文件")]
            [HideLabel]
            public VolumeProfile m_VolumeProfile;

            [ReadOnly]
            [TableTitleGroup("预制体")]
            [HideLabel]
            public GameObject m_Prefab;

        }


        private static void ConfirmCreate(VolumeTableOverview overview, int id, string name, string name_cn, string desc)
        {
            var packageDir = overview.Config.PackageDir;
            var volume_dir_path = AssetUtility.GetVolumeProfileDir(packageDir);
            var prefab_dir_path = AssetUtility.GetVolumePrefabDir(packageDir);

            // var volumePath = $"{PackageDir}";
            DirectoryUtility.ExistsOrCreate(volume_dir_path);
            DirectoryUtility.ExistsOrCreate(prefab_dir_path);

            var volumeProfile = ScriptableObject.CreateInstance<VolumeProfile>();
            var volume_file_path = PathUtility.Join(volume_dir_path, $"{name}.asset");
            AssetDatabase.CreateAsset(volumeProfile, volume_file_path);

            var go = new GameObject(name);
            go.transform.localPosition = Vector3.zero;
            var volume = go.AddComponent<Volume>();
            volume.isGlobal = true;
            volume.weight = 1;
            volume.sharedProfile = volumeProfile;
            go.tag = "GlobalVolume";
            // go.tag


            // 设置GameObject作为预制体
            var prefab_file_path = PathUtility.Join(prefab_dir_path, $"{name}.prefab");
            // string prefabPath = "Assets/Prefabs/MyPrefab.prefab";
            PrefabUtility.SaveAsPrefabAsset(go, prefab_file_path);

            GameObject.DestroyImmediate(go);

            var row = new VolumeTable.VolumeRow();
            row.Id = id;
            row.Name = name;
            row.NameCn = name_cn;
            row.Desc = desc;
            overview.Data.Rows.Add(row);
            EditorUtility.SetDirty(overview);

            m_CreateWindow.Close();
        }


        public class VolumeCreateWindow
        {

            public int Id = 0;

            [LabelText("名字")]
            public string Name = "";

            [LabelText("中文名")]
            public string NameCn = "";

            [LabelText("描述")]
            public string Desc = "";

            [ValueDropdown("GetAllVolumeOverview", ExpandAllMenuItems = true)]
            public VolumeTableOverview Namespace;


            public IEnumerable GetAllVolumeOverview()
            {
                return GameSupportEditorUtility.GetAllVolumeOverview();
            }
            public VolumeCreateWindow()
            {
            }

            [Button("新建", ButtonSizes.Large)]
            public void Create()
            {

                if (Namespace == null || Id == 0 || Name.IsNullOrWhitespace())
                {
                    EditorUtility.DisplayDialog("错误", "Id, Name, 命名空间必须填写", "确定");
                    // GUIUtility.ExitGUI();
                    return;
                }


                if (StringUtility.ContainsChinese(Name))
                {
                    EditorUtility.DisplayDialog("错误", "Name不能包含中文", "确定");
                    // GUIUtility.ExitGUI();
                    return;
                }

                if (StringUtility.IsOnlyDigit(Name))
                {
                    EditorUtility.DisplayDialog("错误", "Name不能全是数字", "确定");
                    return;
                }

                if (char.IsDigit(Name[0]))
                {
                    EditorUtility.DisplayDialog("错误", "Name开头不能是数字", "确定");
                    return;
                }

                if (!GameSupportEditorUtility.CheckVolumeId(Id))
                {
                    EditorUtility.DisplayDialog("错误", "VolumeId已经存在", "确定");
                    return;
                }

                if (!GameSupportEditorUtility.CheckVolumeDupName(Name))
                {
                    EditorUtility.DisplayDialog("错误", "Volume Name已经存在", "确定");
                    return;
                }

                ConfirmCreate(Namespace, Id, Name, NameCn, Desc);
            }
        }


    }
}