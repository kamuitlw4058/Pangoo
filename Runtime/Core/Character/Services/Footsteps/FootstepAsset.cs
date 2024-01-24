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

        [LabelText("脚步声配置")]
        public FootstepEntry[] footsteps;

        [Serializable]
        public struct FootstepEntry
        {
            public Texture texture;

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
    }

}