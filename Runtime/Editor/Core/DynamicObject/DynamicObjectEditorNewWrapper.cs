#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;
using Sirenix.Utilities;




using UnityEditor;
using Sirenix.OdinInspector.Editor;


namespace Pangoo
{
    public class DynamicObjectEditorNewWrapper : ExcelTableRowNewWrapper<DynamicObjectTableOverview, DynamicObjectTable.DynamicObjectRow>
    {

        // public void ConfirmCreate(int id, string name, string assetType, string assetName, GameObject prefab, string fileType)
        // {


        //     var fileName = $"{assetName}";
        //     var fullFileName = $"{fileName}.{fileType}";


        //     var assetPathRow = new AssetPathTable.AssetPathRow();
        //     assetPathRow.Id = id;
        //     assetPathRow.AssetPackageDir = Overview.Config.PackageDir;
        //     assetPathRow.AssetPath = fullFileName;
        //     assetPathRow.AssetType = assetType;
        //     assetPathRow.Name = name;
        //     Overview.Data.Rows.Add(assetPathRow);
        //     EditorUtility.SetDirty(Overview);



        //     var go = new GameObject(fileName);

        //     var prefab_go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        //     prefab_go.transform.parent = go.transform;
        //     prefab_go.ResetTransfrom(false);
        //     prefab_go.name = ConstExcelTable.SubModelName;

        //     // string prefabPath = "Assets/Prefabs/MyPrefab.prefab";
        //     PrefabUtility.SaveAsPrefabAsset(go, assetPathRow.ToPrefabPath());
        //     GameObject.DestroyImmediate(go);


        //     AssetDatabase.SaveAssets();
        //     if (AfterCreate != null)
        //     {
        //         AfterCreate(id);
        //     }


        // }

        // public override void Create()
        // {
        //     if (Id == 0 || Name.IsNullOrWhiteSpace() || AssetName.IsNullOrWhitespace())
        //     {
        //         EditorUtility.DisplayDialog("错误", "Id, Name, 命名空间,ArtPrefab  必须填写", "确定");
        //         // GUIUtility.ExitGUI();
        //         return;
        //     }


        //     if (StringUtility.ContainsChinese(AssetName))
        //     {
        //         EditorUtility.DisplayDialog("错误", "Name不能包含中文", "确定");
        //         // GUIUtility.ExitGUI();
        //         return;
        //     }

        //     if (StringUtility.IsOnlyDigit(AssetName))
        //     {
        //         EditorUtility.DisplayDialog("错误", "Name不能全是数字", "确定");
        //         return;
        //     }

        //     if (char.IsDigit(AssetName[0]))
        //     {
        //         EditorUtility.DisplayDialog("错误", "Name开头不能是数字", "确定");
        //         return;
        //     }

        //     if (CheckExistsId())
        //     {
        //         EditorUtility.DisplayDialog("错误", "Id已经存在", "确定");
        //         return;
        //     }

        //     if (CheckExistsName())
        //     {
        //         EditorUtility.DisplayDialog("错误", "Name已经存在", "确定");
        //         return;
        //     }

        //     ConfirmCreate(Id, Name, AssetType, AssetName, ModelPrefab, FileType);
        // }

    }
}
#endif