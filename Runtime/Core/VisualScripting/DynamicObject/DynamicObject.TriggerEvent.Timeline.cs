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
        public PlayableDirector[] playableDirectors;

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

            DoAwakeTimeineSignal();
        }

        bool InitedTimelineSignal;

        void DoAwakeTimeineSignal()
        {
            if (!InitedTimelineSignal)
            {
                Event.Subscribe(TimelineSignalEventArgs.EventId, OnTimelineSignalEvent);
                playableDirectors = gameObject.GetComponentsInChildren<PlayableDirector>(includeInactive: true);

                if (playableDirectors != null)
                {
                    foreach (var pd in playableDirectors)
                    {
                        Debug.Log($"当前PD:{pd}");
                        var timelineHelper = pd.gameObject.GetOrAddComponent<PangooTimelineHelper>();
                        timelineHelper.dynamicObject = this;
                        timelineHelper.playableDirector = pd;
                        timelineHelper.Path = pd.transform.GetRelativePath(CachedTransfrom);
                    }
                }

                var timelineHelperList =
                    JsonMapper.ToObject<List<DynamicObjectTimelineHelperInfo>>(Row.TimelineHelperList);
                if (timelineHelperList != null && timelineHelperList.Count > 0)
                {
                    foreach (var timelineHelper in timelineHelperList)
                    {
                        var pangooTimelineHelper = GetComponent<PangooTimelineHelper>(timelineHelper.Path);
                        if (pangooTimelineHelper != null)
                        {
                            pangooTimelineHelper.TimelineOptType = timelineHelper.OptType;
                        }
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
            Debug.Log($"OnTimelineSignalEvent:{args.signalAssetName},{args.playableDirectorTime},{args.markerTime}");
            CurrentArgs.playableDirector = args.playableDirector;
            CurrentArgs.signalAssetName = args.signalAssetName;
            CurrentArgs.SignalTime = args.playableDirectorTime;
            CurrentArgs.MarkerTime = args.markerTime;
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