using System;
using System.IO;
using System.Collections.Generic;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using GameFramework.Event;

namespace Pangoo
{
    [Serializable]
    public partial class PlaceholderShowEventArgs : GameEventArgs
    {

        public static readonly int EventId = typeof(PlaceholderShowEventArgs).GetHashCode();


        public override int Id => EventId;


        public string Context;
        public float Duration;


        public static PlaceholderShowEventArgs Create(string context, float duration)
        {
            var args = ReferencePool.Acquire<PlaceholderShowEventArgs>();
            args.Context = context;
            args.Duration = duration;
            return args;
        }

        public override void Clear()
        {
            Context = string.Empty;
            Duration = 0;
        }


        public PlaceholderShowEventArgs()
        {
            Clear();
        }

    }
}

