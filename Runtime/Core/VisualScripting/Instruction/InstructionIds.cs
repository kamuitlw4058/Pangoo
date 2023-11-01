using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    public struct InstrctionIds
    {
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [JsonMember("Ids")]
        public int[] Ids;
#if UNITY_EDITOR
        public IEnumerable InstructionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
        }
#endif
    }


}