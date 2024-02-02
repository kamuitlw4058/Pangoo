using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.VisualScripting;
using Plugins.Pangoo.Plugins.PangooCommon.Helper;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    public enum LightCtrlType
    {
        Light,
        Renderer,
    }

    public class LightCtrlEntry
    {
        public LightCtrlType Type;

    }

    public class LightImmersed : BaseImmersed
    {

        public override void OnUpdate()
        {

        }
    }

}

