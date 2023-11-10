//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.Event;
using GameFramework.Sound;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 播放声音成功事件。
    /// </summary>
    public sealed class PlaySoundResetEventArgs : GameEventArgs
    {
        /// <summary>
        /// 播放声音成功事件编号。
        /// </summary>
        public static readonly int EventId = typeof(PlaySoundResetEventArgs).GetHashCode();

        /// <summary>
        /// 初始化播放声音成功事件的新实例。
        /// </summary>
        public PlaySoundResetEventArgs()
        {
            SerialId = 0;
        }


        /// <summary>
        /// 获取声音的序列编号。
        /// </summary>
        public int SerialId
        {
            get;
            set;
        }

        /// <summary>
        /// 获取播放声音成功事件编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return EventId;
            }
        }


        /// <summary>
        /// 创建播放声音成功事件。
        /// </summary>
        /// <param name="e">内部事件。</param>
        /// <returns>创建的播放声音成功事件。</returns>
        public static PlaySoundResetEventArgs Create(ResetSoundAgentWithSerialIdEventArgs e)
        {
            PlaySoundResetEventArgs playSoundSuccessEventArgs = ReferencePool.Acquire<PlaySoundResetEventArgs>();
            playSoundSuccessEventArgs.SerialId = e.SerialId;
            return playSoundSuccessEventArgs;
        }

        /// <summary>
        /// 清理播放声音成功事件。
        /// </summary>
        public override void Clear()
        {
            SerialId = 0;

        }
    }
}
