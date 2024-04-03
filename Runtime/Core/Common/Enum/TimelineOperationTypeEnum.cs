
using Sirenix.OdinInspector;

namespace Pangoo.Core.Common
{
    public enum TimelineOperationTypeEnum
    {
        [LabelText("默认")]
        None,
        [LabelText("手动操作")]
        Manual,
        [LabelText("手动更新和操作")]
        ManualAndUpdate,
    }
}
