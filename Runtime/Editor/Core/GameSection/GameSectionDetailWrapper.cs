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
                    Save();
                }

            }
        }


        [LabelText("初始化场景")]
        [ValueDropdown("StaticSceneIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(3)]
        public int[] InitSceneIds
        {
            get
            {
                return Row?.InitSceneIds?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.InitSceneIds = value.ToList().ToListString();
                    Save();
                }
            }
        }


        public IEnumerable StaticSceneIdKeepValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<StaticSceneTableOverview>(ids: DynamicSceneIds.ToList());
        }

        public IEnumerable StaticSceneIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<StaticSceneTableOverview>();
        }


        [LabelText("动态物体Ids")]
        [ValueDropdown("DynamicObjectIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(4)]
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


        [LabelText("初始化完成指令")]
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(5)]
        public int[] InstructionIds
        {
            get
            {
                return Row?.InitedInstructionIds?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.InitedInstructionIds = value.ToList().ToListString();
                    Save();
                }
            }
        }

        public IEnumerable DynamicObjectIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<DynamicObjectTableOverview>();
        }

        public IEnumerable InstructionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
        }



    }
}
#endif