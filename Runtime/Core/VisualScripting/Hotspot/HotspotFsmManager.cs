using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Serializable]
    public class HotspotFsmManager
    {
        public Dictionary<int, string> HotspotStateMapper = new Dictionary<int, string>();

        public GameObject Go;

        public List<HotSpotTransition> TransList = new List<HotSpotTransition>();

        public float TransitionTime;

        public bool TransitionInvert;

        [ShowInInspector]
        public bool IsTransition
        {
            get
            {
                return Transition != null;
            }
        }

        SpriteRenderer m_SpriteRenderer;
        public SpriteRenderer SpriteRenderer
        {
            get
            {
                if (m_SpriteRenderer == null)
                {
                    m_SpriteRenderer = Go.GetComponent<SpriteRenderer>();
                }
                return m_SpriteRenderer;
            }
        }

        [ShowInInspector]
        public HotSpotTransition LatestTransition { get; set; }


        HotSpotTransition m_Transition;
        [ShowInInspector]
        public HotSpotTransition Transition
        {
            get
            {
                return m_Transition;
            }
            set
            {
                LatestTransition = m_Transition;
                m_Transition = value;
            }
        }



    }
}