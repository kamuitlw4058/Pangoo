using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using System.Linq;

namespace Pangoo
{

    [ExecuteAlways]
    [RequireComponent(typeof(Animator))]
    public class ShaderFloatColorProp : MonoBehaviour
    {
        public string FloatPropName;

        public string ColorPropName;
        public bool Enable = true;
        public List<Renderer> effectRenderer=new List<Renderer>();

        public float FloatValue;


        [ColorUsageAttribute(true, true)]
        public Color ColorValue;

        float m_Value;
        Color m_Color;
        bool m_IsFirstUpdate = true;




        void Start()
        {
            if (effectRenderer == null)
            {
                effectRenderer.AddRange(GetComponentsInChildren<Renderer>());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!string.IsNullOrEmpty(FloatPropName) && !string.IsNullOrEmpty(ColorPropName)
                && Enable
                && effectRenderer != null
                && (m_Value != FloatValue || m_Color != ColorValue || m_IsFirstUpdate))
            {
                ShaderUtility.SetFloat(effectRenderer.ToList(), FloatPropName, FloatValue);
                ShaderUtility.SetColor(effectRenderer.ToList(), ColorPropName, ColorValue);
                m_IsFirstUpdate = false;
                m_Value = FloatValue;
                m_Color = ColorValue;
            }

        }
    }
}
