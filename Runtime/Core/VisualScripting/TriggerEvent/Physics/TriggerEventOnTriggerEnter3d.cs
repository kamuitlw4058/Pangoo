using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    [Category("Physics/TriggerEnter3d")]
    public class TriggerEventOnTriggerEnter3d : TriggerEvent
    {
        public override TriggerTypeEnum TriggerType => TriggerTypeEnum.OnTriggerEnter3D;


    }
}