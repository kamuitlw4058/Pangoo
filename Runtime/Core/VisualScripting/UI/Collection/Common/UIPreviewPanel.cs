using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using GameFramework.Event;
using Pangoo.Core.Common;
using Pangoo.Common;
using System.Linq;
using System.Runtime.InteropServices;


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


        public UIPreviewParams ParamsRaw = new UIPreviewParams();

        protected override IParams Params => ParamsRaw;

        public TextMeshProUGUI m_Text;

        public TextMeshProUGUI m_NameText;

        public TextMeshProUGUI m_DescText;





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

        bool m_CursorVisible;
        CursorLockMode m_CursorLockState;

        [ShowInInspector]
        public bool CursorVisible
        {
            get
            {
                return Cursor.visible;
            }
            set
            {
                Cursor.visible = value;
            }
        }

        [ShowInInspector]

        public CursorLockMode CursorLockState
        {
            get
            {
                return Cursor.lockState;
            }
            set
            {
                Cursor.lockState = value;
            }
        }

        public float DragFactorX = 1;
        public float DragFactorY = 1;


        public AnimationCurve curve;



        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            PreviewData = PanelData.UserData as PreviewData;
            if (PreviewData == null) return;

            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            m_NameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            m_DescText = transform.Find("Desc").GetComponent<TextMeshProUGUI>();

            MainCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();

            PreviewData.args.Main.CharacterService.SetPlayerControllable(false);

            m_Text.text = PreviewData.PreviewRow.Title;
            if (m_NameText != null)
            {
                m_NameText.text = PreviewData.Name;
            }

            if (m_DescText != null)
            {
                m_DescText.text = PreviewData.Desc;
            }
            DragFactorX = PreviewData.Params.DragFactorX;
            DragFactorY = PreviewData.Params.DragFactorY;

            SetupCursor();

            State = PreviewState.OnShow;
            GrabTime = 0;
            CloseTime = 0;

        }

        void SetupCursor()
        {
            m_CursorVisible = Cursor.visible;
            m_CursorLockState = Cursor.lockState;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void RecoverCursor()
        {
            Cursor.visible = m_CursorVisible;
            Cursor.lockState = m_CursorLockState;
        }

        public float GrabDuration = 0.3f;
        public float GrabTime;
        public float GrabProgress
        {
            get
            {
                return GrabTime / GrabDuration;
            }
        }

        public float CloseDuration = 0.3f;

        public float CloseTime;

        public float CloseProgress
        {
            get
            {
                return CloseTime / CloseDuration;
            }
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
                if (GrabTime < GrabDuration)
                {
                    GrabTime += elapseSeconds;
                    PreviewData.CurrentPosition = MathUtility.Lerp(PreviewData.CurrentPosition, TargetPoint, GrabProgress);
                    if (PreviewData.DynamicObject.Row.PreviewScale != Vector3.zero)
                    {
                        PreviewData.CurrentScale = MathUtility.Lerp(PreviewData.OldScale, PreviewData.DynamicObject.Row.PreviewScale, GrabProgress);
                    }

                    Vector3 direction = MainCamera.transform.position - PreviewData.OldPosition;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    var progress = curve.Evaluate(GrabProgress);

                    PreviewData.CurrentRotation = Quaternion.Lerp(Quaternion.Euler(PreviewData.OldRotation), targetRotation, progress).eulerAngles;
                    return;
                }

            }

            if (State == PreviewState.OnGrab)
            {
                PreviewData.CurrentPosition = TargetPoint;
                Vector3 direction = MainCamera.transform.position - PreviewData.OldPosition;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                PreviewData.CurrentRotation = targetRotation.eulerAngles;

                if (PreviewData.DynamicObject.Row.PreviewScale != Vector3.zero)
                {
                    PreviewData.CurrentScale = PreviewData.DynamicObject.Row.PreviewScale;
                }
                State = PreviewState.OnPreview;
            }


            if (State == PreviewState.OnPreview)
            {
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

                OnDrag();

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
                    var variableUuid = PreviewData.args.Main.DefaultPreviewExitVariableUuid;
                    if (!variableUuid.IsNullOrWhiteSpace())
                    {
                        var isCloseImmediately = PreviewData.DynamicObject.GetVariable<bool>(variableUuid);
                        if (isCloseImmediately)
                        {
                            RecoverCursor();
                            Debug.Log($"立即退出 预览关闭:{isCloseImmediately} :{variableUuid}");
                            PreviewData.args.Main.CharacterService.SetPlayerControllable(true);
                            CloseSelf();
                            return;
                        }
                    }

                    State = PreviewState.OnClosing;
                }
            }

            if (State == PreviewState.OnClosing)
            {
                if (CloseTime < CloseDuration)
                {
                    CloseTime += elapseSeconds;
                    PreviewData.CurrentPosition = MathUtility.Lerp(TargetPoint, PreviewData.OldPosition, CloseProgress);
                    if (PreviewData.DynamicObject.Row.PreviewScale != Vector3.zero)
                    {
                        PreviewData.CurrentScale = MathUtility.Lerp(PreviewData.DynamicObject.Row.PreviewScale, PreviewData.OldScale, CloseProgress);
                    }

                    Vector3 direction = MainCamera.transform.position - PreviewData.OldPosition;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    var progress = curve.Evaluate(GrabTime / GrabDuration);
                    PreviewData.CurrentRotation = Quaternion.Lerp(targetRotation, Quaternion.Euler(PreviewData.OldRotation), progress).eulerAngles;
                    return;
                }

                State = PreviewState.OnClose;

            }

            if (State == PreviewState.OnClose)
            {
                Debug.Log($"正常 预览关闭");
                PreviewData.CurrentScale = PreviewData.OldScale;
                PreviewData.CurrentRotation = PreviewData.OldRotation;
                PreviewData.CurrentPosition = PreviewData.OldPosition;
                RecoverCursor();
                PreviewData.args.Main.CharacterService.SetPlayerControllable(true);
                CloseSelf();
            }

        }

        public void OnDrag()
        {
            if (MainCamera == null)
            {
                return;
            }
            
            var upAxis = MainCamera.transform.TransformDirection(transform.up);
            var rightAxis = MainCamera.transform.TransformDirection(transform.right);
            
            if (Input.GetMouseButton(0))
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");
                
                PreviewData.Rotate(upAxis, -x * DragFactorX * Time.deltaTime, Space.World);
                PreviewData.Rotate(rightAxis, y * DragFactorY * Time.deltaTime, Space.World);
            }
        }
    }
}