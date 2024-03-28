#if UNITY_EDITOR

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
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




        [LabelText("资源Uuid")]
        [ValueDropdown("AssetPathUuidValueDropdown")]
        [PropertyOrder(5)]
        [ShowInInspector]
        [InlineButton("AddAssetPath", SdfIconType.Plus, Label = "")]
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

        [LabelText("模型列表")]
        [PropertyOrder(6)]
        [ShowInInspector]
        [ValueDropdown("@GameSupportEditorUtility.RefPrefabStringDropdown(Prefab, false)")]

        public string[] ModelList
        {
            get
            {
                return UnityRow.Row.ModelList?.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.ModelList = value.ToListString();
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

        void AddAssetPath()
        {
            var assetOverview = AssetDatabaseUtility.FindAssetFirst<AssetPathOverview>(Overview.Config.StreamResScriptableObjectDir);
            Debug.Log($"assetOverview:{assetOverview} Overview.RowDirPath:{Overview.RowDirPath}");
            if (assetOverview != null)
            {
                var assetNewObject = AssetPathNewRowWrapper.Create(assetOverview, ConstExcelTable.DynamicObjectAssetTypeName, Name, afterCreateAsset: OnAfterCreateAsset);
                assetNewObject.MenuWindow = MenuWindow;
                var window = OdinEditorWindow.InspectObject(assetNewObject);
                assetNewObject.OpenWindow = window;
            }
        }

        public void OnAfterCreateAsset(string uuid)
        {
            Debug.Log($"OnAfterCreateAsset:{uuid}");
            AssetPathUuid = uuid;
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




    }
}
#endif

