#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class CharacterDetailRowWrapper : MetaTableDetailRowWrapper<CharacterOverview, UnityCharacterRow>
    {
        // [LabelText("资源ID")]
        // [ValueDropdown("AssetPathIdValueDropdown")]
        // [ShowInInspector]
        // [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        // public int AssetPathId
        // {
        //     get
        //     {
        //         return UnityRow.Row?.AssetPathId ?? 0;
        //     }
        //     set
        //     {
        //         if (Row != null && Overview != null)
        //         {
        //             Row.AssetPathId = value;
        //             Save();
        //         }
        //     }

        // }

        // void ShowCreateAssetPath()
        // {
        //     var assetOverview = GameSupportEditorUtility.GetExcelTableOverviewByConfig<AssetPathTableOverview>(Overview.Config);
        //     var assetNewObject = AssetPathNewWrapper.Create(assetOverview, Id, ConstExcelTable.CharacterAssetTypeName, Name, afterCreateAsset: OnAfterCreateAsset);
        //     var window = OdinEditorWindow.InspectObject(assetNewObject);
        //     assetNewObject.Window = window;
        // }

        // public void OnAfterCreateAsset(int id)
        // {
        //     AssetPathId = id;
        // }


        // public IEnumerable AssetPathIdValueDropdown()
        // {
        //     return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "Character" });
        // }


        // [ShowInInspector]
        // public bool IsPlayer
        // {
        //     get
        //     {
        //         return Row?.IsPlayer ?? false;
        //     }
        //     set
        //     {
        //         if (Row != null && Overview != null)
        //         {
        //             Row.IsPlayer = value;
        //             Save();
        //         }
        //     }
        // }



        // [ShowInInspector]
        // [DelayedProperty]
        // public float LinearSpeed
        // {
        //     get
        //     {
        //         return Row?.MoveSpeed ?? 1;

        //     }
        //     set
        //     {
        //         if (Row != null && Overview != null)
        //         {
        //             Row.MoveSpeed = value;
        //             Save();
        //         }

        //     }
        // }


        // [ShowInInspector]
        // [DelayedProperty]
        // public Vector3 CameraOffset
        // {
        //     get
        //     {
        //         return Row?.CameraOffset ?? Vector3.zero;

        //     }
        //     set
        //     {
        //         if (Row != null && Overview != null)
        //         {
        //             Row.CameraOffset = value;
        //             Save();
        //         }

        //     }
        // }


        // [ShowInInspector]
        // [DelayedProperty]
        // public float XMaxPitch
        // {
        //     get
        //     {
        //         return Row?.XMaxPitch ?? 90;

        //     }
        //     set
        //     {
        //         if (Row != null && Overview != null)
        //         {
        //             Row.XMaxPitch = value;
        //             Save();
        //         }

        //     }
        // }

        // [ShowInInspector]
        // [DelayedProperty]
        // public float YMaxPitch
        // {
        //     get
        //     {
        //         return Row?.YMaxPitch ?? 360;

        //     }
        //     set
        //     {
        //         if (Row != null && Overview != null)
        //         {
        //             Row.YMaxPitch = value;
        //             Save();
        //         }

        //     }
        // }


        // [ShowInInspector]
        // [DelayedProperty]
        // [LabelText("只处理相机")]
        // public bool CameraOnly
        // {
        //     get
        //     {
        //         return Row?.CameraOnly ?? false;

        //     }
        //     set
        //     {
        //         if (Row != null && Overview != null)
        //         {
        //             Row.CameraOnly = value;
        //             Save();
        //         }

        //     }
        // }

    }
}
#endif

