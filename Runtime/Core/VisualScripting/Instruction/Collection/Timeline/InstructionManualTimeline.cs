using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("TimelineDeltaTime")]
    [Category("Timeline/移动时间轴")]
    [Serializable]
    public class InstructionManualTimeline : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionManualTimelineParams ParamsRaw = new InstructionManualTimelineParams();

        public override IParams Params => ParamsRaw;
        public override void RunImmediate(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            bool timelineStarted = false;
            
            if (trans != null)
            {

                var playableDirector = trans.GetComponent<PlayableDirector>();
                if (playableDirector == null)
                {
                    return;
                }

                playableDirector.Play();
                
                var asset = playableDirector.playableAsset as TimelineAsset;
                var makers = asset.markerTrack;


                if (makers != null)
                {
                    var sign = Mathf.Sign(ParamsRaw.TimeFactor);
                    foreach (IMarker marker in makers.GetMarkers())
                    {
                        if (sign > 0 ? marker.time >= playableDirector.time : marker.time <= playableDirector.time &&sign > 0 ? marker.time < playableDirector.time +Time.deltaTime * sign : marker.time > playableDirector.time+Time.deltaTime * sign)
                        {
                            playableDirector.playableGraph.GetOutput(0).PushNotification(playableDirector.playableGraph.GetRootPlayable(0), marker as SignalEmitter, null);
                        }
                    }
                }
                
                switch (playableDirector.extrapolationMode)
                {
                    case DirectorWrapMode.None:
                        if (playableDirector.time>playableDirector.duration)
                        {
                            playableDirector.Stop();
                        }
                        break;
                    case DirectorWrapMode.Hold:
                        if (playableDirector.time>=playableDirector.duration)
                        {
                            playableDirector.time = playableDirector.duration;
                            playableDirector.Pause();
                        }
                        break;
                    case DirectorWrapMode.Loop:
                        if (playableDirector.time>playableDirector.duration)
                        {
                            playableDirector.time = 0;
                        }
                        break;
                }
                
                playableDirector.time += ParamsRaw.TimeFactor*Time.deltaTime;
                playableDirector.Evaluate();

                if (playableDirector.time<=0)
                {
                    playableDirector.time = 0;
                }
            }
        }
    }
}

