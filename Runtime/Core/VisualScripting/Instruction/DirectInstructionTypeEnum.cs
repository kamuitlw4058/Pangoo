using Sirenix.OdinInspector;


namespace Pangoo.Core.VisualScripting
{
    public enum DirectInstructionTypeEnum
    {
        Unknown,



        [LabelText("动态物体/播放Timeline")]
        DynamicObjectPlayTimeline,

        [LabelText("GameSection/切换GameSection")]
        ChangeGameSection,

        [LabelText("变量/设置Bool变量")]
        SetBoolVariable,

        [LabelText("玩家/设置是否可以被控制")]
        SetPlayerIsControllable,

        [LabelText("动态物体自身/设置GameObject激活")]
        SetGameObjectActive,

        [LabelText("动态物体自身/激活相机GameObject")]
        ActiveCameraGameObject,

        [LabelText("动态物体自身/关闭相机GameObject")]
        UnactiveCameraGameObject,

        [LabelText("动态物体自身/播放Timeline")]
        SubGameObjectPlayTimeline,


        [LabelText("动态物体/模型的Active")]
        DynamicObjectModelActive,

        [LabelText("动态物体/设置Hotspot")]
        DynamicObjectHotspotActive,

        [LabelText("通用/运行指令")]
        RunInstruction,

        [LabelText("UI/显示字幕")]
        ShowSubtitle,

        [LabelText("动态物体自身/关闭自身的Trigger")]
        CloseSelfTrigger,

        [LabelText("通用/等待时间")]
        WaitTime,

        [LabelText("动态物体自身/暂停Timeline")]
        SubGameObjectPauseTimeline,

        [LabelText("动态物体/Trigger开关")]
        DynamicObjectTriggerEnabled,

        [LabelText("动态物体/运行执行触发")]
        DynamicObjectRunExecute,

        [LabelText("音频/播放音频")]
        PlaySound,

        [LabelText("音频/停止音频")]
        StopSound,

        [LabelText("动态物体/子物体开关")]
        DynamicObjectSubGameObjectEnabled,

        [LabelText("Tween/ImageFade")]
        ImageFade,

        [LabelText("通用/显示或隐藏光标")]
        ShowHideCursor,

        [LabelText("Tween/CanvasGroup")]
        CanvasGroup,

        [LabelText("通用/等待消息")]
        WaitMsg,

        [LabelText("Tween/KillID")]
        DoTweenKill,

        [LabelText("场景对象/设置GameObject激活")]
        SetGlobalGameObjectActive,

        [LabelText("变量/检查Bool变量列表")]
        CheckBoolVariableList,

        [LabelText("动态物体/设置是否可以交互")]
        DynamicObjectInteractEnable,

        [LabelText("UI/动态物体预览")]
        DynamicObjectPreview,
        [LabelText("动态物体自身/OnEnter")]
        DynamicObjectEnter,
        [LabelText("动态物体自身/OnExit")]
        DynamicObjectExit,

        [LabelText("动态物体/暂停Timeline")]
        DynamicObjectPauseTimeline,

        [LabelText("变量/等待Bool变成预定值")]
        WaitVariableBool,
        
        [LabelText("Tween/Tween灯光强度")]
        TweenLightIntensity,
        [LabelText("动态物体/设置材质球")]
        DynamicObjectSetMaterial,
        
        [LabelText("玩家/设置玩家控制器信息")]
        SetDriverInfo,
    }
}