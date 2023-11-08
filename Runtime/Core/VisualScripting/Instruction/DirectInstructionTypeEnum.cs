using Sirenix.OdinInspector;


namespace Pangoo.Core.VisualScripting
{
    public enum DirectInstructionTypeEnum
    {
        Unknown,



        [LabelText("动态物体播放Timeline")]
        DynamicObjectPlayTimeline,

        [LabelText("切换GameSection")]
        ChangeGameSection,

        [LabelText("设置Bool变量")]
        SetBoolVariable,

        [LabelText("设置玩家是否可以被控制")]
        SetPlayerIsControllable,

        [LabelText("设置子GameObject激活")]
        SetGameObjectActive,

        [LabelText("激活相机GameObject")]
        ActiveCameraGameObject,

        [LabelText("关闭相机GameObject")]
        UnactiveCameraGameObject,

        [LabelText("子物体播放Timeline")]
        SubGameObjectPlayTimeline,


        [LabelText("设置动态物体模型的Active")]
        DynamicObjectModelActive,

        [LabelText("动态物体设置Hotspot")]
        DynamicObjectHotspotActive,

        [LabelText("运行指令")]
        RunInstruction,

        [LabelText("显示字幕")]
        ShowSubtitle,

        [LabelText("关闭自身的Trigger")]
        CloseSelfTrigger,

        [LabelText("等待时间")]
        WaitTime,

        [LabelText("子物体暂停Timeline")]
        SubGameObjectPauseTimeline,

        [LabelText("动态物体的Trigger开关")]
        DynamicObjectModelTriggerEnabled,
    }
}