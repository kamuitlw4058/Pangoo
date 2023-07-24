using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameFramework.Event;

namespace Pangoo{

    public class GameObjectActiveReceiver : MonoBehaviour
    {
        public string ConditionString;
        public GameObject TargetGameObject;
        public bool Subscribed;
        public int EventCount;
        public int ValidEventCount;
        public bool objActive;
        public GameObject LastSender;

        private void OnEnable() {
            //PangooEntry.Event.Subscribe(EventTriggerEventArgs.EventId,OnEventTrigger);
        }

        private void Update()
        {
            if(PangooEntry.Event != null && !Subscribed)
            {
                PangooEntry.Event.Subscribe(GameObejctActiveEventArgs.EventId, OnEventTrigger);
                Subscribed = true;
            }
        }



        void OnEventTrigger(object sender, GameEventArgs e){
            var args = e as GameObejctActiveEventArgs;
            if(ConditionString.Equals(args.ConditionString)){
                TargetGameObject?.SetActive(args.Active);
                ValidEventCount += 1;
                objActive = args.Active;
                LastSender = sender as GameObject;
            }
            EventCount += 1;
        }

        private void OnDisable() {
            if (Subscribed)
            {
                PangooEntry.Event.Unsubscribe(GameObejctActiveEventArgs.EventId, OnEventTrigger);
            }
           
        }


    }
}
