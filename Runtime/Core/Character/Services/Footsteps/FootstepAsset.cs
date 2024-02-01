using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo.MetaTable;
using System;

namespace Pangoo.Core.Characters
{

    [CreateAssetMenu(fileName = "FootstepAsset", menuName = "Pangoo/FootstepAsset", order = 0)]
    public class FootstepAsset : ScriptableObject
    {
        public LayerMask LayerMask = -1;

        [LabelText("脚步声 纹理")]
        public TextureFootstepEntry[] textureFootSteps;

        [LabelText("脚本声 配置变量")]
        [ValueDropdown("VariableUuidDropdown")]
        public string configFootstepsUuid;

        [LabelText("脚步声 配置")]
        public FootstepEntry[] footsteps;



#if UNITY_EDITOR
        public IEnumerable VariableUuidDropdown()
        {
            return VariablesOverview.GetUuidDropdown();
        }

#endif

    }


    [Serializable]
    public struct TextureFootstepEntry
    {
        public Texture texture;
        public FootstepEntry footstepEntry;
    }

    [Serializable]
    public struct FootstepSoundList
    {
        [ValueDropdown("SoundUuidDropdown")]
        [LabelText("音频列表")]
        public string[] soundUuids;

#if UNITY_EDITOR
        public IEnumerable SoundUuidDropdown()
        {
            return SoundOverview.GetUuidDropdown();
        }
#endif
    }


    [Serializable]
    public struct FootstepEntry
    {
        [LabelText("脚本声列表")]
        public FootstepSoundList[] FootList;

        [LabelText("音量")]
        public float volume;

        [LabelText("随机循环间隔")]

        public Vector2 IntervalRange;

        [LabelText("最小间隔")]

        public float MinInterval;

    }
}