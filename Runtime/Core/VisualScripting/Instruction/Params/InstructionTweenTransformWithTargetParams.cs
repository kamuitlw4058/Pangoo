using System;
using System.Collections;
using UnityEngine;
using LitJson;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Pangoo.Common;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{



    [Serializable]
    public class InstructionTweenTransformWithTargetParams : InstructionParams
    {
        // [JsonMember("DynamicObjectUuid")]
        [ValueDropdown("OnDynamicObjectUuidDropdown")]
        [LabelText("参考动态物体")]
        public string DynamicObjectUuid;

        [JsonMember("Path")]
        [LabelText("路径列表")]
        [ValueDropdown("OnDynamicObjectPathDropdown")]
        public string Path;

        [JsonMember("TweenStartType")]
        [LabelText("起始类型")]

        public TweenTransformStartTypeEnum TweenStartType;

        [JsonMember("TweenEndType")]
        [LabelText("结束类型")]

        public TweenTransformEndTypeEnum TweenEndType;

        [JsonMember("TweenType")]
        [LabelText("操控类型")]

        public TweenTransformType TweenType;

        [JsonMember("EaseType")]
        [LabelText("曲线类型")]
        public TweenNormalEaseType EaseType;


        [JsonMember("TweenDuration")]
        [LabelText("持续时间")]

        public double TweenDuration;

        [JsonMember("ForwardBack")]
        [LabelText("是否来回")]

        public bool ForwardBack;

        [JsonMember("ForwardBackType")]
        [LabelText("来回类型")]
        public TweenFlashType ForwardBackType;


        [JsonMember("TweenMin")]
        [LabelText("起始值")]

        public double TweenMin;

        [JsonMember("TweenMax")]
        [LabelText("结束值")]
        public double TweenMax;

        [JsonMember("SoundUuid")]
        [LabelText("音效Uuid")]
        [ValueDropdown("OnSoundUuidDropdown")]
        public string SoundUuid;


        [JsonMember("SoundDelay")]
        [LabelText("音效延迟")]
        public float SoundDelay;

        [LabelText("保存结束时的Transform")]
        [JsonMember("SetFinalTransform")]
        public bool SetFinalTransform;

#if UNITY_EDITOR
        IEnumerable OnDynamicObjectUuidDropdown()
        {
            return DynamicObjectOverview.GetUuidDropdown();
        }

        IEnumerable OnDynamicObjectPathDropdown()
        {
            var prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(DynamicObjectUuid);
            if (prefab != null)
            {
                return GameSupportEditorUtility.RefPrefabStringDropdown(prefab);
            }
            else
            {
                return null;
            }

        }
        IEnumerable OnSoundUuidDropdown()
        {
            return SoundOverview.GetUuidDropdown();
        }
#endif
        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<InstructionTweenTransformWithTargetParams>(val);
            Path = par.Path;
            TweenStartType = par.TweenStartType;
            TweenEndType = par.TweenEndType;
            TweenType = par.TweenType;
            TweenDuration = par.TweenDuration;
            ForwardBack = par.ForwardBack;
            TweenMin = par.TweenMin;
            TweenMax = par.TweenMax;
            SetFinalTransform = par.SetFinalTransform;
            SoundUuid = par.SoundUuid;
            SoundDelay = par.SoundDelay;

        }


    }
}