using System;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class HotSpotTransition
    {
        public string From;
        public string To;

        public AnimationClip Clip;

        public bool CanInvert;
    }

}