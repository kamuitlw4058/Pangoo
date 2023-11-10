#if UNITY_EDITOR
using System.Collections.Generic;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;



namespace Pangoo
{
    [Serializable]
    public partial class DynamicObjectDetailWrapper : ExcelTableRowDetailWrapper<DynamicObjectTableOverview, DynamicObjectTable.DynamicObjectRow>
    {
        [ShowInInspector]
        [PropertyOrder(1)]
        public Space PositionSpace
        {
            get
            {
                return Row?.Space.ToEnum<Space>() ?? Space.Self;
            }
            set
            {
                Row.Space = value.ToString();
            }
        }

        [ShowInInspector]
        [PropertyOrder(1)]
        public Vector3 Postion
        {
            get
            {
                return Row?.Position ?? Vector3.zero;
            }
        }

        [ShowInInspector]
        [PropertyOrder(2)]
        public Vector3 Rotation
        {
            get
            {
                return Row?.Rotation ?? Vector3.zero;
            }
        }

        [ShowInInspector]
        [PropertyOrder(3)]
        public Vector3 Scale
        {
            get
            {
                return Row?.Scale ?? Vector3.one;
            }
        }


        [LabelText("资源ID")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [PropertyOrder(5)]
        [ShowInInspector]
        [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        public int AssetPathId
        {
            get
            {
                return Row?.AssetPathId ?? 0;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.AssetPathId = value;
                    Save();
                }
            }

        }

        [LabelText("默认隐藏模型")]
        [PropertyOrder(6)]
        [ShowInInspector]
        public bool DefaultHideModel
        {
            get
            {
                return Row?.DefaultHideModel ?? false;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.DefaultHideModel = value;
                    Save();
                }
            }

        }

        void ShowCreateAssetPath()
        {
            var assetOverview = GameSupportEditorUtility.GetExcelTableOverviewByConfig<AssetPathTableOverview>(Overview.Config);
            var assetNewObject = AssetPathNewWrapper.Create(assetOverview, Id, ConstExcelTable.DynamicObjectAssetTypeName, Name, afterCreateAsset: OnAfterCreateAsset);
            var window = OdinEditorWindow.InspectObject(assetNewObject);
            assetNewObject.Window = window;
        }

        public void OnAfterCreateAsset(int id)
        {
            AssetPathId = id;
        }


        public IEnumerable AssetPathIdValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "DynamicObject" });
        }

        [ShowInInspector]
        [LabelText("资源预制体")]
        public GameObject AssetPrefab
        {
            get
            {
                return GameSupportEditorUtility.GetPrefabByAssetPathId(AssetPathId);
            }
        }





    }
}
#endif