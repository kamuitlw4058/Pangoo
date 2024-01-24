using UnityEngine;

namespace Pangoo.Core.Characters
{

    [CreateAssetMenu(fileName = "FootstepAsset", menuName = "Pangoo/FootstepAsset", order = 0)]
    public class FootstepAsset : ScriptableObject
    {
        public struct FootstepEntry
        {
            public Texture texture;
            public string[] soundUuids;

            public float volume;
        }
    }

}