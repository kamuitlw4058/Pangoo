using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("TimelineDeltaTime")]
    [Category("Timeline/移动时间轴速度通过鼠标速度")]
    [Serializable]
    public class InstructionManualTimelineSpeedByMouseSpeed : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionManualTimelineParams ParamsRaw = new InstructionManualTimelineParams();
        public override IParams Params { get; }
        
        public override void RunImmediate(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            if (args.PointerData.Equals(null))
            {
                Debug.LogError($"动态物体{args.dynamicObject}没有获取到PointerData");
                return;
            }
            var speed=ParamsRaw.TimeFactor*args.PointerData.delta.magnitude*Time.deltaTime;
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


                if (makers != null && ParamsRaw.TimeFactor!=0)
                {
                    foreach (IMarker marker in makers.GetMarkers())
                    {
                        if (ParamsRaw.TimeFactor > 0)
                        {
                            if (marker.time > playableDirector.time && marker.time <= playableDirector.time +speed)
                            {
                                InvokeSignal(playableDirector, marker);
                            }
                        }

                        if (ParamsRaw.TimeFactor < 0)
                        {
                            if (marker.time < playableDirector.time && marker.time >= playableDirector.time+speed)
                            {
                                InvokeSignal(playableDirector, marker);
                            }
                        }
                    }
                }
                
                // Debug.Log($"当前鼠标速度:{args.PointerData.delta.magnitude}");
                playableDirector.time += speed;

                if (playableDirector.time<=0)
                {
                    playableDirector.time = 0;
                }
                
                if (playableDirector.state == PlayState.Playing)
                {
                    playableDirector.Evaluate();
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
            }
        }
        
        private void InvokeSignal(PlayableDirector playableDirector, IMarker marker)
        {
            playableDirector.playableGraph.GetOutput(0).PushNotification(playableDirector.playableGraph.GetRootPlayable(0),
                marker as SignalEmitter, null);
        }
    }
}
