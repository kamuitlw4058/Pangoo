using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    [Serializable]
    public abstract class TriggerEvent : TPolymorphicItem<TriggerEvent>
    {
        TriggerEventTable.TriggerEventRow Row { get; set; }

        [ShowInInspector]
        [LabelText("触发点类型")]
        protected virtual TriggerTypeEnum TriggerType => TriggerTypeEnum.Unknown;

        protected virtual void OnAwake() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        protected virtual void OnInvoke(TriggerEventParams eventParams)
        {

        }

        public virtual void LoadParamsFromJson(string val) { }
        public virtual string ParamsToJson()
        {
            return string.Empty;
        }
    }
}