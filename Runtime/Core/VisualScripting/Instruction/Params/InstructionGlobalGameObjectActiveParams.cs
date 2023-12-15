using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionGlobalGameObjectActiveParams : InstructionParams
    {
        [JsonMember("Root")]
        public string Root;
        [JsonMember("RootChild")]
        public string RootChild;
        
        [JsonMember("Val")]
        public bool Val;
        
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionGlobalGameObjectActiveParams>(val);
            Root = par.Root;
            RootChild = par.RootChild;
            Val = par.Val;
        }
    }
}
