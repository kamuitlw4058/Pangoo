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
        // [InlineButton("ShowCreateAssetPath", SdfIconType.Plus, Label = "")]
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
            return GameSupportEditorUtility.GetAssetPathIds(assetTypes: new List<string> { "DynamicObject" });
        }



        [LabelText("触发器Ids")]
        [ValueDropdown("TriggerIdValueDropdown", IsUniqueList = true)]

        // [OnValueChanged("OnDynamicSceneIdsChanged")]
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
                }

            }
        }

        public IEnumerable TriggerIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<TriggerEventTableOverview>();
        }



    }
}