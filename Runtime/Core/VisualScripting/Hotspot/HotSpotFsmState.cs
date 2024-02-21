using System;
using UnityEngine;

using Pangoo.Common;
using Sirenix.OdinInspector;
namespace Pangoo.Core.VisualScripting
{

    public enum HotSpotFsmStateType
    {
        AnimationClip,
        Sprite,
    }

    [Serializable]
    public class HotSpotFsmState : NamedFsmState<HotspotFsmManager>
    {
        public HotSpotFsmStateType StateType;

        [ShowIf("@this.StateType == HotSpotFsmStateType.AnimationClip")]
        public AnimationClip StateClip;

        [ShowIf("@this.StateType == HotSpotFsmStateType.Sprite")]
        public Sprite Sprite;

        float stateTime;

        protected override void OnEnter(INamedFsm<HotspotFsmManager> fsm)
        {
            base.OnEnter(fsm);
            Debug.Log($"Enter:{FullName}");

            if (fsm.Owner.IsTransition && fsm.Owner.Transition.CanInvert)
            {
                if (fsm.Owner.Transition.From.Equals(FullName) && fsm.Owner.Transition.To.Equals(fsm.LatestState?.FullName))
                {
                    fsm.Owner.TransitionInvert = true;
                }
                else
                {
                    fsm.Owner.TransitionInvert = false;
                }
                return;
            }

            foreach (var trans in fsm.Owner.TransList)
            {
                if (trans.From.Equals(fsm.LatestState?.FullName) && trans.To.Equals(FullName))
                {
                    fsm.Owner.Transition = trans;
                    fsm.Owner.TransitionTime = 0;
                    fsm.Owner.TransitionInvert = false;
                    return;
                }

                if (trans.CanInvert && trans.From.Equals(FullName) && trans.To.Equals(fsm.LatestState?.FullName))
                {
                    fsm.Owner.Transition = trans;
                    fsm.Owner.TransitionTime = fsm.Owner.Transition.Clip.length;
                    fsm.Owner.TransitionInvert = true;
                }
            }
        }

        protected override void OnUpdate(INamedFsm<HotspotFsmManager> fsm)
        {
            base.OnUpdate(fsm);
            // Debug.Log($"EnterTransition:{fsm.Owner.IsTransition}, enterTimer:{fsm.Owner.TransitionTime}, stateTime:{stateTime}");
            if (fsm.Owner.IsTransition)
            {
                if (fsm.Owner.TransitionInvert)
                {
                    fsm.Owner.TransitionTime -= fsm.DeltaTime;
                    fsm.Owner.Transition.Clip.SampleAnimation(fsm.Owner.Go, fsm.Owner.TransitionTime);
                    if (fsm.Owner.TransitionTime <= 0)
                    {
                        fsm.Owner.Transition = null;
                    }
                }
                else
                {
                    fsm.Owner.TransitionTime += fsm.DeltaTime;
                    fsm.Owner.Transition.Clip.SampleAnimation(fsm.Owner.Go, fsm.Owner.TransitionTime);
                    if (fsm.Owner.TransitionTime > fsm.Owner.Transition.Clip.length)
                    {
                        fsm.Owner.Transition = null;
                    }
                }

                return;
            }


            stateTime += fsm.DeltaTime;
            switch (StateType)
            {
                case HotSpotFsmStateType.AnimationClip:
                    if (StateClip != null)
                    {
                        // Debug.Log($"SampleAnimation:{StateClip}");
                        StateClip.SampleAnimation(fsm.Owner.Go, stateTime % StateClip.length);
                    }
                    break;
                case HotSpotFsmStateType.Sprite:
                    if (fsm.Owner.SpriteRenderer.sprite != Sprite)
                    {
                        fsm.Owner.SpriteRenderer.sprite = Sprite;
                    }
                    break;
            }




        }
    }
}