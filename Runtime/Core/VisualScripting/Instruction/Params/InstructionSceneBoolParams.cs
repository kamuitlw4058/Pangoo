using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionSceneBoolParams : InstructionParams
    {
        [JsonMember("SceneUuid")]
        [ValueDropdown("OnSceneUuidDropdown")]
        public string SceneUuid;

        [JsonMember("Val")]
        public bool Val;

#if UNITY_EDITOR
        IEnumerable OnSceneUuidDropdown()
        {
            return StaticSceneOverview.GetUuidDropdown();
        }
#endif

    }
}