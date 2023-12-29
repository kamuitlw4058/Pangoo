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