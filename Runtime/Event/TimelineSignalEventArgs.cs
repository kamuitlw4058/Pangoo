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


        public static TimelineSignalEventArgs Create(PlayableDirector playableDirector, DynamicObject dynamicObject, string signalAssetName)
        {
            var args = ReferencePool.Acquire<TimelineSignalEventArgs>();
            args.dynamicObject = dynamicObject;
            args.playableDirector = playableDirector;
            args.signalAssetName = signalAssetName;
            return args;
        }

        public override void Clear()
        {
            dynamicObject = null;
            playableDirector = null;
            signalAssetName = null;
        }


        public TimelineSignalEventArgs()
        {
            Clear();
        }

    }
}

