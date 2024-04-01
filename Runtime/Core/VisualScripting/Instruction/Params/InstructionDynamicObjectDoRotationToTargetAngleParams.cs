using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public class InstructionDynamicObjectDoRotationToTargetAngleParams : InstructionParams
    {
        [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        public string DynamicObjectUuid;
        
        [JsonMember("Path")]
        [ValueDropdown("@GameSupportEditorUtility.RefPrefabStringDropdown(GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(DynamicObjectUuid))")]
        public string Path;
        [JsonMember("InitRotation")]
        public Vector3 InitRotation;
        [JsonMember("TargetRotation")]
        public Vector3 TargetRotation;
        [JsonMember("RotationSpeed")]
        public float RotationSpeed = 5f;
        [JsonMember("UseDamping")]
        public bool UseDamping;

        public AnimationCurve AnimationCurve=new AnimationCurve(new Keyframe(0,0),new Keyframe(1,1));
        
        [JsonMember("AnimCurveKeyFramesList")]
        public List<SerializeAnimCurveKeyFrame> AnimCurveKeyFramesList=new List<SerializeAnimCurveKeyFrame>();

        public override string Save()
        {
            AnimCurveKeyFramesList.Clear();
            
            for (int i = 0; i < AnimationCurve.length; i++)
            {
                AnimCurveKeyFramesList.Add(new SerializeAnimCurveKeyFrame()
                {
                    Time = AnimationCurve[i].time,
                    Value = AnimationCurve[i].value,
                    InTangent = AnimationCurve[i].inTangent,
                    OutTangent = AnimationCurve[i].outTangent,
                    WeightedMode =AnimationCurve[i].weightedMode,
                    InWeight = AnimationCurve[i].inWeight,
                    OutWeight = AnimationCurve[i].outTangent,
                });
            }

            return base.Save();
        }

        public override void Load(string val)
        {
            base.Load(val);
            
            AnimationCurve = new AnimationCurve();
            for (int i = 0; i < AnimCurveKeyFramesList.Count; i++)
            {
                Keyframe keyframe = new Keyframe
                {
                    time = AnimCurveKeyFramesList[i].Time,
                    value = AnimCurveKeyFramesList[i].Value,
                    inTangent = AnimCurveKeyFramesList[i].InTangent,
                    outTangent = AnimCurveKeyFramesList[i].OutTangent,
                    weightedMode = AnimCurveKeyFramesList[i].WeightedMode,
                    inWeight = AnimCurveKeyFramesList[i].InWeight,
                    outWeight= AnimCurveKeyFramesList[i].OutWeight,
                };
                AnimationCurve.AddKey(keyframe);
            }
        }
    }

    [System.Serializable]
    public class SerializeAnimCurveKeyFrame
    {
        [JsonMember("Time")]
        public float Time;
        [JsonMember("Value")]
        public float Value;
        [JsonMember("InTangent")]
        public float InTangent;
        [JsonMember("OutTangent")]
        public float OutTangent;
        [JsonMember("WeightedMode")]
        public WeightedMode WeightedMode;
        [JsonMember("InWeight")]
        public float InWeight;
        [JsonMember("OutWeight")]
        public float OutWeight;
    }
}

