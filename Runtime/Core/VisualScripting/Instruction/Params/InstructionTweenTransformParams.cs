using System;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Common;

namespace Pangoo.Core.VisualScripting
{


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