using System;
using System.Collections.Generic;
using UnityEngine;
using Pangoo;
using LitJson;

namespace Pangoo.Core.Common
{
    [Serializable]
    public struct TransformValue
    {
        public Vector3 Postion;

        public Vector3 Rotation;

        public Vector3 Scale;

        public static TransformValue Empty
        {
            get
            {
                var val = new TransformValue();
                val.Postion = Vector3.zero;
                val.Rotation = Vector3.zero;
                val.Scale = Vector3.one;
                return val;
            }
        }
    }
}