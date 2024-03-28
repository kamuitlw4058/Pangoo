

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo.Core.Common
{

    [CreateAssetMenu(fileName = "PrefabBakeInfoAsset", menuName = "PrefabBakeInfoAsset", order = 0)]
    public class PrefabBakeInfoAsset : ScriptableObject
    {
        public List<BakeInfo> bakeInfos = new List<BakeInfo>();


        public void Clear()
        {
            bakeInfos.Clear();
        }
    }

}