using System;
using System.Collections;
using GameFramework;
using Pangoo.Common;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    // [Common.Title("Debug Text")]
    [Category("Tween/门缓动")]

    [Serializable]
    public class InstructionDoorTweenTransform : Instruction
    {
        [ShowInInspector]
        [HideInEditorMode]
        public Args LastestArgs { get; set; }

        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        InstructionDoorTweenTransformParams ParamsRaw = new InstructionDoorTweenTransformParams();

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


        public bool IsForward;



        public float LerpValueWithConfig(float startOrigin, float val, bool forwardBack = false, bool isRotation = false)
        {
            float start = 0;
            switch (ParamsRaw.TweenStartType)
            {
                case TweenTransformStartTypeEnum.RelativeOrigin:
                    start = startOrigin + (float)ParamsRaw.TweenMin;
                    break;
                case TweenTransformStartTypeEnum.ConfigValue:
                    start = (float)ParamsRaw.TweenMin;
                    break;
            }

            float end = 0;
            switch (ParamsRaw.TweenEndType)
            {
                case TweenTransformEndTypeEnum.RelativeStart:
                    if (IsForward)
                    {
                        end = start + (float)ParamsRaw.TweenMaxForward;
                    }
                    else
                    {
                        end = start + (float)ParamsRaw.TweenMaxBack;
                    }

                    break;
                case TweenTransformEndTypeEnum.ConfigValue:
                    if (IsForward)
                    {
                        end = (float)ParamsRaw.TweenMaxForward;
                    }
                    else
                    {
                        end = (float)ParamsRaw.TweenMaxBack;
                    }
                    break;
            }

            if (isRotation && Mathf.Abs(end - start) > 180)
            {
                if (end > start)
                {
                    end -= 360;
                }
                else
                {
                    end += 360;
                }

            }
            // Debug.Log($"startOrigin:{startOrigin} Start:{start} End:{end},val:{val}");

            return LerpValue(start, end, val, forwardBack);
        }

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

        public bool CheckTweenTransformType(TweenTransformType type)
        {
            if (((int)ParamsRaw.TweenType & (int)type) > 0)
            {
                return true;
            }
            return false;
        }

        public void UpdateTween(float progress)
        {
            var positionX = m_StartPosition.x;
            var positionY = m_StartPosition.y;
            var positionZ = m_StartPosition.z;

            var rotationX = m_StartRotation.x;
            var rotationY = m_StartRotation.y;
            var rotationZ = m_StartRotation.z;



            if (CheckTweenTransformType(TweenTransformType.PostionX))
            {
                positionX = LerpValueWithConfig(m_StartPosition.x, progress, ParamsRaw.ForwardBack);
            }

            if (CheckTweenTransformType(TweenTransformType.PostionY))
            {
                positionY = LerpValueWithConfig(m_StartPosition.y, progress, ParamsRaw.ForwardBack);
            }

            if (CheckTweenTransformType(TweenTransformType.PostionZ))
            {
                positionZ = LerpValueWithConfig(m_StartPosition.z, progress, ParamsRaw.ForwardBack);
            }
            m_TargetTransform.localPosition = new Vector3(positionX, positionY, positionZ);


            if (CheckTweenTransformType(TweenTransformType.RotationX))
            {
                rotationX = LerpValueWithConfig(m_StartRotation.x, progress, ParamsRaw.ForwardBack, true);
            }

            if (CheckTweenTransformType(TweenTransformType.RotationY))
            {
                rotationY = LerpValueWithConfig(m_StartRotation.y, progress, ParamsRaw.ForwardBack, true);
            }

            if (CheckTweenTransformType(TweenTransformType.RotationZ))
            {
                rotationZ = LerpValueWithConfig(m_StartRotation.z, progress, ParamsRaw.ForwardBack, true);
            }

            m_TargetTransform.localRotation = Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ));
            // Debug.Log($"args.Target:{m_TargetTransform.localRotation}, rotationZ:{rotationZ} ,min:{m_Params.TweenMin} max:{m_Params.TweenMax} progress:{progress} m_Params.ForwardBack:{m_Params.ForwardBack} z:{((int)m_Params.TweenType & (int)TweenTransformType.PostionZ)}");
        }




        protected override IEnumerator Run(Args args)
        {
            // Debug.Log($"args.Target:{args.Target}");
            if (args.Target != null)
            {
                var playerLocalPosition = args.dynamicObject.CachedTransfrom.InverseTransformPoint(args.Main.CharacterService.Player.CachedTransform.position);
                var finalDistance = 0f;

                switch (ParamsRaw.OpenDirectionAxis)
                {
                    case TweenTransformAxis.X:
                        finalDistance = playerLocalPosition.x;
                        break;
                    case TweenTransformAxis.Y:
                        finalDistance = playerLocalPosition.y;
                        break;
                    case TweenTransformAxis.Z:
                        finalDistance = playerLocalPosition.z;
                        break;
                }



                if (finalDistance >= 0)
                {

                    IsForward = ParamsRaw.InverseForwardBack ? false : true;
                }
                else
                {
                    IsForward = ParamsRaw.InverseForwardBack ? true : false;
                }
                m_TargetTransform = args.Target.transform;
                m_StartPosition = m_TargetTransform.localPosition;
                m_StartRotation = m_TargetTransform.localRotation.eulerAngles;
                m_StartTime = Time.time;
                var startProcess = Time.time < (m_StartTime + ParamsRaw.TweenDuration);
                Debug.Log($"args.Target:{args.Target}, startProcess:{startProcess} TweenMin:{ParamsRaw.TweenMin} TweenMax:{ParamsRaw.TweenMaxForward},{ParamsRaw.TweenMaxBack} TweenDuration:{ParamsRaw.TweenDuration}");
                while (Time.time < (m_StartTime + ParamsRaw.TweenDuration))
                {
                    progress = (Time.time - m_StartTime) / (float)ParamsRaw.TweenDuration;
                    UpdateTween(progress);
                    yield return null;
                }
                UpdateTween(1.0f);
                Debug.Log($"End Tween");
                var inCollider = args.dynamicObject.CachedTransfrom.Find("InCollider");
                var outCollider = args.dynamicObject.CachedTransfrom.Find("OutCollider");
                if (inCollider != null)
                {
                    if (IsForward)
                    {
                        inCollider.gameObject.SetActive(false);
                    }
                    else
                    {
                        inCollider.gameObject.SetActive(true);
                    }
                }

                if (outCollider != null)
                {
                    if (IsForward)
                    {
                        outCollider.gameObject.SetActive(true);
                    }
                    else
                    {
                        outCollider.gameObject.SetActive(false);
                    }
                }


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
