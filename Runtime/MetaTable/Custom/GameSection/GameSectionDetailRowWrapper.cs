#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
using System.Linq;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class GameSectionDetailRowWrapper : MetaTableDetailRowWrapper<GameSectionOverview, UnityGameSectionRow>
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
                return UnityRow.Row?.DynamicSceneIds.ToSplitArr<int>();
            }
            set
            {

                UnityRow.Row.DynamicSceneIds = value.ToListString();
                Save();

            }
        }

        [LabelText("动态场景Uuids")]
        [ValueDropdown("StaticSceneUuidValueDropdown", IsUniqueList = true)]

        // [OnValueChanged("OnDynamicSceneIdsChanged")]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(1)]
        public string[] DynamicSceneUuids
        {
            get
            {
                return UnityRow.Row?.DynamicSceneUuids.ToSplitArr<string>();
            }
            set
            {

                UnityRow.Row.DynamicSceneUuids = value.ToListString();
                Save();

            }
        }

        public IEnumerable StaticSceneUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneUuids();
        }

        public IEnumerable StaticSceneIdDynamicValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneIds();
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
                return UnityRow.Row?.KeepSceneIds?.ToArrInt() ?? new int[0];
            }
            set
            {
                UnityRow.Row.KeepSceneIds = value.ToListString();
                Save();
            }
        }

        [LabelText("持续场景uuids")]
        [ValueDropdown("StaticSceneUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(2)]
        public string[] KeepSceneUuids
        {
            get
            {
                return UnityRow.Row.KeepSceneUuids.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.KeepSceneUuids = value.ToListString();
                Save();
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
                return UnityRow.Row?.InitSceneIds?.ToArrInt() ?? new int[0];
            }
            set
            {
                UnityRow.Row.InitSceneIds = value.ToListString();
                Save();
            }
        }

        [LabelText("初始化场景")]
        [ValueDropdown("StaticSceneUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(3)]
        public string[] InitSceneUuids
        {
            get
            {
                return UnityRow.Row.InitedInstructionUuids?.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.InitedInstructionUuids = value.ToListString();
                Save();
            }
        }


        public IEnumerable StaticSceneIdKeepValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneIds(excludeIds: DynamicSceneIds.ToList());
        }

        public IEnumerable StaticSceneIdValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneIds();
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

                return UnityRow.Row?.DynamicObjectIds?.ToArrInt() ?? new int[0];
            }
            set
            {
                UnityRow.Row.DynamicObjectIds = value.ToList().ToListString();
                Save();
            }
        }



        [LabelText("动态物体Uuids")]
        [ValueDropdown("DynamicObjectIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(4)]
        public string[] DynamicObjectUuids
        {
            get
            {

                return UnityRow.Row?.DynamicObjectUuids?.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.DynamicObjectUuids = value.ToListString();
                Save();
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
                return UnityRow.Row?.InitedInstructionIds?.ToArrInt() ?? new int[0];
            }
            set
            {


                UnityRow.Row.InitedInstructionIds = value.ToListString();
                Save();
            }
        }

        [LabelText("初始化完成指令Uuids")]
        [ValueDropdown("InstructionUuidsValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(5)]
        public string[] InstructionUuids
        {
            get
            {
                return UnityRow.Row.InitedInstructionUuids.ToSplitArr<string>();
            }
            set
            {


                UnityRow.Row.InitedInstructionUuids = value.ToListString();
                Save();
            }
        }

        [LabelText("编辑器初始化完成指令")]
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(5)]
        public int[] EditorInstructionIds
        {
            get
            {
                return UnityRow.Row?.EditorInitedInstructionIds?.ToArrInt() ?? new int[0];
            }
            set
            {


                UnityRow.Row.EditorInitedInstructionIds = value.ToListString();
                Save();
            }
        }

        [LabelText("编辑器初始化完成指令Uuids")]
        [ValueDropdown("InstructionUuidsValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [ShowInInspector]
        [PropertyOrder(5)]
        public string[] EditorInstructionUuids
        {
            get
            {
                return UnityRow.Row.EditorInitedInstructionUuids?.ToSplitArr<string>();
            }
            set
            {


                UnityRow.Row.EditorInitedInstructionUuids = value.ToListString();
                Save();
            }
        }


        public IEnumerable DynamicObjectIdValueDropdown()
        {
            return GameSupportEditorUtility.GetDynamicObjectIds();
        }

        public IEnumerable DynamicObjectUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetDynamicObjectUuids();
        }

        public IEnumerable InstructionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetInstructionIds();
        }

        public IEnumerable InstructionUuidsValueDropdown()
        {
            return GameSupportEditorUtility.GetInstructionUuids();
        }
    }
}
#endif

