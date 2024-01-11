using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.VisualScripting;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionDynamicObjectSetMaterialParams : InstructionParams
    {
        [JsonMember("TargetPath")]
        public string TargetPath;
        [JsonMember("Index")]
        public int Index;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionDynamicObjectSetMaterialParams>(val);
            TargetPath = par.TargetPath;
            Index = par.Index;
        }
    }
}

