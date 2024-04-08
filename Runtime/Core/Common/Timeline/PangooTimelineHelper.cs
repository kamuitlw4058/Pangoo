using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;

namespace Pangoo.Core.Common
{

    public class PangooTimelineHelper : MonoBehaviour, INotificationReceiver
    {

        public TimelineOperationTypeEnum TimelineOptType;

        public PlayableDirector playableDirector;

        public DynamicObject dynamicObject;

        [ShowInInspector]
        public PlayState PlayableDirectorState
        {
            get
            {
                return playableDirector?.state ?? PlayState.Paused;
            }
        }

        public string Path;

        public float Speed;

        public float AbsDeltaTime
        {
            get
            {
                return Time.deltaTime * Mathf.Abs(Speed);
            }
        }

        public float DeltaTime
        {
            get
            {
                return Time.deltaTime * Speed;
            }
        }

        TimelineAsset m_Asset;


        public TimelineAsset Asset
        {
            get
            {
                if (m_Asset == null)
                {
                    m_Asset = playableDirector.playableAsset as TimelineAsset;
                }
                return m_Asset;
            }
        }



        public void Start()
        {
            SetTimelineByMode();
        }

        

        private void Update()
        {
            if (TimelineOptType != TimelineOperationTypeEnum.ManualAndUpdate) return;
            if (Speed.Equals(0))return;

            var makers = Asset.markerTrack;
            if (makers != null)
            {
                foreach (IMarker marker in makers.GetMarkers())
                {
                    if (Speed>0)
                    {
                        if (playableDirector.time < marker.time && marker.time <= playableDirector.time + DeltaTime)
                        {
                            InvokeSignal(playableDirector,marker);
                            return;
                        }
                    }
                    if(Speed<0)
                    {
                        if (playableDirector.time > marker.time && marker.time >= playableDirector.time + DeltaTime)
                        {
                            InvokeSignal(playableDirector,marker);
                            return;
                        }
                    }
                }
            }

            playableDirector.time = playableDirector.time + DeltaTime;
            playableDirector.Evaluate();
        }
        
        public void SetTimelineByMode()
        {
            switch (TimelineOptType)
            {
                case TimelineOperationTypeEnum.Manual:
                case TimelineOperationTypeEnum.ManualAndUpdate:
                    if (playableDirector != null)
                    {
                        playableDirector.timeUpdateMode = DirectorUpdateMode.Manual;
                        playableDirector.playOnAwake = false;
                        playableDirector.Play();
                    }

                    break;
            }
        }
        
        private void InvokeSignal(PlayableDirector playableDirector, IMarker marker)
        {
            playableDirector.time = marker.time;
            playableDirector.Evaluate();
            Speed = 0;
            
            playableDirector.playableGraph.GetOutput(0).PushNotification(playableDirector.playableGraph.GetRootPlayable(0),
                marker as SignalEmitter, null);
        }
        
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            Debug.Log($"OnTimelineNotify:{gameObject.name}:{playableDirector?.playableAsset}");
            var signal = notification as SignalEmitter;
            string signalAssetName = null;
            double signalTime = -1;
            if (signal != null)
            {
                if (signal.asset != null)
                {
                    signalAssetName = signal.asset.name;
                }
                signalTime = signal.time;
            }
            PangooEntry.Event.FireNow(this, TimelineSignalEventArgs.Create(playableDirector, dynamicObject, signalAssetName, signalTime));
        }

    }
}