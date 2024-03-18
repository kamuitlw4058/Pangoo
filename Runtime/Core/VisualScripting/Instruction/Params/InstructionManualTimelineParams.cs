using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;
using GameSupportEditorUtility = Pangoo.GameSupportEditorUtility;

namespace  Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionManualTimelineParams : InstructionParams
    {
        [JsonMember("Path")]
        [LabelText("路径列表")]
        [ListDrawerSettings(Expanded = true)]
        public string Path;

        [JsonMember("TimeFactor")]
        public float TimeFactor;
    }
}

