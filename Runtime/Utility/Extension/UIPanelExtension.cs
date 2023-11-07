using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using System;

namespace Pangoo
{
    public static class UIPanelExtension
    {
        private static int s_SerialId = 0;

        public static int ShowUI(this UIComponent uiComponent, UIPanelData uiInfo)
        {



            if (uiInfo == null)
            {
                // Log.Error("Can not load entity id '{0}' from data table.", entityId.ToString());
                return 0;
            }



            // if (LogicType == null)
            // {
            //     return 0;
            // }

            // 真是的去创建实体。
            // var serialId = uiComponent.GenerateSerialId();
            // Debug.Log($"entityComponent:{uiComponent} entityInfo:{entityInfo} entityInfo:{entityInfo.GroupName}");
            // if (!uiComponent.HasEntityGroup(entityInfo.GroupName))
            // {
            //     // PoolParamData poolParamData = entityData.EntityGroupData.PoolParamData;
            //     // PangooEntry.Entity.AddEntityGroup(entityInfo.GroupName, poolParamData.InstanceAutoReleaseInterval, poolParamData.InstanceCapacity, poolParamData.InstanceExpireTime, poolParamData.InstancePriority);
            //     PangooEntry.Entity.AddEntityGroup(entityInfo.GroupName, (float)entityInfo.GroupInstanceAutoReleaseInterval, entityInfo.GroupInstanceCapacity, (float)entityInfo.GroupInstanceExpireTime, entityInfo.GroupInstancePriority);
            // }

            return uiComponent.OpenUIForm(uiInfo.AssetPath, "Default", Constant.AssetPriority.UIFormAsset, uiInfo);
        }

        // private static int GenerateSerialId(this EntityComponent entityComponent)
        // {
        //     return ++s_SerialId;
        // }

    }

}