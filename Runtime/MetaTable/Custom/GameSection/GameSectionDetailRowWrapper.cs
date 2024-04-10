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
using Pangoo.Core.Characters;
using Pangoo.Core.VisualScripting;

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


        [ShowInInspector]
        [LabelText("状态变量")]
        [FoldoutGroup("玩家配置")]
        [OnValueChanged("@VariablesOverview.GetVariableUuidDropdown(VariableValueTypeEnum.Int.ToString(), VariableTypeEnum.Global.ToString(),false)")]
        public string StateVariable
        {
            get
            {
                return UnityRow.Row.StateVariableUuid;
            }
            set
            {
                UnityRow.Row.StateVariableUuid = value;
            }
        }


        [ShowInInspector]
        [LabelText("出生字符串")]
        [FoldoutGroup("玩家配置")]
        public string BornDictString
        {
            get
            {
                return UnityRow.Row.PlayerBirthPlaceList;
            }
        }

        Dictionary<int, CharacterBornInfo> m_BornDict;

        [ShowInInspector]
        [LabelText("出生列表")]
        [FoldoutGroup("玩家配置")]
        [OnValueChanged("OnBornDictChanged", includeChildren: true)]
        public Dictionary<int, CharacterBornInfo> BornDict
        {
            get
            {
                if (m_BornDict == null)
                {
                    try
                    {
                        m_BornDict = JsonMapper.ToObject<Dictionary<int, CharacterBornInfo>>(UnityRow.Row.PlayerBirthPlaceList);
                    }
                    catch
                    {

                    }
                    if (m_BornDict == null)
                    {
                        m_BornDict = new Dictionary<int, CharacterBornInfo>();
                    }
                }

                return m_BornDict;
            }
            set
            {
                m_BornDict = value;
                OnBornDictChanged();
            }
        }

        public void OnBornDictChanged()
        {
            Debug.Log($"OnBornDictChanged");
            if (m_BornDict == null)
            {
                m_BornDict = new Dictionary<int, CharacterBornInfo>();
            }

            try
            {
                UnityRow.Row.PlayerBirthPlaceList = JsonMapper.ToJson(m_BornDict);
            }
            catch
            {

            }

            Save();
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

