using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using Pangoo.Core.Common;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class DirectInstructionList : IParams
    {
        [JsonMember("RefDynamicObjectId")]
        [OnValueChanged("OnRefDynamicObjectIdChanged")]
        [ValueDropdown("OnRefDynamicObjectIdDropdown")]
        [LabelText("参考动态对象")]
        public int RefDynamicObjectId;

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
            if (RefDynamicObjectId != 0)
            {
                UpdatePrefab();
            }
        }

        public IEnumerable OnRefDynamicObjectIdDropdown()
        {
            return GameSupportEditorUtility.GetDynamicObjectIds();
        }

        public void OnRefDynamicObjectIdChanged()
        {
            Debug.Log($"RefDynamicObjectId Changed:{RefDynamicObjectId}");
            RefPrefab = GameSupportEditorUtility.GetPrefabByDynamicObjectId(RefDynamicObjectId);

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
            RefDynamicObjectId = list.RefDynamicObjectId;
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
                UpdatePrefab();
            }


        }

        public InstructionList ToInstructionList(InstructionTable table = null)
        {
            if (DirectInstructions == null || DirectInstructions.Length == 0)
            {
                return null;
            }

            List<Instruction> ret = new List<Instruction>();
            for (int i = 0; i < DirectInstructions.Length; i++)
            {
                var instruction = DirectInstructions[i].ToInstruction(table);
                Debug.Log($"Instruction:{instruction}");
                if (instruction != null)
                {
                    ret.Add(instruction);
                }
            }
            return new InstructionList(ret.ToArray());
        }


        public static InstructionList LoadInstructionList(string val, InstructionTable table = null)
        {
            List<Instruction> ret = new List<Instruction>();
            var directInstructions = JsonMapper.ToObject<DirectInstructionList>(val);
            Debug.Log($"directInstructions:{directInstructions},{directInstructions.DirectInstructions.Length}");
            if (directInstructions != null)
            {
                return directInstructions.ToInstructionList(table);
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