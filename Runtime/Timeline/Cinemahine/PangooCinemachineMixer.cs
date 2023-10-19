
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

namespace Pangoo.Timeline
{

    internal sealed class PangooCinemachineMixer : PlayableBehaviour
    {
        public delegate PlayableDirector MasterDirectorDelegate();

        static public MasterDirectorDelegate GetMasterPlayableDirector;

        // The brain that this track controls
        private CinemachineBrain mBrain;
        private int mBrainOverrideId = -1;
        private bool mPreviewPlay;

#if UNITY_EDITOR && UNITY_2019_2_OR_NEWER
        class ScrubbingCacheHelper
        {
            // Remember the active clips of the previous frame so we can track camera cuts
            public int ActivePlayableA;
            public int ActivePlayableB;

            struct ClipObjects
            {
                public List<List<CinemachineVirtualCameraBase>> Cameras;
                public float MaxDampTime;
            }
            List<ClipObjects> CachedObjects;

            static List<CinemachineVirtualCameraBase> scratch = new List<CinemachineVirtualCameraBase>();

            public void Init(Playable playable)
            {
                // Build our vcam registry for scrubbing updates
                CachedObjects = new List<ClipObjects>(playable.GetInputCount());
                for (int i = 0; i < playable.GetInputCount(); ++i)
                {
                    var cs = new ClipObjects
                    {
                        Cameras = new List<List<CinemachineVirtualCameraBase>>(),
                    };

                    var clip = (ScriptPlayable<PangooCinemachineShotPlayable>)playable.GetInput(i);
                    PangooCinemachineShotPlayable shot = clip.GetBehaviour();
                    if (shot != null && shot.IsValid)
                    {
                        var mainVcam = shot.VirtualCamera;
                        cs.Cameras.Add(new List<CinemachineVirtualCameraBase>());

                        // Add all child cameras
                        scratch.Clear();
                        mainVcam.GetComponentsInChildren(scratch);
                        for (int j = 0; j < scratch.Count; ++j)
                        {
                            var vcam = scratch[j];

                            int nestLevel = 0;
                            for (ICinemachineCamera p = vcam.ParentCamera;
                                    p != null && p != (ICinemachineCamera)mainVcam; p = p.ParentCamera)
                            {
                                ++nestLevel;
                            }
                            while (cs.Cameras.Count <= nestLevel)
                                cs.Cameras.Add(new List<CinemachineVirtualCameraBase>());
                            cs.Cameras[nestLevel].Add(vcam);
                            cs.MaxDampTime = Mathf.Max(cs.MaxDampTime, vcam.GetMaxDampTime());
                        }
                    }
                    CachedObjects.Add(cs);
                }
            }

            public void ScrubToHere(float currentTime, int playableIndex, bool isCut, float timeInClip, Vector3 up)
            {
                PangooTargetPositionCache.CurrentTime = currentTime;

                if (PangooTargetPositionCache.CacheMode == PangooTargetPositionCache.Mode.Record)
                {
                    // If the clip is newly activated, force the time to clip start, 
                    // in case timeline skipped some frames.  This will avoid target lerps between shots.
                    if (Time.frameCount != PangooTargetPositionCache.CurrentFrame)
                        PangooTargetPositionCache.IsCameraCut = false;
                    PangooTargetPositionCache.CurrentFrame = Time.frameCount;
                    if (isCut)
                        PangooTargetPositionCache.IsCameraCut = true;

                    return;
                }

                if (!PangooTargetPositionCache.HasCurrentTime)
                    return;

                var cs = CachedObjects[playableIndex];
                float stepSize = PangooTargetPositionCache.CacheStepSize;

                // Impose upper limit on damping time, to avoid simulating too many frames
                float maxDampTime = Mathf.Max(0, timeInClip - stepSize);
                maxDampTime = Mathf.Min(cs.MaxDampTime, Mathf.Min(maxDampTime, 4.0f));

                var endTime = PangooTargetPositionCache.CurrentTime;
                var startTime = Mathf.Max(
                    PangooTargetPositionCache.CacheTimeRange.Start + stepSize, endTime - maxDampTime);
                var numSteps = Mathf.FloorToInt((endTime - startTime) / stepSize);
                for (int step = numSteps; step >= 0; --step)
                {
                    var t = Mathf.Max(startTime, endTime - step * stepSize);
                    PangooTargetPositionCache.CurrentTime = t;
                    var deltaTime = (step == numSteps) ? -1
                        : (t - startTime < stepSize ? t - startTime : stepSize);

                    // Update all relevant vcams, leaf-most first
                    for (int i = cs.Cameras.Count - 1; i >= 0; --i)
                    {
                        var sublist = cs.Cameras[i];
                        for (int j = sublist.Count - 1; j >= 0; --j)
                        {
                            var vcam = sublist[j];
                            if (deltaTime < 0)
                                vcam.ForceCameraPosition(
                                    PangooTargetPositionCache.GetTargetPosition(vcam.transform),
                                    PangooTargetPositionCache.GetTargetRotation(vcam.transform));
                            vcam.InternalUpdateCameraState(up, deltaTime);
                        }
                    }
                }
            }
        }
        ScrubbingCacheHelper m_ScrubbingCacheHelper;
#endif

#if UNITY_EDITOR && UNITY_2019_2_OR_NEWER
        public override void OnGraphStart(Playable playable)
        {
            base.OnGraphStart(playable);
            m_ScrubbingCacheHelper = null;
        }
#endif

        public override void OnPlayableDestroy(Playable playable)
        {
            if (mBrain != null)
                mBrain.ReleaseCameraOverride(mBrainOverrideId); // clean up
            mBrainOverrideId = -1;
#if UNITY_EDITOR && UNITY_2019_2_OR_NEWER
            m_ScrubbingCacheHelper = null;
#endif
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            mPreviewPlay = false;
#if UNITY_EDITOR && UNITY_2019_2_OR_NEWER
            var cacheMode = PangooTargetPositionCache.Mode.Disabled;
            if (!Application.isPlaying)
            {
                if (GetMasterPlayableDirector != null)
                {
                    var d = GetMasterPlayableDirector();
                    if (d != null && d.playableGraph.IsValid())
                        mPreviewPlay = GetMasterPlayableDirector().playableGraph.IsPlaying();
                }
                if (PangooTargetPositionCache.UseCache)
                {
                    cacheMode = mPreviewPlay ? PangooTargetPositionCache.Mode.Record : PangooTargetPositionCache.Mode.Playback;
                    if (m_ScrubbingCacheHelper == null)
                    {
                        m_ScrubbingCacheHelper = new ScrubbingCacheHelper();
                        m_ScrubbingCacheHelper.Init(playable);
                    }
                }
            }
            PangooTargetPositionCache.CacheMode = cacheMode;
#endif
        }

        public CinemachineBrain GetBrainFromGo(string tagName)
        {
            CinemachineBrain brain = null;
            var go = GameObject.FindGameObjectWithTag(tagName);
            if (go != null)
            {
                brain = go.GetComponent<CinemachineBrain>();
            }
            return brain;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);

            // Get the brain that this track controls.
            // Older versions of timeline sent the gameObject by mistake.
            GameObject go = playerData as GameObject;
            if (go == null)
                mBrain = (CinemachineBrain)playerData;
            else
                mBrain = go.GetComponent<CinemachineBrain>();

            if (mBrain == null) mBrain = GameObject.FindObjectOfType<CinemachineBrain>();

            if (mBrain == null)
            {
                Debug.LogError($"Find CinemachineBrain Failed!");
                return;
            }


            // Find which clips are active.  We can process a maximum of 2.
            // In the case that the weights don't add up to 1, the outgoing weight
            // will be calculated as the inverse of the incoming weight.
            int activeInputs = 0;
            int clipIndexA = -1;
            int clipIndexB = -1;
            bool incomingIsA = false; // Assume that incoming clip is clip B
            float weightB = 1;
            for (int i = 0; i < playable.GetInputCount(); ++i)
            {
                float weight = playable.GetInputWeight(i);
                var clip = (ScriptPlayable<PangooCinemachineShotPlayable>)playable.GetInput(i);
                PangooCinemachineShotPlayable shot = clip.GetBehaviour();
                if (shot != null && shot.IsValid
                    && playable.GetPlayState() == PlayState.Playing
                    && weight > 0)
                {
                    clipIndexA = clipIndexB;
                    clipIndexB = i;
                    weightB = weight;
                    if (++activeInputs == 2)
                    {
                        // Deduce which clip is incoming (timeline doesn't know)
                        var clipA = playable.GetInput(clipIndexA);
                        // Incoming has later start time (therefore earlier current time)
                        incomingIsA = clip.GetTime() >= clipA.GetTime();
                        // If same start time, longer clip is incoming
                        if (clip.GetTime() == clipA.GetTime())
                            incomingIsA = clip.GetDuration() < clipA.GetDuration();
                        break;
                    }
                }
            }

            // Special case: check for only one clip that's fading out - it must be outgoing
            if (activeInputs == 1 && weightB < 1
                    && playable.GetInput(clipIndexB).GetTime() > playable.GetInput(clipIndexB).GetDuration() / 2)
            {
                incomingIsA = true;
            }
            if (incomingIsA)
            {
                (clipIndexA, clipIndexB) = (clipIndexB, clipIndexA);
                weightB = 1 - weightB;
            }

            ICinemachineCamera camA = null;
            if (clipIndexA >= 0)
            {
                PangooCinemachineShotPlayable shot
                    = ((ScriptPlayable<PangooCinemachineShotPlayable>)playable.GetInput(clipIndexA)).GetBehaviour();
                camA = shot.VirtualCamera;
            }

            ICinemachineCamera camB = null;
            if (clipIndexB >= 0)
            {
                PangooCinemachineShotPlayable shot
                    = ((ScriptPlayable<PangooCinemachineShotPlayable>)playable.GetInput(clipIndexB)).GetBehaviour();
                camB = shot.VirtualCamera;
            }

            // Override the Cinemachine brain with our results
            mBrainOverrideId = mBrain.SetCameraOverride(
                mBrainOverrideId, camA, camB, weightB, GetDeltaTime(info.deltaTime));

#if UNITY_EDITOR && UNITY_2019_2_OR_NEWER
            if (m_ScrubbingCacheHelper != null && PangooTargetPositionCache.CacheMode != PangooTargetPositionCache.Mode.Disabled)
            {
                bool isNewB = (m_ScrubbingCacheHelper.ActivePlayableA != clipIndexB
                    && m_ScrubbingCacheHelper.ActivePlayableB != clipIndexB);

                m_ScrubbingCacheHelper.ActivePlayableA = clipIndexA;
                m_ScrubbingCacheHelper.ActivePlayableB = clipIndexB;
                if (clipIndexA >= 0)
                    m_ScrubbingCacheHelper.ScrubToHere(
                        (float)GetMasterPlayableDirector().time, clipIndexA, false,
                        (float)playable.GetInput(clipIndexA).GetTime(), mBrain.DefaultWorldUp);
                if (clipIndexB >= 0)
                    m_ScrubbingCacheHelper.ScrubToHere(
                        (float)GetMasterPlayableDirector().time, clipIndexB, isNewB && weightB > 0.99f,
                        (float)playable.GetInput(clipIndexB).GetTime(), mBrain.DefaultWorldUp);
            }
#endif
        }

        float GetDeltaTime(float deltaTime)
        {
            if (mPreviewPlay || Application.isPlaying)
                return deltaTime;

            // We're scrubbing or paused
            if (PangooTargetPositionCache.CacheMode == PangooTargetPositionCache.Mode.Playback
                && PangooTargetPositionCache.HasCurrentTime)
            {
                return 0;
            }
            return -1;
        }
    }
}

