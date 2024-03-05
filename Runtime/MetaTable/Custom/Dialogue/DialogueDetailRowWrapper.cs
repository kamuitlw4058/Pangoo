#if UNITY_EDITOR

using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using MetaTable;
using UnityEditor;
using Pangoo.Core.VisualScripting;
using UnityEngine.Timeline;
using Pangoo.Timeline;


namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class DialogueDetailRowWrapper : MetaTableDetailRowWrapper<DialogueOverview, UnityDialogueRow>
    {
        DialogueType m_DialogueType;

        [ShowInInspector]
        [LabelText("对话类型")]
        public DialogueType CurrentDialogueType
        {
            get
            {
                if (m_DialogueType == DialogueType.None)
                {
                    m_DialogueType = UnityRow.Row.DialogueType.ToEnum<DialogueType>();
                }
                return m_DialogueType;
            }
            set
            {
                m_DialogueType = value;
                UnityRow.Row.DialogueType = value.ToString();
                Save();
            }
        }

        [ShowInInspector]
        [ValueDropdown("OnActorsLinesUuidDropdown")]
        [ShowIf("@this.CurrentDialogueType == DialogueType.ActorsLines")]
        [LabelText("台词")]
        public string ActorsLinesUuid
        {
            get
            {
                return UnityRow.Row.ActorsLinesUuid;
            }
            set
            {
                UnityRow.Row.ActorsLinesUuid = value;
                Save();
            }

        }

        IEnumerable OnActorsLinesUuidDropdown()
        {
            return ActorsLinesOverview.GetUuidDropdown();
        }

        List<DialogueOptionInfo> m_DialogueOptionInfos;

        [ShowInInspector]
        [ShowIf("@this.CurrentDialogueType == DialogueType.Option")]
        [HideReferenceObjectPicker]
        [TableList]
        [LabelText("选项")]
        [OnValueChanged("OnDialogueOptionInfosChanged", IncludeChildren = true)]
        public List<DialogueOptionInfo> DialogueOptionInfos
        {
            get
            {
                if (m_DialogueOptionInfos == null)
                {
                    try
                    {
                        m_DialogueOptionInfos = JsonMapper.ToObject<List<DialogueOptionInfo>>(UnityRow.Row.Options);
                    }
                    catch
                    {
                        m_DialogueOptionInfos = null;
                    }

                }


                if (m_DialogueOptionInfos == null)
                {
                    m_DialogueOptionInfos = new List<DialogueOptionInfo>();
                    SaveOptions();

                }


                return m_DialogueOptionInfos;
            }
            set
            {
                m_DialogueOptionInfos = value;
                SaveOptions();
            }
        }

        public void OnDialogueOptionInfosChanged()
        {
            SaveOptions();
        }

        void SaveOptions()
        {
            UnityRow.Row.Options = JsonMapper.ToJson(m_DialogueOptionInfos);
            Save();
        }


        [ShowInInspector]
        [LabelText("下一个对话")]
        [ValueDropdown("OnDialogueUuidDropdown")]
        [ShowIf("@this.CurrentDialogueType == DialogueType.ActorsLines")]

        public string NextDialogueUuid
        {
            get
            {
                return UnityRow.Row.NextDialogueUuid;
            }
            set
            {
                UnityRow.Row.NextDialogueUuid = value;
                Save();
            }

        }

        IEnumerable OnDialogueUuidDropdown()
        {
            return DialogueOverview.GetUuidDropdown(AdditionalOptions: new List<Tuple<string, string>>() { new Tuple<string, string>("结束", string.Empty) });
        }






    }


}
#endif

