using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("ChangeTimelineMode")]
    [Category("Timeline/改变Time")]
    [Serializable]
    public class InstructionChangeTimelineModeParams : InstructionParams
    {
        [JsonMember("Path")]
        public string Path;
        
        [JsonMember("TimelineOperationType")]
        public TimelineOperationTypeEnum TimelineOperationType;
    }
}

