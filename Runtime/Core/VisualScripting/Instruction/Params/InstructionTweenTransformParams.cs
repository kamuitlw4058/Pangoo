using System;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [System.Flags]
    public enum TweenTransformType
    {
        PostionX = 1 << 1,
        PostionY = 1 << 2,
        PostionZ = 1 << 3,
        RotationX = 1 << 4,
        RotationY = 1 << 5,
        RotationZ = 1 << 6,
    }

    public enum TweenTransformStartTypeEnum
    {
        [LabelText("相对原始值")]
        RelativeOrigin,

        [LabelText("配置值")]
        ConfigValue,
    }

    public enum TweenTransformEndTypeEnum
    {
        [LabelText("相对起始值")]
        RelativeStart,

        [LabelText("配置值")]
        ConfigValue,
    }

    [Serializable]
    public class InstructionTweenTransformParams : InstructionParams
    {
        [JsonMember("TweenStartType")]
        public TweenTransformStartTypeEnum TweenStartType;

        [JsonMember("TweenEndType")]
        public TweenTransformEndTypeEnum TweenEndType;

        [JsonMember("TweenType")]
        public TweenTransformType TweenType;


        [JsonMember("TweenDuration")]
        public double TweenDuration;

        [JsonMember("ForwardBack")]
        public bool ForwardBack;


        [JsonMember("TweenMin")]
        public double TweenMin;

        [JsonMember("TweenMax")]
        public double TweenMax;

        [LabelText("保存结束时的Transform")]
        [JsonMember("SetFinalTransform")]
        public bool SetFinalTransform;


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionTweenTransformParams>(val);
            TweenStartType = par.TweenStartType;
            TweenEndType = par.TweenEndType;
            TweenType = par.TweenType;
            TweenDuration = par.TweenDuration;
            ForwardBack = par.ForwardBack;
            TweenMin = par.TweenMin;
            TweenMax = par.TweenMax;
            SetFinalTransform = par.SetFinalTransform;
        }


    }
}