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
    public partial class ExitStaticSceneEventArgs : GameEventArgs
    {


        public static readonly int EventId = typeof(ExitStaticSceneEventArgs).GetHashCode();


        public override int Id => EventId;

        public int AssetPathId = 0;

        public static ExitStaticSceneEventArgs Create(int assetPathId)
        {
            var args = ReferencePool.Acquire<ExitStaticSceneEventArgs>();
            args.AssetPathId = assetPathId;
            return args;
        }


        public override void Clear(){
            AssetPathId = 0;
        }


        public ExitStaticSceneEventArgs()
        {
              Clear();
        }

    }
}

