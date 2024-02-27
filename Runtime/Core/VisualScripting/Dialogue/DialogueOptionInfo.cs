using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class DialogueOptionInfo
    {
        [JsonMember("Option")]
        [LabelText("选项文字")]
        public string Option;

        [JsonMember("NextDialogueUuid")]
        [LabelText("下一个对话")]
        [ValueDropdown("OnDialogueUuidDropdown")]
        public string NextDialogueUuid;

#if UNITY_EDITOR
        IEnumerable OnDialogueUuidDropdown()
        {
            return DialogueOverview.GetUuidDropdown(AdditionalOptions: new List<Tuple<string, string>>() { new Tuple<string, string>("结束", string.Empty) });
        }
#endif


    }
}