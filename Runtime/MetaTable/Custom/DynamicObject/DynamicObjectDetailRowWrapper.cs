#if UNITY_EDITOR

using System;
using System.Collections;
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
    public partial class DynamicObjectDetailRowWrapper : MetaTableDetailRowWrapper<DynamicObjectOverview, UnityDynamicObjectRow>
    {
        [ShowInInspector]
        [PropertyOrder(1)]
        public Space PositionSpace
        {
            get
            {
                return UnityRow.Row?.Space.ToEnum<Space>() ?? Space.Self;
            }
            set
            {
                UnityRow.Row.Space = value.ToString();
            }
        }

        [ShowInInspector]
        [PropertyOrder(1)]
        public Vector3 Postion
        {
            get
            {
                return UnityRow.Row?.Position ?? Vector3.zero;
            }
        }

        [ShowInInspector]
        [PropertyOrder(2)]
        public Vector3 Rotation
        {
            get
            {
                return UnityRow.Row?.Rotation ?? Vector3.zero;
            }
        }

        [ShowInInspector]
        [PropertyOrder(3)]
        public Vector3 Scale
        {
            get
            {
                return UnityRow.Row?.Scale ?? Vector3.one;
            }
        }


        [LabelText("资源ID")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [PropertyOrder(5)]
        [ShowInInspector]
        // [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        public int AssetPathId
        {
            get
            {
                return UnityRow.Row?.AssetPathId ?? 0;
            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.AssetPathId = value;
                    Save();
                }
            }

        }


        [LabelText("资源Uuid")]
        [ValueDropdown("AssetPathUuidValueDropdown")]
        [PropertyOrder(5)]
        [ShowInInspector]
        // [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
        public string AssetPathUuid
        {
            get
            {
                return UnityRow.Row.AssetPathUuid;
            }
            set
            {

                UnityRow.Row.AssetPathUuid = value;
                Save();
            }

        }

        [LabelText("默认隐藏模型")]
        [PropertyOrder(6)]
        [ShowInInspector]
        public bool DefaultHideModel
        {
            get
            {
                return UnityRow.Row?.DefaultHideModel ?? false;
            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.DefaultHideModel = value;
                    Save();
                }
            }

        }

        // void ShowCreateAssetPath()
        // {
        //     var assetOverview = GameSupportEditorUtility.GetExcelTableOverviewByConfig<AssetPathTableOverview>(Overview.Config);
        //     var assetNewObject = AssetPathNewWrapper.Create(assetOverview, Id, ConstExcelTable.DynamicObjectAssetTypeName, Name, afterCreateAsset: OnAfterCreateAsset);
        //     var window = OdinEditorWindow.InspectObject(assetNewObject);
        //     assetNewObject.Window = window;
        // }

        public void OnAfterCreateAsset(int id)
        {
            AssetPathId = id;
        }



        public IEnumerable AssetPathIdValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "DynamicObject" });
        }

        public IEnumerable AssetPathUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathUuids(assetTypes: new List<string> { "DynamicObject" });
        }



        GameObject m_Prefab;


        [ShowInInspector]
        [LabelText("资源预制体")]
        [ReadOnly]
        public GameObject Prefab
        {
            get
            {
                if (m_Prefab == null)
                {
                    m_Prefab = GameSupportEditorUtility.GetPrefabByAssetPathUuid(UnityRow.Row.AssetPathUuid);
                }
                return m_Prefab;
            }
            set
            {

            }
        }


        [Button("更新AssetPathUuid通过Id")]
        public void UpdateAssetPathUuidByAssetPathId()
        {
            var row = GameSupportEditorUtility.GetAssetPathById(AssetPathId);
            if (row != null)
            {
                AssetPathUuid = row.Uuid;
            }
        }

    }
}
#endif

