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

        public bool IsInteracting
        {
            get
            {
                return m_Tracker?.IsInteracting ?? false;
            }
        }


        public InteractionItemTracker m_Tracker = null;

        public Action<Args> InteractEvent;


        void OnInteract(Characters.Character character, IInteractive interactive)
        {

            Debug.Log($"OnInteract:{gameObject.name}");
            if (InteractEvent != null)
            {
                InteractEvent.Invoke(CurrentArgs);
            }
        }




        [Button("触发交互指令")]
        void OnInteract()
        {
            OnInteract(null, null);
        }



        void OnInteractEnd()
        {
            bool allFinish = true;
            foreach (var trigger in TriggerEvents.Values)
            {
                if (trigger.TriggerType == TriggerTypeEnum.OnInteract && trigger.IsRunning)
                {
                    allFinish = false;
                    break;
                }
            }
            if (allFinish)
            {
                m_Tracker?.Stop();
            }
        }

        void OnInteractEvent(Args eventParams)
        {
            Debug.Log($"OnInteractEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents.Values)
            {
                if (!trigger.Enabled)
                {
                    continue;
                }

                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnInteract:
                        Debug.Log($"Trigger:{trigger?.Row?.Id} inovke ");
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }




    }
}