using System;
using System.Collections;
using GameFramework;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Debug Text")]
    [Category("Tween/缓动Transform")]

    [Serializable]
    public class InstructionTweenTransform : Instruction
    {
        [ShowInInspector]
        [HideInEditorMode]
        public Args LastestArgs { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionTweenTransformParams ParamsRaw = new InstructionTweenTransformParams();

        public override IParams Params => this.ParamsRaw;

        public override InstructionType InstructionType => InstructionType.Coroutine;
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Tween: {this.ParamsRaw}";


        float m_StartTime;

        [ShowInInspector]
        [HideInEditorMode]
        Transform m_TargetTransform = null;

        Vector3 m_StartPosition;

        Vector3 m_StartRotation;

        [HideInEditorMode]
        public float progress;




        // CONSTRUCTORS: --------------------------------------------------------------------------

        public InstructionTweenTransform()
        { }

        public float LerpValue(float start, float end, float val, bool forwardBack = false)
        {
            float progress = 0;
            if (forwardBack)
            {
                if (val >= 0.5)
                {
                    progress = Mathf.Clamp((float)(val - 0.5) * 2, 0f, 1f);
                    return Mathf.Lerp(end, start, val);
                }
                else
                {
                    progress = Mathf.Clamp((float)(val) * 2, 0f, 1f);
                    return Mathf.Lerp(start, end, val);
                }
            }

            return Mathf.Lerp(start, end, val);

        }

        public void UpdateTween(float progress)
        {
            var positionX = m_StartPosition.x;
            var positionY = m_StartPosition.y;
            var positionZ = m_StartPosition.z;

            var rotationX = m_StartRotation.x;
            var rotationY = m_StartRotation.y;
            var rotationZ = m_StartRotation.z;



            if (((int)ParamsRaw.TweenType & (int)TweenTransformType.PostionX) > 0)
            {
                positionX = LerpValue(m_StartPosition.x + (float)ParamsRaw.TweenMin, m_StartPosition.x + (float)ParamsRaw.TweenMax, progress, ParamsRaw.ForwardBack);
            }

            if (((int)ParamsRaw.TweenType & (int)TweenTransformType.PostionY) > 0)
            {
                positionY = LerpValue(m_StartPosition.y + (float)ParamsRaw.TweenMin, m_StartPosition.y + (float)ParamsRaw.TweenMax, progress, ParamsRaw.ForwardBack);
            }

            if (((int)ParamsRaw.TweenType & (int)TweenTransformType.PostionZ) > 0)
            {
                positionZ = LerpValue(m_StartPosition.z + (float)ParamsRaw.TweenMin, m_StartPosition.z + (float)ParamsRaw.TweenMax, progress, ParamsRaw.ForwardBack);
            }
            m_TargetTransform.localPosition = new Vector3(positionX, positionY, positionZ);


            if (((int)ParamsRaw.TweenType & (int)TweenTransformType.RotationX) > 0)
            {
                rotationX = LerpValue(m_StartRotation.x + (float)ParamsRaw.TweenMin, m_StartRotation.x + (float)ParamsRaw.TweenMax, progress, ParamsRaw.ForwardBack);
            }

            if (((int)ParamsRaw.TweenType & (int)TweenTransformType.RotationY) > 0)
            {
                rotationY = LerpValue(m_StartRotation.y + (float)ParamsRaw.TweenMin, m_StartRotation.y + (float)ParamsRaw.TweenMax, progress, ParamsRaw.ForwardBack);
            }

            if (((int)ParamsRaw.TweenType & (int)TweenTransformType.RotationZ) > 0)
            {
                rotationZ = LerpValue(m_StartRotation.z + (float)ParamsRaw.TweenMin, m_StartRotation.z + (float)ParamsRaw.TweenMax, progress, ParamsRaw.ForwardBack);
            }

            m_TargetTransform.localRotation = Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ));
            // Debug.Log($"args.Target:{m_TargetTransform.localRotation}, rotationZ:{rotationZ} ,min:{m_Params.TweenMin} max:{m_Params.TweenMax} progress:{progress} m_Params.ForwardBack:{m_Params.ForwardBack} z:{((int)m_Params.TweenType & (int)TweenTransformType.PostionZ)}");
        }


        protected override IEnumerator Run(Args args)
        {
            Debug.Log($"args.Target:{args.Target}");
            if (args.Target != null)
            {
                m_TargetTransform = args.Target.transform;
                m_StartPosition = m_TargetTransform.localPosition;
                m_StartRotation = m_TargetTransform.localRotation.eulerAngles;
                m_StartTime = Time.time;
                var startProcess = Time.time < (m_StartTime + ParamsRaw.TweenDuration);
                Debug.Log($"args.Target:{args.Target}, startProcess:{startProcess} TweenMin:{ParamsRaw.TweenMin} TweenMax:{ParamsRaw.TweenMax} TweenDuration:{ParamsRaw.TweenDuration}");
                while (Time.time < (m_StartTime + ParamsRaw.TweenDuration))
                {
                    progress = (Time.time - m_StartTime) / (float)ParamsRaw.TweenDuration;
                    UpdateTween(progress);
                    yield return null;
                }
                UpdateTween(1.0f);
                Debug.Log($"End Tween");

                if (ParamsRaw.SetFinalTransform)
                {
                    Debug.Log($"Set SetFinalTransform:path:{args.TargetPath}");
                    Trigger.dynamicObject.SetTargetTransformValue(args.TargetPath, m_TargetTransform.ToTransformValue());
                }
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
