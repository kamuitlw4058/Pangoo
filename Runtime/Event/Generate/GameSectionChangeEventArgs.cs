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

        public int GameSectionId;


        public static GameSectionChangeEventArgs Create(int gameSectionId)
        {
            var args = ReferencePool.Acquire<GameSectionChangeEventArgs>();
            args.GameSectionId = gameSectionId;
              return args;
        }


        public override void Clear(){
            GameSectionId = 0;
        }


        public GameSectionChangeEventArgs()
        {
              Clear();
        }

    }
}

