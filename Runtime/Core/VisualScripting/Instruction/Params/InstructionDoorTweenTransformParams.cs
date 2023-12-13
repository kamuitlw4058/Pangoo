using System;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class InstructionDoorTweenTransformParams : InstructionParams
    {
        [JsonMember("TweenStartType")]
        public TweenTransformStartTypeEnum TweenStartType;

        [JsonMember("TweenEndType")]
        public TweenTransformEndTypeEnum TweenEndType;

        [JsonMember("TweenType")]
        public TweenTransformType TweenType;

        [JsonMember("OpenDirectionAxis")]
        public TweenTransformAxis OpenDirectionAxis;


        [JsonMember("TweenDuration")]
        public double TweenDuration;

        [JsonMember("ForwardBack")]
        public bool ForwardBack;


        [JsonMember("TweenMin")]
        public double TweenMin;

        [JsonMember("TweenMaxForward")]
        public double TweenMaxForward;

        [JsonMember("TweenMaxBack")]
        public double TweenMaxBack;

        [LabelText("保存结束时的Transform")]
        [JsonMember("SetFinalTransform")]
        public bool SetFinalTransform;


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionDoorTweenTransformParams>(val);
            TweenStartType = par.TweenStartType;
            TweenEndType = par.TweenEndType;
            TweenType = par.TweenType;
            TweenDuration = par.TweenDuration;
            ForwardBack = par.ForwardBack;
            TweenMin = par.TweenMin;
            TweenMaxForward = par.TweenMaxForward;
            TweenMaxBack = par.TweenMaxBack;
            OpenDirectionAxis = par.OpenDirectionAxis;
            SetFinalTransform = par.SetFinalTransform;
        }


    }
}