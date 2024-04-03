using System.Collections.Generic;
using UnityEngine;
using Pangoo.Common;
using Pangoo.Core.Common;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {
        Dictionary<string, Transform> CachedTransformDict = new Dictionary<string, Transform>();

        public Transform GetTransform(string path, Args args = null)
        {

            if (path.IsNullOrWhiteSpace())
            {
                return CachedTransfrom;
            }


            if (path.Equals(ConstString.Self))
            {
                return CachedTransfrom;
            }

            if (path.Equals(ConstString.Target) && args != null)
            {
                return args.Target?.transform;
            }

            Transform ret;

            if (CachedTransformDict.TryGetValue(path, out ret))
            {
                return ret;
            }

            ret = this.CachedTransfrom.Find(path);
            if (ret != null)
            {
                CachedTransformDict.Add(path, ret);
            }
            return ret;
        }

    }


}