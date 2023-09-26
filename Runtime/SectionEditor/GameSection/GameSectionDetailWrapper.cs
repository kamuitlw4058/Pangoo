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
using System;

namespace Pangoo
{
    [Serializable]
    public class GameSectionDetailWrapper : ExcelTableRowDetailWrapper<GameSectionTableOverview, GameSectionTable.GameSectionRow>
    {
        [LabelText("动态场景Ids")]
        [ValueDropdown("StaticSceneIdDynamicValueDropdown", IsUniqueList = true)]

        // [OnValueChanged("OnDynamicSceneIdsChanged")]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(1)]
        public int[] DynamicSceneIds
        {
            get
            {
                return Row?.DynamicSceneIds?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.DynamicSceneIds = value.ToList().ToListString();
                    Save();
                }

            }
        }

        public IEnumerable StaticSceneIdDynamicValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<StaticSceneTableOverview>(ids: KeepSceneIds.ToList());
        }


        [LabelText("持续场景Ids")]
        [ValueDropdown("StaticSceneIdKeepValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(2)]
        public int[] KeepSceneIds
        {
            get
            {
                return Row?.KeepSceneIds?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.KeepSceneIds = value.ToList().ToListString();
                    Debug.Log($"Keep changed");
                    Save();
                }

            }
        }

        public IEnumerable StaticSceneIdKeepValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<StaticSceneTableOverview>(ids: DynamicSceneIds.ToList());
        }


        [LabelText("动态物体Ids")]
        [ValueDropdown("DynamicObjectIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(3)]
        public int[] DynamicObjectIds
        {
            get
            {
                return Row?.DynamicObjectIds?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.DynamicObjectIds = value.ToList().ToListString();
                    Save();
                }

            }
        }
        public IEnumerable DynamicObjectIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
        }




    }
}