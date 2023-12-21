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
    public partial class EnterStaticSceneEventArgs : GameEventArgs
    {


        public static readonly int EventId = typeof(EnterStaticSceneEventArgs).GetHashCode();


        public override int Id => EventId;


        public string AssetPathUuid;


        public static EnterStaticSceneEventArgs Create(string assetPathUuid)
        {
            var args = ReferencePool.Acquire<EnterStaticSceneEventArgs>();
            args.AssetPathUuid = assetPathUuid;
            return args;
        }


        public override void Clear()
        {
            AssetPathUuid = string.Empty;
        }


        public EnterStaticSceneEventArgs()
        {
            Clear();
        }

    }
}

