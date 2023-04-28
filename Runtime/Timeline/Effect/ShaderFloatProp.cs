using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;

namespace Pangoo
{
    [ExecuteAlways]

    public class ShaderFloatProp : MonoBehaviour
    {
        public string PropName = "_Threshold";
        public bool Enable = true;
        public Renderer[] effectRenderer;

        public float Value=1f;

        float m_Value;
        bool m_IsFirstUpdate = true;


        void Start()
        {
            if (effectRenderer == null)
            {
                effectRenderer = GetComponentsInChildren<Renderer>();
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (!string.IsNullOrEmpty(PropName)
                && Enable
                && effectRenderer != null
                && (m_Value != Value || m_IsFirstUpdate))
            {
                ShaderUtility.SetFloat(effectRenderer.ToList(), PropName, Value);
                m_IsFirstUpdate = false;
                m_Value = Value;
            }

        }
    }
}
