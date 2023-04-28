// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using GameFramework.Event;

namespace Pangoo
{
     [Serializable]
    public partial class EventTriggerEventArgs : GameEventArgs
    {


        public static readonly int EventId = typeof(EventTriggerEventArgs).GetHashCode();


        public override int Id => EventId;

        public string ConditionString;


        public static EventTriggerEventArgs Create(string ConditionString)
        {
            var args = ReferencePool.Acquire<EventTriggerEventArgs>();
            args.ConditionString = ConditionString;
              return args;
        }

        public override void Clear(){
            ConditionString = string.Empty;
        }


        public EventTriggerEventArgs()
        {
              Clear();
        }

    }
}

