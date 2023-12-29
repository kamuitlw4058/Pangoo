using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;

namespace Pangoo
{

    public static class InstructionArgsExtension
    {
        public static Transform GetTransformByPath(Args args, string path)
        {
            Transform ret = null;
            if (path.Equals("Self"))
            {
                ret = args.dynamicObject.CachedTransfrom;
            }
            else
            {
                ret = args.dynamicObject.CachedTransfrom.Find(path);
            }
            return ret;
        }

        public static Transform GetTransformByPath(Args args, string uuid, string path)
        {
            Transform DoTransform = null;
            Transform ret = null;

            if (uuid.IsNullOrWhiteSpace() || uuid.Equals("Self"))
            {
                DoTransform = args?.dynamicObject?.CachedTransfrom;
            }
            else
            {
                var DynamicObjectService = args.Main.DynamicObject;
                var targetEntity = DynamicObjectService.GetLoadedEntity(uuid);
                DoTransform = targetEntity?.DynamicObj?.CachedTransfrom;
            }

            if (DoTransform != null)
            {
                if (path.IsNullOrWhiteSpace() || path.Equals("Self"))
                {
                    ret = DoTransform;
                }
                else
                {
                    ret = DoTransform.Find(path);
                }
            }

            return ret;
        }
    }
}
