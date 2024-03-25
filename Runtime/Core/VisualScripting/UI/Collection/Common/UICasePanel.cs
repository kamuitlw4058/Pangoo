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
using Pangoo.MetaTable;


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





        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);




            m_Text = GetComponentInChildren<TextMeshProUGUI>();
            m_NameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            m_DescText = transform.Find("Desc").GetComponent<TextMeshProUGUI>();

            MainCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();

            m_Text?.gameObject.SetActive(false);
            m_NameText?.gameObject.SetActive(false);
            m_DescText?.gameObject.SetActive(false);


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