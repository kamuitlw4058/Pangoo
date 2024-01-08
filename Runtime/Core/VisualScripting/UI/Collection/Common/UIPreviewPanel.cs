using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using GameFramework.Event;
using Pangoo.Core.Common;
using Pangoo.Common;
using System.Linq;

namespace Pangoo.Core.VisualScripting
{
    [Category("通用/预览")]

    public class UIPreviewPanel : UIPanel
    {
        public enum PreviewState
        {
            None,
            OnShow,
            OnGrab,
            OnPreview,
            OnClosing,
            OnClose,
        }


        public UISubtitleParams ParamsRaw = new UISubtitleParams();

        protected override IParams Params => ParamsRaw;

        public TextMeshProUGUI m_Text;


        public PreviewData PreviewData;


        public PreviewState m_PreviewState = PreviewState.None;

        PreviewState State
        {
            get
            {
                return m_PreviewState;
            }
            set
            {
                if (m_PreviewState == value) return;

                m_PreviewState = value;
                switch (value)
                {
                    case PreviewState.OnShow:
                        OnShow();
                        break;
                    case PreviewState.OnGrab:
                        OnGrab();
                        break;
                    case PreviewState.OnPreview:
                        OnPreview();
                        break;
                    case PreviewState.OnClosing:
                        OnClosing();
                        break;
                }
            }
        }

        Camera MainCamera { get; set; }


        public Vector3 TargetPoint
        {
            get
            {
                return MainCamera.transform.TransformPoint(transform.forward * 0.3f);
            }
        }

        void OnShow()
        {

        }

        void OnGrab()
        {

        }


        void OnPreview()
        {
        }


        void OnClosing()
        {

        }



        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            PreviewData = PanelData.UserData as PreviewData;
            if (PreviewData == null) return;

            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            MainCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();

            PreviewData.args.Main.CharacterService.SetPlayerControllable(false);

            m_Text.text = PreviewData.PreviewRow.Title;
            State = PreviewState.OnShow;

        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (MainCamera == null)
            {
                Debug.Log($"Main Camera Is Null");
                return;
            }

            if ((State == PreviewState.OnShow || State == PreviewState.OnGrab))
            {
                State = PreviewState.OnGrab;
                if ((PreviewData.CurrentPosition - TargetPoint).magnitude > 0.01)
                {
                    PreviewData.CurrentPosition = MathUtility.Lerp(PreviewData.CurrentPosition, TargetPoint, 0.5f);
                    return;
                }
            }

            if (State == PreviewState.OnGrab)
            {
                State = PreviewState.OnPreview;
            }


            if (State == PreviewState.OnPreview)
            {
                PreviewData.CurrentPosition = TargetPoint;
                bool flag = false;
                var InteractKeyCodes = PreviewData.InteractKeyCodes;
                for (int i = 0; i < InteractKeyCodes.Length; i++)
                {
                    if (Input.GetKeyDown(InteractKeyCodes[i]))
                    {
                        var variableUuid = PreviewData.args.Main.DefaultPreviewInteraceVariableUuid;
                        if (!variableUuid.IsNullOrWhiteSpace())
                        {
                            PreviewData.DynamicObject.SetVariable<int>(variableUuid, i);
                        }
                        PreviewData.DynamicObject?.OnPreview();
                    }
                }


                var ExitKeyCodes = PreviewData.ExitKeyCodes;
                if (ExitKeyCodes.Length == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        flag = true;
                    }
                }
                else
                {
                    foreach (var keyCode in ExitKeyCodes)
                    {
                        if (Input.GetKeyDown(keyCode))
                        {
                            flag = true;
                        }
                    }
                }

                if (flag)
                {
                    State = PreviewState.OnClosing;
                }
            }

            if (State == PreviewState.OnClosing)
            {
                if ((PreviewData.CurrentPosition - PreviewData.OldPosition).magnitude > 0.01)
                {
                    PreviewData.CurrentPosition = MathUtility.Lerp(PreviewData.CurrentPosition, PreviewData.OldPosition, 0.5f);
                    return;
                }
                else
                {
                    PreviewData.CurrentPosition = PreviewData.OldPosition;
                    State = PreviewState.OnClose;
                }
            }

            if (State == PreviewState.OnClose)
            {
                PreviewData.args.Main.CharacterService.SetPlayerControllable(true);
                CloseSelf();
            }

        }

    }
}