#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameFramework;

namespace Pangoo
{
    public class SimpleUIDetailWrapper : ExcelTableRowDetailWrapper<SimpleUITableOverview, SimpleUITable.SimpleUIRow>
    {

        [LabelText("资源ID")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [PropertyOrder(0)]
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

        public IEnumerable AssetPathIdValueDropdown()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "UI" });
        }


        public IEnumerable AssetPathIdValueDropdownWithoutSelf()
        {
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "UI" });
        }

        void ShowCreateAssetPath()
        {
            var assetOverview = GameSupportEditorUtility.GetExcelTableOverviewByConfig<AssetPathTableOverview>(Overview.Config);
            var assetNewObject = AssetPathNewWrapper.Create(assetOverview, Id, ConstExcelTable.UIAssetTypeName, Name, afterCreateAsset: OnAfterCreateAsset);
            var window = OdinEditorWindow.InspectObject(assetNewObject);
            assetNewObject.Window = window;
        }

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

                return Row?.UIParamsType;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.UIParamsType = value;
                    Row.Params = "{}";
                    Save();
                    UpdateUI();
                }
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
            if (Row.UIParamsType.IsNullOrWhiteSpace())
            {
                return;
            }

            var instanceType = Utility.Assembly.GetType(Row.UIParamsType);
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

            m_Instance.Load(Row.Params);
        }

        [ShowInInspector]
        [ReadOnly]
        public string Params
        {
            get
            {
                return Row?.Params;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.Params = value;
                    Save();
                }
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
            Instance.Load(Row.Params);
        }



    }


}
#endif