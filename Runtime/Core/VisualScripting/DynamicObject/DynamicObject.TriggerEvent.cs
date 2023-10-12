using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;
using Pangoo.Core.Service;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Core.Character;
using GameFramework;
using UnityEngine.Rendering;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        void OnTriggerEnter3dEvent(Args eventParams)
        {
            Debug.Log($"OnTriggerEnter3dEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnTriggerEnter3D:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }


        void OnTriggerExit3dEvent(Args eventParams)
        {
            Debug.Log($"OnTriggerExit3dEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnTriggerEnter3D:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }



        void OnInteractEvent(Args eventParams)
        {
            Debug.Log($"OnInteractEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents)
            {
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnInteract:
                        // trigger.EventRunInstructionsEnd -= OnInteractEnd;
                        // trigger.EventRunInstructionsEnd += OnInteractEnd;
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }

    }
}