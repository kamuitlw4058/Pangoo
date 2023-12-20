#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections.Generic;
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
    public partial class GameSectionRowWrapper : MetaTableRowWrapper<GameSectionOverview, GameSectionNewRowWrapper, UnityGameSectionRow>
    {
        [ShowInInspector]
        [TableTitleGroup("动态场景")]
        public int DynamicSceneCount
        {
            get
            {
                return UnityRow.Row.DynamicSceneUuids.ToSplitArr<string>().Count();
            }
        }

        [ShowInInspector]
        [TableTitleGroup("持续场景")]

        public int KeeySceneCount
        {
            get
            {
                return UnityRow.Row.KeepSceneUuids.ToSplitArr<string>().Count();
            }
        }

        [ShowInInspector]
        [TableTitleGroup("初始化场景")]

        public int InitSceneCount
        {
            get
            {
                return UnityRow.Row.InitSceneUuids.ToSplitArr<string>().Count();
            }
        }

        [ShowInInspector]
        [TableTitleGroup("动态物体")]

        public int DynamicObjectCount
        {
            get
            {
                return UnityRow.Row.DynamicObjectUuids.ToSplitArr<string>().Count();
            }
        }

        [ShowInInspector]
        [TableTitleGroup("初始化指令")]

        public int InitInstructionCount
        {
            get
            {
                return UnityRow.Row.InitedInstructionUuids.ToSplitArr<string>().Count();
            }
        }


        [ShowInInspector]
        [TableTitleGroup("初始化编辑器指令")]

        public int EditorInitInstructionCount
        {
            get
            {
                return UnityRow.Row.EditorInitedInstructionUuids.ToSplitArr<string>().Count();
            }
        }


    }
}
#endif

