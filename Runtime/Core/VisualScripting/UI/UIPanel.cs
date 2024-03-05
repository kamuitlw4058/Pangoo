using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;

namespace Pangoo.Core.VisualScripting
{

    public abstract class UIPanel : UIFormLogic, IParams
    {
        protected UIPanelData PanelData;
        protected RectTransform rectTransform;

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

        protected void SetupCursor()
        {
            m_CursorVisible = Cursor.visible;
            m_CursorLockState = Cursor.lockState;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        protected void RecoverCursor()
        {
            Cursor.visible = m_CursorVisible;
            Cursor.lockState = m_CursorLockState;
        }


        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            rectTransform = GetComponent<RectTransform>();
            rectTransform?.SetUIPanelDefault();
            PanelData = userData as UIPanelData;
            // Debug.Log($"PanelData:");
            Load(PanelData.InfoRow.Params);

        }
        protected abstract IParams Params { get; }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            ReferencePool.Release(PanelData);
        }

        public void CloseSelf()
        {
            PangooEntry.UI?.CloseUIForm(UIForm);
        }

        public void Load(string val)
        {
            Params.Load(val);
        }

        public string Save()
        {
            return Params.Save();
        }
    }
}