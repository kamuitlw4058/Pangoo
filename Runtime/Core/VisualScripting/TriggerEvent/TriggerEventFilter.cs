using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using System.Linq;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class TriggerEventFilter
    {
        [JsonMember("FilterType")]
        public TriggerEventFilterEnum FilterType;

        [ShowIf("FilterType", TriggerEventFilterEnum.KeyCode)]
        [JsonMember("FilterKeyCode")]
        public KeyCode FilterKeyCode;
        
        private float FilterTimer;
        [ShowIf("FilterType", TriggerEventFilterEnum.Timer)]
        [JsonMember("FilterTimerDuration")]
        public float FilterTimerDuration;

        public bool Check()
        {
            switch (FilterType)
            {
                case TriggerEventFilterEnum.KeyCode:
                    if (FilterKeyCode == KeyCode.None)
                    {
                        return true;
                    }
                    if (!Input.GetKeyDown(FilterKeyCode))
                    {
                        return false;
                    }
                    break;
                case TriggerEventFilterEnum.Timer:
                    if (FilterTimer<FilterTimerDuration)
                    {
                        FilterTimer += Time.deltaTime;
                        return false;
                    }
                    else
                    {
                        FilterTimer = 0;
                        return true;
                    }
                    break;
            }

            return true;
        }

    }
}