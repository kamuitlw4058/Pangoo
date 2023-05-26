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
    public partial class GameSectionChangeEventArgs : GameEventArgs
    {


        public static readonly int EventId = typeof(GameSectionChangeEventArgs).GetHashCode();


        public override int Id => EventId;


        public static GameSectionChangeEventArgs Create()
        {
            var args = ReferencePool.Acquire<GameSectionChangeEventArgs>();
              return args;
        }


        public override void Clear(){
        }


        public GameSectionChangeEventArgs()
        {
              Clear();
        }

    }
}

