using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pangoo.Core.VisualScripting;

namespace Pangoo.Core.Common
{

    public class PangooSignalReceiver : MonoBehaviour, INotificationReceiver
    {
        public PlayableDirector playableDirector;

        public DynamicObject dynamicObject;


        public void OnNotify(Playable origin, INotification notification, object context)
        {
            Debug.Log($"OnTimelineNotify");
            var signal = notification as SignalEmitter;
            string signalAssetName = null;
            if (signal != null)
            {
                if (signal.asset != null)
                {
                    signalAssetName = signal.asset.name;
                }
            }
            PangooEntry.Event.FireNow(this, TimelineSignalEventArgs.Create(playableDirector, dynamicObject, signalAssetName));
        }
    }
}