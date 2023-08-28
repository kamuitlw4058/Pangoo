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
        public bool Subscribed;
        public int EventCount;
        private void OnEnable() {
            //PangooEntry.Event.Subscribe(EventTriggerEventArgs.EventId,OnEventTrigger);
        }

        private void Update()
        {
            if(PangooEntry.Event != null && !Subscribed)
            {
                PangooEntry.Event.Subscribe(EventTriggerEventArgs.EventId, OnEventTrigger);
                Subscribed = true;
            }
        }



        void OnEventTrigger(object sender, GameEventArgs e){
            var args = e as EventTriggerEventArgs;
            if(ConditionString.Equals(args.ConditionString)){
                TriggerEvent?.Invoke();
                EventCount += 1;
            }
        }

        private void OnDisable() {
            if (Subscribed)
            {
                PangooEntry.Event.Unsubscribe(EventTriggerEventArgs.EventId, OnEventTrigger);
            }
           
        }


    }
}
