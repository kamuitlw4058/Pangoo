using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class TriggerEventFilter
    {
        [JsonMember("FilterType")]
        public TriggerEventFilterEnum FilterType;

        [JsonMember("KeyCodePressTypeEnum")]
        public KeyCodePressType KeyCodePressTypeEnum;
        [ShowIf("FilterType", TriggerEventFilterEnum.KeyCode)]
        [JsonMember("FilterKeyCode")]
        public KeyCode FilterKeyCode;
        
        private float FilterTimer;
        [ShowIf("FilterType", TriggerEventFilterEnum.Timer)]
        [JsonMember("FilterTimerDuration")]
        public float FilterTimerDuration;

        [ShowIf("FilterType", TriggerEventFilterEnum.MouseKey)]
        [JsonMember("MouseKeyCode")]
        public MouseKeyCodeType MouseKeyCodeTypeEnum;

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
                case TriggerEventFilterEnum.MouseKey:
                    switch (KeyCodePressTypeEnum)
                    {
                        case KeyCodePressType.Down:
                            if (Input.GetMouseButtonDown(MouseKeyCodeTypeEnum.GetHashCode()))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        case KeyCodePressType.Pressed:
                            if (Input.GetMouseButton(MouseKeyCodeTypeEnum.GetHashCode()))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        case KeyCodePressType.Up:
                            if (Input.GetMouseButtonUp(MouseKeyCodeTypeEnum.GetHashCode()))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                            break;
                    }
                    break;
            }

            return true;
        }

    }
}

public enum MouseKeyCodeType
{
    Left,
    Right,
    Middle,
}

public enum KeyCodePressType
{
    Down,
    Pressed,
    Up,
}