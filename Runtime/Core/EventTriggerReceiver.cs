using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;

namespace Pangoo{

    public class EventTriggerReceiver : MonoBehaviour
    {
        public string ConditionString;
        public UnityEvent TriggerEvent;
        private void OnEnable() {
            PangooEntry.Event.Subscribe(EventTriggerEventArgs.EventId,OnEventTrigger);
        }

        void OnEventTrigger(object sender, GameEventArgs e){
            var args = e as EventTriggerEventArgs;
            if(ConditionString.Equals(args.ConditionString)){
                TriggerEvent?.Invoke();
            }
        }

        private void OnDisable() {
            PangooEntry.Event.Unsubscribe(EventTriggerEventArgs.EventId,OnEventTrigger);
        }


    }
}
