using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using GameFramework.Event;
using LitJson;


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

                var timelineHelperList =
                     JsonMapper.ToObject<List<DynamicObjectTimelineHelperInfo>>(Row.TimelineHelperList);
                if (timelineHelperList!=null && timelineHelperList.Count>0)
                {
                    foreach (var timelineHelper in timelineHelperList)
                    {
                        var trans = GetTransform(timelineHelper.Path);
                        var pangooTimelineHelper=trans.GetOrAddComponent<PangooTimelineHelper>();
                        pangooTimelineHelper.dynamicObject = this;
                        pangooTimelineHelper.TimelineOptType = timelineHelper.OptType;
                        pangooTimelineHelper.Path = timelineHelper.Path;
                    }
                }
                
                InitedTimelineSignal = true;
            }

        }

        void DoDisableTimeineSignal()
        {
            if (InitedTimelineSignal)
            {
                Event.UnSubscribe(TimelineSignalEventArgs.EventId, OnTimelineSignalEvent);
                InitedTimelineSignal = false;
            }
        }

        void OnTimelineSignalEvent(object sender, GameEventArgs e)
        {
            var args = e as TimelineSignalEventArgs;
            if (args.dynamicObject == null || (args.dynamicObject != null && args.dynamicObject != this)) return;
            Debug.Log($"OnTimelineSignalEvent:{args}");
            CurrentArgs.playableDirector = args.playableDirector;
            CurrentArgs.signalAssetName = args.signalAssetName;
            CurrentArgs.SignalTime = args.playableDirectorTime;
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