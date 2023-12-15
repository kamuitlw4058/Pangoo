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
using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class SimpleUIDetailRowWrapper : MetaTableDetailRowWrapper<SimpleUIOverview, UnitySimpleUIRow>
    {

        [LabelText("资源Uuid")]
        [ValueDropdown("AssetPathUuidValueDropdown")]
        [PropertyOrder(0)]
        [ShowInInspector]
        public string AssetPathUuid
        {
            get
            {
                return UnityRow.Row?.AssetPathUuid;
            }
            set
            {

                UnityRow.Row.AssetPathUuid = value;
                Save();
            }

        }

        [LabelText("资源ID")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [PropertyOrder(0)]
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

                UnityRow.Row.AssetPathId = value;
                Save();
            }

        }

        public IEnumerable AssetPathIdValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "UI" });
        }

        public IEnumerable AssetPathUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathUuids(assetTypes: new List<string> { "UI" });
        }



        // void ShowCreateAssetPath()
        // {
        //     var assetOverview = GameSupportEditorUtility.GetExcelTableOverviewByConfig<AssetPathTableOverview>(Overview.Config);
        //     var assetNewObject = AssetPathNewWrapper.Create(assetOverview, Id, ConstExcelTable.UIAssetTypeName, Name, afterCreateAsset: OnAfterCreateAsset);
        //     var window = OdinEditorWindow.InspectObject(assetNewObject);
        //     assetNewObject.Window = window;
        // }

        public void OnAfterCreateAsset(int id)
        {
            AssetPathId = id;
        }

        [ShowInInspector]
        [ValueDropdown("GetUIParamsType")]
        public string UIParamsType
        {
            get
            {
                if (m_Instance == null)
                {
                    UpdateUI();
                }

                return UnityRow.Row?.UIParamsType;
            }
            set
            {

                UnityRow.Row.UIParamsType = value;
                UnityRow.Row.Params = "{}";
                Save();
                UpdateUI();
            }
        }

        public IEnumerable GetUIParamsType()
        {
            return GameSupportEditorUtility.GetUIParamsType();
        }




        UIPanelParams m_Instance;

        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        public UIPanelParams Instance
        {
            get
            {
                return m_Instance;
            }
            set
            {
                m_Instance = value;
            }
        }


        void UpdateUI()
        {
            if (UnityRow.Row.UIParamsType.IsNullOrWhiteSpace())
            {
                return;
            }

            var instanceType = AssemblyUtility.GetType(UnityRow.Row.UIParamsType);
            if (instanceType == null)
            {
                return;
            }

            m_Instance = Activator.CreateInstance(instanceType) as UIPanelParams;
            if (m_Instance == null)
            {
                return;
            }
            // Debug.Log($"instanceType:{instanceType} Row.UIType:{Row.UIParamsType} m_Instance:{m_Instance}");

            m_Instance.Load(UnityRow.Row.Params);
        }

        [ShowInInspector]
        [ReadOnly]
        public string Params
        {
            get
            {
                return UnityRow.Row?.Params;
            }
            set
            {
                UnityRow.Row.Params = value;
                Save();
            }
        }



        [Button("保存参数")]
        [TableColumnWidth(80, resizable: false)]
        public void SaveParams()
        {
            Params = Instance.Save();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            Instance.Load(UnityRow.Row.Params);
        }

        [Button("升级到Uuid")]
        public void UpgradeUuid()
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

