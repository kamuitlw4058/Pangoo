#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System;


using UnityEditor;
using Sirenix.OdinInspector.Editor;



namespace Pangoo
{
    [Serializable]
    public class DynamicObjectDetailWrapper : ExcelTableRowDetailWrapper<DynamicObjectTableOverview, DynamicObjectTable.DynamicObjectRow>
    {
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

        [LabelText("资源ID")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [PropertyOrder(3)]
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



        [LabelText("触发器Ids")]
        [ValueDropdown("TriggerIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(4)]
        public int[] TriggerIds
        {
            get
            {
                return Row?.TriggerEventIds?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.TriggerEventIds = value.ToList().ToListString();
                    Save();
                    BuildTriggers();
                }

            }
        }

        public void BuildTriggers()
        {
            m_Triggers.Clear();
            foreach (var trigger in TriggerIds)
            {
                var wrapper = new TriggerDetailWrapper();
                wrapper.Id = trigger;
                var overview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<TriggerEventTableOverview>(trigger);
                var row = GameSupportEditorUtility.GetExcelTableRowWithOverviewById<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>(trigger);
                wrapper.Overview = overview;
                wrapper.Row = row;
                Triggers.Add(wrapper);
            }
        }

        List<TriggerDetailWrapper> m_Triggers;

        [ShowInInspector]
        [LabelText("触发器实现")]
        [ListDrawerSettings(HideAddButton = true, HideRemoveButton = true)]
        [PropertyOrder(5)]
        [HideReferenceObjectPicker]
        public List<TriggerDetailWrapper> Triggers
        {
            get
            {
                if (m_Triggers == null)
                {
                    m_Triggers = new List<TriggerDetailWrapper>();
                    BuildTriggers();
                }
                return m_Triggers;
            }
            set
            {
                m_Triggers = value;
                BuildTriggers();
            }
        }


        public IEnumerable TriggerIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<TriggerEventTableOverview>();
        }

        public bool UseHotspot
        {
            get
            {
                return Row?.UseHotspot ?? false;
            }
        }



    }
}
#endif