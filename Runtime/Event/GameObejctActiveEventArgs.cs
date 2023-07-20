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
    public partial class GameObejctActiveEventArgs : GameEventArgs
    {


        public static readonly int EventId = typeof(GameObejctActiveEventArgs).GetHashCode();


        public override int Id => EventId;

        public string ConditionString;

        public bool Active;


        public static GameObejctActiveEventArgs Create(string ConditionString,bool Active)
        {
            var args = ReferencePool.Acquire<GameObejctActiveEventArgs>();
            args.ConditionString = ConditionString;
            args.Active = Active;
            return args;
        }

        public override void Clear(){
            ConditionString = string.Empty;
            Active = false;
        }


        public GameObejctActiveEventArgs()
        {
              Clear();
        }

    }
}

