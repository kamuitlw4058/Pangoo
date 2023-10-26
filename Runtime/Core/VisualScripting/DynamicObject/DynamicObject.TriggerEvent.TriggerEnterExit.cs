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

        public Action<Args> TriggerEnter3dEvent;

        public Action<Args> TriggerExit3dEvent;


        void OnTriggerEnter3dEvent(Args eventParams)
        {
            Debug.Log($"OnTriggerEnter3dEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents)
            {
                if (!trigger.Enabled) continue;
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
                if (!trigger.Enabled) continue;
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnTriggerEnter3D:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }





        public void TriggerEnter3d(Collider collider)
        {
            TriggerEnter3dEvent?.Invoke(CurrentArgs);
        }

        public void TriggerExit3d(Collider collider)
        {
            TriggerExit3dEvent?.Invoke(CurrentArgs);
        }




    }
}