using System;
using DG.Tweening;
using EFramework.Tweens.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Pangoo.Timeline
{

    public class OpenCtrlRotation : OpenCtrlBase
    {
        public enum RotationAxie
        {
            X, Y, Z
        }

        [LabelText("是否是往内开")]
        public bool IsInnerOpen = true;

        [LabelText("旋转轴")]
        public RotationAxie rotationAxie = RotationAxie.Y;
        [Range(0, 180)]
        [LabelText("打开角度")]
        public float OpenRotation = 90;

        [Range(0, 180)]
        [LabelText("关闭角度")]
        public float CloseRotation = 0;

        [LabelText("打开时长")]
        public float OpenDuration = 2;
        [LabelText("关闭时间")]
        public float CloseDuration = 2;



        [LabelText("是否打开")]
        [ShowInInspector]
        public override bool IsOpened
        {
            get
            {
                return transform.localEulerAngles.y == OpenRotation;
            }
        }
        [LabelText("是否关闭")]
        [ShowInInspector]
        public override bool IsClosed
        {
            get
            {
                return transform.localEulerAngles.y == CloseRotation;
            }
        }

        public override float Progress
        {
            set
            {

                float StartRotation = 0;
                float EndRotation = 0;
                Ease EaseType;
                if (IsOpening)
                {
                    StartRotation = CloseRotation;
                    EndRotation = OpenRotation;
                    EaseType = OpenEase;
                }
                else
                {
                    StartRotation = OpenRotation;
                    EndRotation = CloseRotation;
                    EaseType = CloseEase;
                }
                var rotation = DOVirtual.EasedValue(StartRotation, EndRotation, value, EaseType);
                if (!IsInnerOpen)
                {
                    rotation = -rotation;
                }

                switch (rotationAxie)
                {
                    case RotationAxie.X:
                        transform.localEulerAngles = new Vector3(rotation, 0, 0);
                        break;
                    case RotationAxie.Y:
                        transform.localEulerAngles = new Vector3(0, rotation, 0);
                        break;
                    case RotationAxie.Z:
                        transform.localEulerAngles = new Vector3(0, 0, rotation);
                        break;
                }


            }
        }



        [Button("开动画")]
        public override void OnOpen()
        {
            if (IsPlaying && IsOpened)
            {
                return;
            }

            transform.DOLocalRotate(new Vector3(0, IsInnerOpen ? OpenRotation : -OpenRotation, 0), OpenDuration, DoorRotateMode)
            .SetEase(OpenEase)
            .OnComplete(OnOpenComplete);
            IsPlaying = true;
        }

        [Button("关动画")]
        public override void OnClose()
        {
            if (IsPlaying && IsClosed)
            {
                return;
            }

            transform.DOLocalRotate(new Vector3(0, CloseRotation, 0), CloseDuration, DoorRotateMode)
            .SetEase(CloseEase)
            .OnComplete(OnCloseComplete);
            IsPlaying = true;
        }


    }
}