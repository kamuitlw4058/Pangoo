using Sirenix.OdinInspector;

namespace Pangoo.Common
{
    [System.Flags]
    public enum TweenTransformType
    {
        PostionX = 1 << 1,
        PostionY = 1 << 2,
        PostionZ = 1 << 3,
        RotationX = 1 << 4,
        RotationY = 1 << 5,
        RotationZ = 1 << 6,
    }

    public enum TweenTransformAxis
    {
        X,
        Y,
        Z,
    }

    public enum TweenFlashType
    {
        Flash,
        InFlash,
        OutFlash,
        InOutFlash,
    }

    public enum TweenNormalEaseType
    {
        [LabelText("线性")]
        Linear,
        [LabelText("1次SineIn")]
        InSine,
        [LabelText("1次SineOut")]
        OutSine,
        [LabelText("1次SineInOut")]
        InOutSine,

        [LabelText("2次SineIn")]
        InQuad,
        [LabelText("2次SineOut")]
        OutQuad,
        [LabelText("2次SineInOut")]
        InOutQuad,


        [LabelText("3次SineIn")]
        InCubic,
        [LabelText("3次SineOut")]
        OutCubic,
        [LabelText("3次SineInOut")]
        InOutCubic,

        [LabelText("4次SineIn")]
        InQuart,
        [LabelText("4次SineOut")]
        OutQuart,
        [LabelText("4次SineInOut")]
        InOuQuart,


        [LabelText("5次SineIn")]
        InExpo,
        [LabelText("5次SineOut")]
        OutExpo,
        [LabelText("5次SineInOut")]
        InOutExpo,
    }


    public enum TweenTransformStartTypeEnum
    {
        [LabelText("相对原始值")]
        RelativeOrigin,

        [LabelText("配置值")]
        ConfigValue,
    }

    public enum TweenTransformEndTypeEnum
    {
        [LabelText("相对起始值")]
        RelativeStart,

        [LabelText("配置值")]
        ConfigValue,
    }
}