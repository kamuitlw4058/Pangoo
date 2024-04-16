using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using LitJson;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using UnityEngine.Timeline;
using Pangoo.Common;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public class TriggerEventFilter
    {
        [JsonMember("FilterType")]
        [LabelText("过滤器类型")]
        public TriggerEventFilterEnum FilterType;

        [JsonMember("KeyCodePressTypeEnum")]
        [ShowIf("@FilterType==TriggerEventFilterEnum.KeyCode||FilterType==TriggerEventFilterEnum.MouseKey")]
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

        [ShowIf("FilterType", TriggerEventFilterEnum.MouseDrag)]
        [JsonMember("mouseDirType")]
        public MouseDirTypeEnum mouseDirType;

        [ShowIf("FilterType", TriggerEventFilterEnum.SubTrigger)]
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        [JsonMember("DynamicObjectUuid")]
        public string DynamicObjectUuid;

        [ShowIf("FilterType", TriggerEventFilterEnum.SubTrigger)]
        [JsonMember("SubTriggerPath")]
        [ValueDropdown("@GameSupportEditorUtility.RefPrefabStringDropdown(GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(DynamicObjectUuid))")]
        public string SubTriggerPath;

        [ShowIf("FilterType", TriggerEventFilterEnum.Ray)]
        [JsonMember("PointPath")]
        public string PointPath;

        [ShowIf("FilterType", TriggerEventFilterEnum.TimelineSignal)]
        [JsonMember("TimelineSignalName")]
        public string TimelineSignalName;

#if UNITY_EDITOR

        SignalAsset m_SignalAsset;

        [AssetSelector]
        [JsonNoMember]
        [ShowInInspector]
        [ShowIf("FilterType", TriggerEventFilterEnum.TimelineSignal)]
        public SignalAsset TimelineSignalAsset
        {
            get
            {
                return m_SignalAsset;
            }
            set
            {
                m_SignalAsset = value;
                TimelineSignalName = m_SignalAsset.name;
            }
        }
#endif

        public bool Check(Args args)
        {
            switch (FilterType)
            {
                case TriggerEventFilterEnum.KeyCode:
                    if (FilterKeyCode == KeyCode.None)
                    {
                        return true;
                    }

                    switch (KeyCodePressTypeEnum)
                    {
                        case KeyCodePressType.Down:
                            return Input.GetKeyDown(FilterKeyCode);
                        case KeyCodePressType.Pressed:
                            return Input.GetKey(FilterKeyCode);
                        case KeyCodePressType.Up:
                            return Input.GetKeyUp(FilterKeyCode);
                    }
                    break;
                case TriggerEventFilterEnum.Timer:
                    if (FilterTimer < FilterTimerDuration)
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
                            return Input.GetMouseButtonDown(MouseKeyCodeTypeEnum.GetHashCode());
                        case KeyCodePressType.Pressed:
                            return Input.GetMouseButton(MouseKeyCodeTypeEnum.GetHashCode());
                        case KeyCodePressType.Up:
                            return Input.GetMouseButtonUp(MouseKeyCodeTypeEnum.GetHashCode());
                    }
                    break;
                case TriggerEventFilterEnum.MouseDrag:
                    return GetIsPush();
                case TriggerEventFilterEnum.SubTrigger:
                    if (args.triggerPath.IsNullOrWhiteSpace()) return false;

                    if (args.triggerPath.Equals(SubTriggerPath))
                    {
                        return true;
                    }
                    return false;
                case TriggerEventFilterEnum.Ray:
                    if (args.PointerPath.IsNullOrWhiteSpace()) return false;
                    if (args.PointerPath.Equals(PointPath))
                    {
                        return true;
                    }
                    return false;
                case TriggerEventFilterEnum.TimelineSignal:
                    if (args.signalAssetName.NullOrWhiteSpaceEquals(TimelineSignalName))
                    {
                        return true;
                    }
                    return false;
            }

            return true;
        }
        public bool GetIsPush()
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            switch (mouseDirType)
            {
                case MouseDirTypeEnum.XUp:
                    if (x > 0)
                    {
                        return true;
                    }

                    break;
                case MouseDirTypeEnum.XDown:
                    if (x < 0)
                    {
                        return true;
                    }

                    break;
                case MouseDirTypeEnum.YUp:
                    if (y > 0)
                    {
                        return true;
                    }

                    break;
                case MouseDirTypeEnum.YDown:
                    if (y < 0)
                    {
                        return true;
                    }

                    break;
                case MouseDirTypeEnum.XYUp:
                    if (x > 0 || y > 0)
                    {
                        return true;
                    }

                    break;
                case MouseDirTypeEnum.Any:
                    return true;
                case MouseDirTypeEnum.XDownYUp:
                    if (x < 0 || y > 0)
                    {
                        return true;
                    }

                    break;
            }
            return false;
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

public enum MouseDirTypeEnum
{
    XUp,
    XDown,
    YUp,
    XYUp,
    XDownYUp,
    YDown,
    Any,
}