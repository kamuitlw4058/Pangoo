using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Pangoo.Timeline
{

    [DisallowMultipleComponent]
    public abstract class OpenCtrlBase : MonoBehaviour
    {
        [LabelText("是否正在运行")]
        public bool IsPlaying;

        protected bool m_IsOpening;


        [LabelText("是在进行开还是关操作")]
        public virtual bool IsOpening
        {
            get
            {
                return m_IsOpening;
            }
            set
            {
                m_IsOpening = value;
            }
        }


        [LabelText("打开曲线")]
        public Ease OpenEase = Ease.InOutQuad;

        [LabelText("关闭曲线")]
        public Ease CloseEase = Ease.InOutQuad;

        public RotateMode DoorRotateMode;

        [TabGroup("打开开始事件")]
        public UnityEvent OpenStartEvent;


        [TabGroup("打开结束事件")]
        public UnityEvent OpenEndEvent;

        [TabGroup("关闭开始事件")]
        public UnityEvent CloseStartEvent;

        [TabGroup("关闭结束事件")]
        public UnityEvent CloseEndEvent;

        [LabelText("是否打开")]
        [ShowInInspector]
        public virtual bool IsOpened
        {
            get
            {
                return false;
            }
        }
        [LabelText("是否关闭")]
        [ShowInInspector]
        public virtual bool IsClosed
        {
            get
            {
                return false;
            }
        }

        public virtual float Progress
        {
            set
            {


            }
        }

        public void OnOpenComplete()
        {
            IsPlaying = false;
            OpenEndEvent?.Invoke();
        }

        public void OnCloseComplete()
        {
            IsPlaying = false;
            CloseEndEvent?.Invoke();
        }

        [Button("开动画")]
        public virtual void OnOpen()
        {
            if (IsPlaying && IsOpened)
            {
                return;
            }


        }

        [Button("关动画")]
        public virtual void OnClose()
        {

        }


    }
}