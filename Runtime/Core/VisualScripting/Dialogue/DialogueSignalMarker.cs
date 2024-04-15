using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.ComponentModel;
using System;


[DisplayName("对话信号")]
[ShowOdinSerializedPropertiesInInspector]
public class DialogueSignal : Marker, INotification, INotificationOptionProvider
{
    [AssetSelector]
    [LabelText("信号文件")]
    public SignalAsset Asset;


    public PropertyName id { get; }

    NotificationFlags INotificationOptionProvider.flags => NotificationFlags.TriggerOnce | NotificationFlags.TriggerInEditMode;


}
