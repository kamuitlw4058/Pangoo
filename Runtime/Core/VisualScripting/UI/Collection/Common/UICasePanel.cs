using System;
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
    [Category("通用/案件")]

    public class UICasePanel : UIPanel
    {


        public UICaseParams ParamsRaw = new UICaseParams();

        protected override IParams Params => ParamsRaw;

        public TextMeshProUGUI m_Text;

        public TextMeshProUGUI m_NameText;

        public TextMeshProUGUI m_DescText;




        Camera MainCamera { get; set; }


        public Vector3 TargetPoint
        {
            get
            {
                return MainCamera.transform.TransformPoint(transform.forward * 0.3f);
            }
        }


        public CaseContent Data;





        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            Data = PanelData.UserData as CaseContent;
            if (Data == null) return;

            Data.ShowCaseDynamicObject();


            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            m_NameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            m_DescText = transform.Find("Desc").GetComponent<TextMeshProUGUI>();

            MainCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();


            // PreviewData.args.Main.CharacterService.SetPlayerControllable(false);

            // m_Text.text = PreviewData.PreviewRow.Title;
            // if (m_NameText != null)
            // {
            //     m_NameText.text = PreviewData.Name;
            // }

            // if (m_DescText != null)
            // {
            //     m_DescText.text = PreviewData.Desc;
            // }


            SetupCursor();


        }




        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (MainCamera == null)
            {
                Debug.Log($"Main Camera Is Null");
                return;
            }


        }


    }
}