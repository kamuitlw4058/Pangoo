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

        public override void OnInvoke(TriggerEventParams eventParams)
        {
            base.OnInvoke(eventParams);
            Debug.Log($"In Event Invoke!.Row:{Row}");
            Instructions.Start(new Args(Row));
        }
    }
}