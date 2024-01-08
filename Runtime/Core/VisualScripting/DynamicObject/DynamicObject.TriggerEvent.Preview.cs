using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using GameFramework.Event;


namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {



        public void OnPreview()
        {
            var val = GetVariable<int>(Main.DefaultPreviewInteraceVariableUuid);
            Debug.Log($"OnPreview:{val}");
            TriggerInovke(TriggerTypeEnum.OnPreview);
        }

    }
}