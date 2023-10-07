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
        public TriggerEventTable.TriggerEventRow Row { get; set; }

        public InstructionList Instructions { get; set; }

        [ShowInInspector]
        [LabelText("触发点类型")]
        public virtual TriggerTypeEnum TriggerType => TriggerTypeEnum.Unknown;

        public virtual void OnAwake()
        {

        }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }

        public virtual void OnInvoke(TriggerEventParams eventParams)
        {

        }

        public virtual void LoadParamsFromJson(string val) { }
        public virtual string ParamsToJson()
        {
            return string.Empty;
        }
    }
}