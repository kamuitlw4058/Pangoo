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

        [Serializable]
        public struct TextureFootstepEntry
        {
            public Texture texture;
            public FootstepEntry footstepEntry;
        }


        [Serializable]
        public struct FootstepEntry
        {


            [ValueDropdown("SoundUuidDropdown")]
            public string[] soundUuids;

            public float volume;

            public Vector2 IntervalRange;

            public float MinInterval;
#if UNITY_EDITOR
            public IEnumerable SoundUuidDropdown()
            {
                return SoundOverview.GetUuidDropdown();
            }

#endif



        }

#if UNITY_EDITOR
        public IEnumerable VariableUuidDropdown()
        {
            return VariablesOverview.GetUuidDropdown();
        }

#endif

    }

}