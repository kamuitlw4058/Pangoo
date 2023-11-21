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

    }
}
