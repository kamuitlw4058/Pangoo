using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;

namespace Pangoo{

    public class EventTriggerSender : MonoBehaviour
    {
        public string ConditionString;

        private void OnEnable() {
            PangooEntry.Event.Fire( this,EventTriggerEventArgs.Create(ConditionString));
        }

    }
}
