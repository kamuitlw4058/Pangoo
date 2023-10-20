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

    // [Version(0, 1, 1)]
    [Common.Title("Debug Text")]
    [Category("Tween/CreateOrMovePlayer")]



    [Serializable]
    public class InstructionTweenTransform : Instruction
    {
        [ShowInInspector]
        public Args LastestArgs { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionTweenTransformParams m_Params = new InstructionTweenTransformParams();

        public override InstructionType InstructionType => InstructionType.Coroutine;
        // private PropertyGetString m_Message = new PropertyGetString("My message");

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Tween: {this.m_Params}";

        float m_StartTime;

        Transform m_TargetTransform = null;

        Vector3 m_StartPosition;
        Vector3 m_StartRotation;




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

            if (((int)m_Params.TweenType & (int)TweenTransformType.PostionX) > 0)
            {
                positionX = LerpValue(m_StartPosition.x + (float)m_Params.TweenMin, m_StartPosition.x + (float)m_Params.TweenMax, progress, m_Params.ForwardBack);
            }

            if (((int)m_Params.TweenType & (int)TweenTransformType.PostionY) > 0)
            {
                positionY = LerpValue(m_StartPosition.y + (float)m_Params.TweenMin, m_StartPosition.y + (float)m_Params.TweenMax, progress, m_Params.ForwardBack);
            }

            if (((int)m_Params.TweenType & (int)TweenTransformType.PostionZ) > 0)
            {
                positionZ = LerpValue(m_StartPosition.z + (float)m_Params.TweenMin, m_StartPosition.z + (float)m_Params.TweenMax, progress, m_Params.ForwardBack);
            }
            m_TargetTransform.localPosition = new Vector3(positionX, positionY, positionZ);


            if (((int)m_Params.TweenType & (int)TweenTransformType.RotationX) > 0)
            {
                rotationX = LerpValue(m_StartRotation.x + (float)m_Params.TweenMin, m_StartRotation.x + (float)m_Params.TweenMax, progress, m_Params.ForwardBack);
            }

            if (((int)m_Params.TweenType & (int)TweenTransformType.PostionY) > 0)
            {
                rotationY = LerpValue(m_StartRotation.y + (float)m_Params.TweenMin, m_StartRotation.y + (float)m_Params.TweenMax, progress, m_Params.ForwardBack);
            }

            if (((int)m_Params.TweenType & (int)TweenTransformType.PostionZ) > 0)
            {
                rotationZ = LerpValue(m_StartRotation.z + (float)m_Params.TweenMin, m_StartRotation.z + (float)m_Params.TweenMax, progress, m_Params.ForwardBack);
            }
            m_TargetTransform.localRotation = Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ));
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
                var startProcess = Time.time < (m_StartTime + m_Params.TweenDuration);
                Debug.Log($"args.Target:{args.Target}, startProcess:{startProcess} TweenMin:{m_Params.TweenMin} TweenMax:{m_Params.TweenMax} TweenDuration:{m_Params.TweenDuration}");
                var positionMin = m_StartPosition.x + (float)m_Params.TweenMin;


                while (Time.time < (m_StartTime + m_Params.TweenDuration))
                {

                    var progress = (Time.time - m_StartTime) / (float)m_Params.TweenDuration;
                    UpdateTween(progress);
                    yield return null;
                }
                UpdateTween(1.0f);
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

        public override string ParamsString()
        {
            return m_Params.ToJson();
        }

        public override void LoadParams(string instructionParams)
        {
            m_Params.LoadFromJson(instructionParams);
        }

        // METHODS: -------------------------------------------------------------------------------

    }
}
