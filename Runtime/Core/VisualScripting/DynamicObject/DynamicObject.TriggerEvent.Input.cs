using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public Action<Args> TriggerMouseLeftEvent;


        void OnInteractMouseLeft(Args eventParams)
        {
            Debug.Log($"OnInteractMouseLeft:{gameObject.name}");
            foreach (var trigger in TriggerEvents.Values)
            {
                if (!trigger.Enabled)
                {
                    continue;
                }

                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnMouseLeft:
                        Debug.Log($"Trigger:{trigger?.Row?.Id} {TriggerTypeEnum.OnMouseLeft} inovke ");
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }
    }
}