using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Common;
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
        public InstructionManualTimelineSpeedByMouseSpeedParams ParamsRaw = new InstructionManualTimelineSpeedByMouseSpeedParams();
        public override IParams Params { get; }

        public override void RunImmediate(Args args)
        {
            var trans = args.dynamicObject.CachedTransfrom.Find(ParamsRaw.Path);
            if (args.PointerData.Equals(null))
            {
                Debug.LogError($"动态物体{args.dynamicObject}没有获取到PointerData");
                return;
            }

            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            Vector2 mouseDelta = new Vector2(mouseX, mouseY);
            var mouseSpeed = Mathf.Clamp(mouseDelta.magnitude, 0, ParamsRaw.MaxMouseSpeed);
            var speed = ParamsRaw.TimeFactor * mouseSpeed * Time.deltaTime;

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


                if (makers != null && ParamsRaw.TimeFactor != 0)
                {
                    foreach (IMarker marker in makers.GetMarkers())
                    {
                        if (ParamsRaw.TimeFactor > 0)
                        {
                            if (marker.time > playableDirector.time && marker.time <= playableDirector.time + speed)
                            {
                                InvokeSignal(playableDirector, marker);
                            }
                        }

                        if (ParamsRaw.TimeFactor < 0)
                        {
                            if (marker.time < playableDirector.time && marker.time >= playableDirector.time + speed)
                            {
                                InvokeSignal(playableDirector, marker);
                            }
                        }
                    }
                }

                playableDirector.time += speed;
                playableDirector.UpdateManuel();

            }
        }

        private void InvokeSignal(PlayableDirector playableDirector, IMarker marker)
        {
            playableDirector.playableGraph.GetOutput(0).PushNotification(playableDirector.playableGraph.GetRootPlayable(0),
                marker as SignalEmitter, null);
        }
    }
}
