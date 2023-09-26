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
    public class StaticSceneDetailWrapper : ExcelTableRowDetailWrapper<StaticSceneTableOverview, StaticSceneTable.StaticSceneRow>
    {

        [LabelText("资源ID")]
        [ValueDropdown("AssetPathIdValueDropdown")]
        [PropertyOrder(0)]
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
            return GameSupportEditorUtility.GetAssetPathIds(ids: new List<int> { AssetPathId }, assetTypes: new List<string> { "Scene" });
        }


        [LabelText("加载场景Ids")]
        [ValueDropdown("StaticSceneIdValueDropdown", IsUniqueList = true)]

        // [OnValueChanged("OnDynamicSceneIdsChanged")]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(1)]
        public int[] LoadSceneIds
        {
            get
            {
                return Row?.LoadSceneIds?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.LoadSceneIds = value.ToList().ToListString();
                    Save();
                }

            }
        }



        public IEnumerable StaticSceneIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<StaticSceneTableOverview>();
        }


    }
}