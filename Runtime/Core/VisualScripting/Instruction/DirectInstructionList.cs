using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;
using System.Text;
using LitJson;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class DirectInstructionList : IParams
    {
        [JsonMember("DirectInstructions")]
        [HideReferenceObjectPicker]
        [LabelText("指令列表")]
        [TableList(AlwaysExpanded = true)]
        public DirectInstruction[] DirectInstructions = new DirectInstruction[0];


        public void Load(string val)
        {
            var list = JsonMapper.ToObject<DirectInstructionList>(val);
            DirectInstructions = list.DirectInstructions;
            if (DirectInstructions == null)
            {
                DirectInstructions = new DirectInstruction[0];
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