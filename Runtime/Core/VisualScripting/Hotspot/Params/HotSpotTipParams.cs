using System;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class HotSpotTipParams : HotspotParams
    {


        [JsonMember("Offset")]
        public Vector3 Offset;

        [JsonMember("Space")]
        public Space Space;



        public override void Load(string val)
        {
            var par = JsonMapper.ToObject<HotSpotTipParams>(val);
            Offset = par.Offset;
            Space = par.Space;

        }
    }
}