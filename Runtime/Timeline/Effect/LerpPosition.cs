using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
using DG.Tweening.Core;

namespace Pangoo
{
    [ExecuteAlways]
    public class LerpPosition : MonoBehaviour
    {
        public bool Enable = true;

        public float Value;

        public Transform Target;
        
        public Transform Start;
        public Transform End;

        public float m_Value1;
         public ShaderFloatProp Prop;
        bool m_IsFirstUpdate = true;

        public float propFactor = 1f;

        public float propOffset = 0.1f;



        // Update is called once per frame
        void Update()
        {
            if (Enable
                && (m_Value1 != Value || m_IsFirstUpdate))
            {
                m_IsFirstUpdate = false;
                m_Value1 = Value;
                Target.transform.localPosition =  Vector3.Lerp(Start.transform.localPosition,End.transform.localPosition,Value);
                Prop.Value = (Value * propFactor) + propOffset;
            }

        }
    }
}
