using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using Pangoo.Core.Common;
using UnityEngine;
using Pangoo.MetaTable;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class DirectInstructionList : IParams
    {
        [JsonMember("RefDynamicObjectUuid")]
        [OnValueChanged("OnRefDynamicObjectIdChanged")]
        [ValueDropdown("OnRefDynamicObjectIdDropdown")]
        [LabelText("参考动态对象")]
        public string RefDynamicObjectUuid;

        [JsonNoMember]
        [ReadOnly]

        public GameObject RefPrefab;

        [JsonMember("DirectInstructions")]
        [HideReferenceObjectPicker]
        [LabelText("指令列表")]
        [TableList(AlwaysExpanded = true)]
        [OnCollectionChanged(After = "OnDirectInstructionsChanged")]
        public DirectInstruction[] DirectInstructions = new DirectInstruction[0];

#if UNITY_EDITOR
        public void Init()
        {
            OnRefDynamicObjectIdChanged();
            if (!RefDynamicObjectUuid.IsNullOrWhiteSpace())
            {
                UpdatePrefab();
            }
        }

        public IEnumerable OnRefDynamicObjectIdDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }

        public void OnRefDynamicObjectIdChanged()
        {
            // Debug.Log($"RefDynamicObjectId Changed:{RefDynamicObjectId}");
            RefPrefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(RefDynamicObjectUuid);
            if (!RefDynamicObjectUuid.IsNullOrWhiteSpace())
            {
                UpdatePrefab();
            }

        }

        public void UpdatePrefab()
        {
            for (int i = 0; i < DirectInstructions.Length; i++)
            {
                Debug.Log($"Set di:{DirectInstructions[i].InstructionType} :{RefPrefab}");
                DirectInstructions[i].SetPrefab(RefPrefab);
            }
        }

        public void OnDirectInstructionsChanged(CollectionChangeInfo info, object value)
        {
            if (info.ChangeType == CollectionChangeType.Add
            || info.ChangeType == CollectionChangeType.Insert)
            {
                UpdatePrefab();
            }
            Debug.Log($"info:{info}, value:{value}");
        }
#endif


        public void Load(string val)
        {
            var list = JsonMapper.ToObject<DirectInstructionList>(val);
            RefDynamicObjectUuid = list.RefDynamicObjectUuid;
            DirectInstructions = list.DirectInstructions;
#if UNITY_EDITOR
            OnRefDynamicObjectIdChanged();
#endif
            if (DirectInstructions == null)
            {
                DirectInstructions = new DirectInstruction[0];
            }
            else
            {
#if UNITY_EDITOR
                UpdatePrefab();
#endif
            }


        }

        public InstructionList ToInstructionList(InstructionGetRowByUuidHandler handler = null)
        {
            if (DirectInstructions == null || DirectInstructions.Length == 0)
            {
                return null;
            }

            List<Instruction> ret = new List<Instruction>();
            for (int i = 0; i < DirectInstructions.Length; i++)
            {
                var instruction = DirectInstructions[i].ToInstruction(handler);
                // Debug.Log($"Instruction:{instruction}");
                if (instruction != null)
                {
                    ret.Add(instruction);
                }
            }
            return new InstructionList(ret.ToArray());
        }


        public static InstructionList LoadInstructionList(string val, InstructionGetRowByUuidHandler handler = null)
        {
            List<Instruction> ret = new List<Instruction>();
            var directInstructions = JsonMapper.ToObject<DirectInstructionList>(val);
            // Debug.Log($"directInstructions:{directInstructions},{directInstructions?.DirectInstructions?.Length}");
            if (directInstructions != null)
            {
                return directInstructions.ToInstructionList(handler);
            }
            Debug.Log($"directInstructions: return null");

            return null;
        }

        public string Save()
        {
            return JsonMapper.ToJson(this);
        }
    }

}