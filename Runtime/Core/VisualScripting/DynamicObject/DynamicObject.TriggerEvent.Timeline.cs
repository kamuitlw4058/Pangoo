using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using GameFramework.Event;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public Action<Args> PlayTimelineEvent;

        InstructionPlayTimeline PlayTimelineInstruction;


        PlayableDirector playableDirector { get; set; }

        void DoAwakeTimeline()
        {
            playableDirector = gameObject.GetComponent<PlayableDirector>();
            if (playableDirector != null)
            {
                PlayTimelineEvent += OnPlayTimelineEvent;
                PlayTimelineInstruction = Activator.CreateInstance<InstructionPlayTimeline>();
            }
        }

        bool InitedTimelineSignal;

        void DoAwakeTimeineSignal()
        {
            if (!InitedTimelineSignal)
            {
                Event.Subscribe(TimelineSignalEventArgs.EventId, OnTimelineSignalEvent);
                var playableDirectors = gameObject.GetComponentsInChildren<PlayableDirector>();
                if (playableDirectors != null)
                {
                    foreach (var pd in playableDirectors)
                    {
                        var signalReceiver = pd.gameObject.GetOrAddComponent<PangooSignalReceiver>();
                        signalReceiver.dynamicObject = this;
                        signalReceiver.playableDirector = pd;
                    }
                }

                InitedTimelineSignal = true;
            }

        }

        void DoDisableTimeineSignal()
        {
            if (InitedTimelineSignal)
            {
                Main.Event.UnSubscribe(TimelineSignalEventArgs.EventId, OnTimelineSignalEvent);
                InitedTimelineSignal = false;
            }
        }

        void OnTimelineSignalEvent(object sender, GameEventArgs e)
        {
            var args = e as TimelineSignalEventArgs;
            if (args.dynamicObject == null || (args.dynamicObject != null && args.dynamicObject != this)) return;
            CurrentArgs.playableDirector = args.playableDirector;
            CurrentArgs.signalAssetName = args.signalAssetName;
            TriggerInovke(TriggerTypeEnum.OnTimelineSignal);
        }

        void OnPlayTimelineEvent(Args args)
        {
            if (PlayTimelineInstruction != null)
            {
                args.ChangeTarget(gameObject);
                PlayTimelineInstruction.RunImmediate(args);
            }
        }


        public void PlayTimeline()
        {
            PlayTimelineEvent?.Invoke(CurrentArgs);
        }


    }
}