using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using LitJson;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class InstructionPlayerClampParams : InstructionParams
    {
        [JsonMember("CharacterUuid")]
        [ValueDropdown("GetCharacterUuids", IsUniqueList = true)]
        public string CharacterUuid;

        [Title("重置相机旋转角度")]
        [JsonMember("ResetCameraRotation")]
        public bool ResetCameraRotation;

        [Title("是否开启相机旋转限制")]
        [JsonMember("OpenCameraClamp")]
        public bool OpenCameraClamp;

        [Title("限制相机旋转角度")]
        [JsonMember("RotationClamp")]
        public Vector2 RotationClamp;

        [Title("开启相机抖动")]
        [JsonMember("OpenCameraNoise")]
        public bool OpenCameraNoise;

        [Title("选择相机抖动的方式")]
        [JsonMember("NoiseSettings")]
        [ValueDropdown("GetNoiseSettings")]
        public string NoiseSettings;
        [LabelText("振幅增益")]
        [JsonMember("AmplitudeGain")]
        public float AmplitudeGain = 1;
        [LabelText("频率增益")]
        [JsonMember("FrequencyGain")]
        public float FrequencyGain = 1;


        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionPlayerClampParams>(val);
            CharacterUuid = par.CharacterUuid;
            ResetCameraRotation = par.ResetCameraRotation;
            OpenCameraClamp = par.OpenCameraClamp;
            RotationClamp = par.RotationClamp;
            OpenCameraNoise = par.OpenCameraNoise;
            NoiseSettings = par.NoiseSettings;
            AmplitudeGain = par.AmplitudeGain;
            FrequencyGain = par.FrequencyGain;
        }

#if UNITY_EDITOR
        public IEnumerable GetCharacterUuids()
        {
            return CharacterOverview.GetUuidDropdown();
        }

        public IEnumerable GetNoiseSettings()
        {
            return GameSupportEditorUtility.GetNoiseSettings();
        }
#endif
    }
}
