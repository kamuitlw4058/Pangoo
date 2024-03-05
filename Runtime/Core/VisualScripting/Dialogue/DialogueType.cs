using System;
using System.IO;
using UnityEngine;
using LitJson;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{
    public enum DialogueSubtitleType
    {
        Sound,
        Subtitle,
    }


    public enum DialogueType
    {
        [LabelText("无")]
        None,
        [LabelText("台词")]
        ActorsLines,
        [LabelText("选项")]
        Option
    }

}