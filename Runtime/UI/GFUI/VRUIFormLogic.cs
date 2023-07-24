using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;

namespace Pangoo{

    public class VRUIFormLogic : UIFormLogic
    {
        [SerializeField][ReadOnly][FoldoutGroup("Canvas")] Camera m_MainCamera;
        [SerializeField][ReadOnly][FoldoutGroup("Canvas")] Canvas PanelCanvas;
        [SerializeField][FoldoutGroup("Canvas")] float PanelDistance = 0.5f;
         Camera MainCamera{
            get{
                if(m_MainCamera == null){
                    var MainCameraGameObject= GameObject.FindGameObjectWithTag("MainCamera");
                    if(MainCameraGameObject != null){
                        m_MainCamera = MainCameraGameObject.GetComponent<Camera>();
                    }
                }
                return m_MainCamera;
            }
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            PanelCanvas =  transform.GetOrAddComponent<Canvas>();
            if(MainCamera != null){
                PanelCanvas.renderMode = RenderMode.ScreenSpaceCamera;
                PanelCanvas.planeDistance = PanelDistance;
                PanelCanvas.worldCamera = MainCamera;
            }
        }

    }
}