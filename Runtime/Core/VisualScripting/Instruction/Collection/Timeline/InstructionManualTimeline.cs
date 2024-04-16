using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Common;
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
            var playableDirector = args.dynamicObject.GetComponent<PlayableDirector>(ParamsRaw.Path);
            if (playableDirector != null)
            {

                playableDirector.Play();

                var asset = playableDirector.playableAsset as TimelineAsset;
                var makers = asset.markerTrack;

                if (makers != null && ParamsRaw.TimeFactor != 0)
                {
                    foreach (IMarker marker in makers.GetMarkers())
                    {
                        if (ParamsRaw.TimeFactor > 0)
                        {
                            if (marker.time > playableDirector.time && marker.time <= playableDirector.time + Time.deltaTime * ParamsRaw.TimeFactor)
                            {
                                InvokeSignal(playableDirector, marker);
                            }
                        }

                        if (ParamsRaw.TimeFactor < 0)
                        {
                            if (marker.time < playableDirector.time && marker.time >= playableDirector.time + Time.deltaTime * ParamsRaw.TimeFactor)
                            {
                                InvokeSignal(playableDirector, marker);
                            }
                        }
                    }
                }

                playableDirector.time += ParamsRaw.TimeFactor * Time.deltaTime;
                playableDirector.UpdateManuel();

            }
        }

        private static void InvokeSignal(PlayableDirector playableDirector, IMarker marker)
        {
            playableDirector.playableGraph.GetOutput(0).PushNotification(playableDirector.playableGraph.GetRootPlayable(0),
                marker as SignalEmitter, null);
        }
    }
}

