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
    public static class RayUtility
    {
        public static QueryTriggerInteraction ToQueryTriggerInteraction(QueryTriggerInteractionType type)
        {
            QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;
            switch (type)
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

            return queryTriggerInteraction;
        }
    }
    
}