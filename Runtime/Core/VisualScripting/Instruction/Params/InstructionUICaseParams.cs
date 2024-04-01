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
    public class InstructionUICaseParams : InstructionParams
    {
        [JsonMember("CaseUuid")]
        [ValueDropdown("@CasesOverview.GetUuidDropdown()")]
        public string CaseUuid;


        [JsonMember("WaitClosed")]
        public bool WaitClosed;

    }
}