

using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Timeline
{
    sealed class OpenCtrlBehaviour : PlayableBehaviour
    {
        public OpenCtrlBase OpenCtrlTarget;

        public bool IsOpen;

        public Ease EaseType;

        bool HasProcessFrame = false;
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            OpenCtrlTarget.IsOpening = IsOpen;
            OpenCtrlTarget.Progress = 0;
            if (IsOpen)
            {

                OpenCtrlTarget.OpenStartEvent?.Invoke();
                Debug.Log($"开始开门！！！！");
            }
            else
            {
                OpenCtrlTarget.CloseStartEvent?.Invoke();
                Debug.Log($"开始关门！！！！");
            }
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (HasProcessFrame)
            {
                OpenCtrlTarget.IsOpening = IsOpen;
                OpenCtrlTarget.Progress = 1;
                if (IsOpen)
                {
                    OpenCtrlTarget.OpenEndEvent?.Invoke();
                    Debug.Log($"结束开门！！！！");
                }
                else
                {
                    OpenCtrlTarget.CloseEndEvent?.Invoke();
                    Debug.Log($"结束关门！！！！");
                }
                HasProcessFrame = false;
            }


        }
        

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            HasProcessFrame = true;
            var progress = playable.GetTime() / playable.GetDuration();
            OpenCtrlTarget.IsOpening = IsOpen;
            OpenCtrlTarget.Progress = (float)progress;
        }
    }
}
