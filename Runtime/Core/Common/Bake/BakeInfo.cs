

using System;
using UnityEngine;

namespace Pangoo.Core.Common
{
    [System.Serializable]
    public struct BakeInfo
    {
        public string Path;

        public int lightmapIndex;

        public Vector4 lightmapScaleOffset;

        public int realtimeLightmapIndex;


        public Vector4 realtimeLightmapScaleOffset;

    }
}