
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo;
using Pangoo.MetaTable;

namespace Pangoo.Editor
{
    public partial class GameSceneStaticSceneEditor : MonoBehaviour
    {
        public const string SubModelName = "Model";
        private OdinEditorWindow m_CreateRowWindow;
        private static OdinEditorWindow m_CreateAssetPathWindow;



        [Button("创建新的静态场景", ButtonSizes.Large)]
        public void BuildDynamicObject()
        {
            m_CreateRowWindow = OdinEditorWindow.InspectObject(new StaticSceneCreateWindow(this));
        }

        // public void ConfirmCreate(string uuid, string name, int assetPathId, List<int> NeedLoadStaticScenes)
        // {


        //     StaticSceneOverview overview = StaticSceneOverview.GetOverviewByUuid(uuid);
        //     if (overview == null)
        //     {
        //         return;
        //     }

        //     var row = new StaticScene
        //     row.Id = id;
        //     row.Name = name;
        //     row.EntityGroupId = 1;
        //     row.AssetPathId = assetPathId;
        //     row.LoadSceneIds = NeedLoadStaticScenes.ToListString();
        //     overview.Data.Rows.Add(row);
        //     EditorUtility.SetDirty(overview);


        //     AssetDatabase.SaveAssets();
        //     //OnSectionChange();
        // }


        public class StaticSceneCreateWindow
        {

            public int Id = 0;

            [LabelText("场景名字")]
            public string Name = "";



            [LabelText("资源ID")]
            [ValueDropdown("AssetPathIdValueDropdown")]
            [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
            public int AssetPathId;

            [ValueDropdown("StaticSceneIdValueDropdown")]
            public List<int> NeedLoadStaticScenes;

            public IEnumerable AssetPathIdValueDropdown()
            {
                return GameSupportEditorUtility.GetAssetPathIds(ids: new List<int> { AssetPathId }, assetTypes: new List<string> { "Scene" });
            }

            public IEnumerable StaticSceneIdValueDropdown()
            {
                return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<StaticSceneTableOverview>(excludeIds: new List<int> { Id });
            }


            // public void ShowCreateAssetPath()
            // {
            //     PackageConfig config = GameSupportEditorUtility.GetPakcageConfigByOverviewRowId<GameSectionTableOverview>(m_Editor.Section);
            //     var window = new AssetPathWrapper(config, Id, ConstExcelTable.StaticSceneAssetTypeName, Name, ConstExcelTable.PrefabType, AfterCreateAsset);
            //     m_CreateAssetPathWindow = OdinEditorWindow.InspectObject(window);
            // }

            public void AfterCreateAsset(int id)
            {
                AssetPathId = id;
                m_CreateAssetPathWindow.Close();
            }




            GameSceneStaticSceneEditor m_Editor;



            public StaticSceneCreateWindow(GameSceneStaticSceneEditor editor)
            {
                m_Editor = editor;
            }


            // [Button("新建", ButtonSizes.Large)]
            // public void Create()
            // {

            //     if (Id == 0 || Name.IsNullOrWhiteSpace())
            //     {
            //         EditorUtility.DisplayDialog("错误", "Id, Name, 命名空间  必须填写", "确定");
            //         // GUIUtility.ExitGUI();
            //         return;
            //     }


            //     if (GameSupportEditorUtility.ExistsExcelTableOverviewId<StaticSceneTableOverview>(Id))
            //     {
            //         EditorUtility.DisplayDialog("错误", "Id已经存在", "确定");
            //         return;
            //     }


            //     m_Editor.ConfirmCreate(Id, Name, AssetPathId, NeedLoadStaticScenes);
            // }
        }
    }
}
#endif