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
        [LabelText("场景Uuids")]
        [ValueDropdown("StaticSceneUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(ShowFoldout = true)]
        [ShowInInspector]
        [PropertyOrder(1)]
        public string[] SceneUuids
        {
            get
            {
                return UnityRow?.Row?.SceneUuids.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.SceneUuids = value.ToListString();
                Save();
            }
        }



        public IEnumerable StaticSceneUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetStaticSceneUuids();
        }


        [LabelText("动态物体Uuids")]
        [ValueDropdown("DynamicObjectUuidValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(4)]
        public string[] DynamicObjectUuids
        {
            get
            {

                return UnityRow?.Row?.DynamicObjectUuids?.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.DynamicObjectUuids = value.ToListString();
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
                return UnityRow?.Row.InitedInstructionUuids.ToSplitArr<string>();
            }
            set
            {


                UnityRow.Row.InitedInstructionUuids = value.ToListString();
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
                return UnityRow?.Row.EditorInitedInstructionUuids?.ToSplitArr<string>();
            }
            set
            {


                UnityRow.Row.EditorInitedInstructionUuids = value.ToListString();
                Save();
            }
        }



        public IEnumerable DynamicObjectUuidValueDropdown()
        {
            return GameSupportEditorUtility.GetDynamicObjectUuids();
        }


        public IEnumerable InstructionUuidsValueDropdown()
        {
            return GameSupportEditorUtility.GetInstructionUuids();
        }


    }
}
#endif

