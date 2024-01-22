using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Common
{
    
    public enum QueryTriggerInteractionType
    {
        [LabelText("只检测Collider")]
        Ignore,
        [LabelText("检测Collider和Trigger")]
        Collider,
        [LabelText("使用Unity项目设置")]
        UseGlobal
    }
    [System.Serializable]
    public class RayUtility
    {
        public Ray ray;
        public RaycastHit hit;
        public float rayLength=100f;
        public LayerMask layerMask;


        private QueryTriggerInteractionType m_QueryTriggerInteractionType;
        [LabelText("设置射线检测条件")]
        [ShowInInspector]
        public QueryTriggerInteractionType QueryTriggerInteractionType
        {
            get => m_QueryTriggerInteractionType;
            set
            {
                switch (value)
                {
                    case QueryTriggerInteractionType.Ignore:
                        queryTriggerInteraction = QueryTriggerInteraction.Ignore;
                        break;
                    case QueryTriggerInteractionType.Collider:
                        queryTriggerInteraction = QueryTriggerInteraction.Collide;
                        break;
                    case QueryTriggerInteractionType.UseGlobal:
                        queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
                        break;
                }

                m_QueryTriggerInteractionType=value;
            }
        }
        [ReadOnly]
        public QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;
        [ReadOnly]
        public Collider TargetCollider;
        [ReadOnly]
        public Collider HitCollider;
    }
    
}