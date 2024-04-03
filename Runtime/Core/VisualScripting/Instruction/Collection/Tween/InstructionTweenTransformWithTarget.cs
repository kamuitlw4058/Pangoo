using System;
using System.Collections;
using GameFramework;
using Pangoo.Common;
using Pangoo.Core.Common;

using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework;
using DG.Tweening;
using DG.Tweening.Core;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Debug Text")]
    [Category("Tween/缓动Transform（带目标)")]

    [Serializable]
    public class InstructionTweenTransformWithTarget : Instruction
    {
        [ShowInInspector]
        [HideInEditorMode]
        public Args LastestArgs { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionTweenTransformWithTargetParams ParamsRaw = new InstructionTweenTransformWithTargetParams();

        public override IParams Params => this.ParamsRaw;

        public override InstructionType InstructionType => InstructionType.Coroutine;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Tween: {this.ParamsRaw}";


        [ShowInInspector]
        [HideInEditorMode]
        Transform m_TargetTransform = null;

        Vector3 m_StartPosition;

        Vector3 m_StartRotation;



        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionTweenTransformWithTarget()
        { }

        public void SetEase(Tweener tweener)
        {
            if (ParamsRaw.ForwardBack)
            {
                TweenerUtility.TweenerSetFlashEase(tweener, ParamsRaw.ForwardBackType);
            }
            else
            {
                TweenerUtility.TweenerSetNormalEase(tweener, ParamsRaw.EaseType);
            }
        }


        protected override IEnumerator Run(Args args)
        {
            Transform trans = args.dynamicObject.GetTransform(ParamsRaw.Path, args);
            Debug.Log($"路径Transform{ParamsRaw.Path},{args.Target},{trans}");
            if (trans == null)
            {
                Debug.Log($"路径Transform{ParamsRaw.Path},{args.Target},{trans}");
                trans = args.Target?.transform;
            }
            if (trans == null)
            {
                yield break;
            }


            m_TargetTransform = trans;
            m_StartPosition = m_TargetTransform.localPosition;
            m_StartRotation = m_TargetTransform.localRotation.eulerAngles;
            float runTime = 0;
            bool playedSound = false;


            bool positionComplete = true;
            bool rotationComplete = true;


            if (TweenerUtility.HasTweenTransformPositionType(ParamsRaw.TweenType))
            {
                var (startPosition, endPostion) = TweenerUtility.TweenPositionStartEnd(m_StartPosition, ParamsRaw.TweenType, ParamsRaw.TweenStartType, ParamsRaw.TweenEndType, min: (float)ParamsRaw.TweenMin, max: (float)ParamsRaw.TweenMax);
                m_TargetTransform.localPosition = startPosition;
                positionComplete = false;
                var tweener = m_TargetTransform.DOLocalMove(endPostion, (float)ParamsRaw.TweenDuration).OnComplete(() => { positionComplete = true; });
                SetEase(tweener);

            }

            if (TweenerUtility.HasTweenTransformRotationType(ParamsRaw.TweenType))
            {
                var (start, end) = TweenerUtility.TweenRotationStartEnd(m_StartRotation, ParamsRaw.TweenType, ParamsRaw.TweenStartType, ParamsRaw.TweenEndType, min: (float)ParamsRaw.TweenMin, max: (float)ParamsRaw.TweenMax);
                m_TargetTransform.localRotation = Quaternion.Euler(start);
                rotationComplete = false;
                var tweener = m_TargetTransform.DOLocalRotateQuaternion(Quaternion.Euler(end), (float)ParamsRaw.TweenDuration).OnComplete(() => { rotationComplete = true; });
                SetEase(tweener);

            }

            while (!positionComplete || !rotationComplete)
            {
                yield return null;
                var deltaTime = args?.dynamicObject?.DeltaTime ?? 0;
                if (deltaTime == 0)
                {
                    deltaTime = Time.deltaTime;
                }


                runTime += deltaTime;

                if (runTime >= ParamsRaw.SoundDelay && !ParamsRaw.SoundUuid.IsNullOrWhiteSpace() && !playedSound)
                {
                    Debug.Log($"播放音频：{ParamsRaw.SoundUuid}");
                    args.Main.Sound.PlaySound(ParamsRaw.SoundUuid);
                    playedSound = true;
                }

            }


            if (ParamsRaw.SetFinalTransform)
            {
                Debug.Log($"Set SetFinalTransform:path:{args.TargetPath}");
                Trigger.dynamicObject.SetTargetTransformValue(args.TargetPath, m_TargetTransform.ToTransformValue());
            }
        }

        public override void RunImmediate(Args args)
        {
            LastestArgs = args;
            if (args.Target != null)
            {
                // while ()
            }

        }



    }
}
