using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using System.Linq;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class TriggerEventFilter
    {
        [JsonMember("FilterType")]
        public TriggerEventFilterEnum FilterType;

        [JsonMember("FilterKeyCode")]
        public KeyCode FilterKeyCode;

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
            }

            return true;
        }

    }
}