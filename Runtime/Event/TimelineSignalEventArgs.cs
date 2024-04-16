using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using UnityEngine.Timeline;
using GameFramework;
using GameFramework.Event;
using Pangoo.Core.VisualScripting;
using UnityEngine.Playables;


namespace Pangoo
{
    [Serializable]
    public partial class TimelineSignalEventArgs : GameEventArgs
    {

        public static readonly int EventId = typeof(TimelineSignalEventArgs).GetHashCode();


        public override int Id => EventId;

        public DynamicObject dynamicObject;

        public PlayableDirector playableDirector;

        public string signalAssetName;


        public double playableDirectorTime;

        public double markerTime;

        public static TimelineSignalEventArgs Create(PlayableDirector playableDirector, DynamicObject dynamicObject, string signalAssetName, double playableDirectorTime, double markerTime)
        {
            var args = ReferencePool.Acquire<TimelineSignalEventArgs>();
            args.dynamicObject = dynamicObject;
            args.playableDirector = playableDirector;
            args.signalAssetName = signalAssetName;
            args.playableDirectorTime = playableDirectorTime;
            args.markerTime = markerTime;
            return args;
        }

        public override void Clear()
        {
            dynamicObject = null;
            playableDirector = null;
            signalAssetName = null;
            markerTime = 0;
            playableDirectorTime = 0;

        }


        public TimelineSignalEventArgs()
        {
            Clear();
        }

    }
}

