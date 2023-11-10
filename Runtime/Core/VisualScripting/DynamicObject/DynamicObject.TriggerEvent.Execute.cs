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

        public void StartExecuteEvent()
        {
            Debug.Log($"OnExecuteEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents.Values)
            {
                if (!trigger.Enabled)
                {
                    continue;
                }

                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnExecute:
                        Debug.Log($"Trigger:{trigger?.Row?.Id} inovke ");
                        trigger.OnInvoke(CurrentArgs);
                        break;
                }
            }
        }

        public void StopExecuteEvent()
        {
            Debug.Log($"OnExecuteEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents.Values)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnExecute:
                        trigger.IsStoped = true;
                        break;
                }
            }
        }






    }
}