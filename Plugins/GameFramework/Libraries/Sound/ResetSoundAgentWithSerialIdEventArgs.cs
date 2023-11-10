//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

namespace GameFramework.Sound
{
    /// <summary>
    /// 重置声音代理事件。
    /// </summary>
    public sealed class ResetSoundAgentWithSerialIdEventArgs : GameFrameworkEventArgs
    {
        /// <summary>
        /// 初始化重置声音代理事件的新实例。
        /// </summary>
        public ResetSoundAgentWithSerialIdEventArgs()
        {
        }

        public int SerialId;

        /// <summary>
        /// 创建重置声音代理事件。
        /// </summary>
        /// <returns>创建的重置声音代理事件。</returns>
        public static ResetSoundAgentWithSerialIdEventArgs Create(int SerialId)
        {
            ResetSoundAgentWithSerialIdEventArgs resetSoundAgentEventArgs = ReferencePool.Acquire<ResetSoundAgentWithSerialIdEventArgs>();
            resetSoundAgentEventArgs.SerialId = SerialId;
            return resetSoundAgentEventArgs;
        }

        /// <summary>
        /// 清理重置声音代理事件。
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;
        }
    }
}
