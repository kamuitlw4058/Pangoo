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

        public string GameSectionUuid;


        public static GameSectionChangeEventArgs Create(string uuid)
        {
            var args = ReferencePool.Acquire<GameSectionChangeEventArgs>();
            args.GameSectionUuid = uuid;
            return args;
        }


        public override void Clear()
        {
            GameSectionUuid = string.Empty;
        }


        public GameSectionChangeEventArgs()
        {
            Clear();
        }

    }
}

