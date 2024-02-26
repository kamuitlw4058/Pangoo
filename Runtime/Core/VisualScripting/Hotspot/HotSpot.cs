using System;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Pangoo.MetaTable;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    // [Image(typeof(IconCircleOutline), ColorTheme.Type.Yellow)]

    [Serializable]
    public abstract class HotSpot : MonoSubService<DynamicObject>
    {
        public IHotspotRow Row { get; set; }

        public DynamicObject dynamicObject { get; set; }

        public GameObject Target { get; set; }

        Transform m_TargetTransform;


        public Vector3 Offset { get; set; }

        public Transform TargetTransform
        {
            get
            {
                if (m_TargetTransform == null)
                {
                    m_TargetTransform = Target?.transform;
                }
                return m_TargetTransform;
            }

        }

        public bool Hide { get; set; }
        public virtual void LoadParamsFromJson(string val) { }
        public virtual string ParamsToJson()
        {
            return "{}";
        }

    }
}