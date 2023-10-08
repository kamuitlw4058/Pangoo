using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    [Category("Character/OnInteraction")]
    public class TriggerEventOnInteraction : TriggerEvent
    {
        public override TriggerTypeEnum TriggerType => TriggerTypeEnum.OnInteract;


    }
}