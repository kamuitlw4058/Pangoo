using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using GameFramework.Event;
using Pangoo.Core.Common;
using Pangoo.Common;

namespace Pangoo.Core.VisualScripting
{
    [Category("通用/预览")]

    public class UIPreviewPanel : UIPanel
    {
        public UISubtitleParams ParamsRaw = new UISubtitleParams();

        protected override IParams Params => ParamsRaw;


        Vector3 OldPosition;

        GameObject PreviewGo;




        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            var previewData = PanelData.UserData as PreviewData;
            if (previewData == null) return;

            OldPosition = previewData.Go.transform.position;
            PreviewGo = previewData.Go;

        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (PreviewGo == null) return;
            var camera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();
            if (camera == null) return;

            if (PreviewGo.transform.position != camera.transform.TransformPoint(transform.forward * 0.3f))
            {
                PreviewGo.transform.position = MathUtility.Lerp(PreviewGo.transform.position, camera.transform.TransformPoint(transform.forward * 0.3f), 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PreviewGo != null)
                {
                    PreviewGo.transform.position = OldPosition;
                }
                CloseSelf();
            }

        }

    }
}