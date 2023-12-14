using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject:IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        bool m_IsEnterPointer;

        public bool IsEnterPointer
        {
            get
            {
                return m_IsEnterPointer;
            }
            set
            {
                m_IsEnterPointer = value;
            }
        }
        
        public Action<Args> PointerEnterEvent;

        public Action<Args> PointerExitEvent;
        public Action<Args> PointerClickEvent;
        
        void OnPointerEnterEvent(Args eventParams)
        {
            Debug.Log($"OnPointerEnterEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents.Values)
            {
                if (!trigger.Enabled) continue;
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnPointerEnter:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }
        
        void OnPointerExitEvent(Args eventParams)
        {
            Debug.Log($"OnPointerExitEvent:{gameObject.name}");
            foreach (var trigger in TriggerEvents.Values)
            {
                if (!trigger.Enabled) continue;
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnPointerExit:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }

        void OnPointerClickEvent(Args eventParams)
        {
            Debug.Log($"OnPointerClickEvent:{gameObject.name}");
            
            foreach (var trigger in TriggerEvents.Values)
            {
                if (!trigger.Enabled) continue;
                switch (trigger.TriggerType)
                {
                    case TriggerTypeEnum.OnPointerClick:
                        trigger.OnInvoke(eventParams);
                        break;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsEnterPointer = true;
            PointerEnterEvent?.Invoke(CurrentArgs);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsEnterPointer = false;
            PointerExitEvent?.Invoke(CurrentArgs);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClickEvent?.Invoke(CurrentArgs);
        }
    }
}
