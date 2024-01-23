using Pangoo.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.Pangoo.Plugins.PangooCommon.Helper
{
    [System.Serializable]
    public class RayHelper
    {
        public Ray ray;
        public RaycastHit hit;
        public float rayLength=100f;
        public LayerMask layerMask;
        
        [ReadOnly]
        public Collider TargetCollider;
        [ReadOnly]
        public Collider HitCollider;
        
        public QueryTriggerInteractionType m_QueryTriggerInteractionType;
        
        [LabelText("设置射线检测条件")]
        [ShowInInspector]
        public QueryTriggerInteractionType QueryTriggerInteractionType
        {
            get => m_QueryTriggerInteractionType;
            set
            {
                queryTriggerInteraction = RayUtility.ToQueryTriggerInteraction(value);
                m_QueryTriggerInteractionType=value;
            }
        }
        [ReadOnly]
        public QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;
    }
}