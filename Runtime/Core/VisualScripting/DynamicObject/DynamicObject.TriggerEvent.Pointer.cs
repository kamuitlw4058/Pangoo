using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using Pangoo.Core.Characters;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using UnityEngine.EventSystems;



namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public Action<Args> TriggerOnPointerEnter;



        protected override void DoPointerEnter(PointerEventData pointerEventData)
        {
            base.DoPointerEnter(pointerEventData);
            CurrentArgs.PointerData = pointerEventData;
            TriggerInovke(TriggerTypeEnum.OnPointerEnter);
        }

        protected override void DoPointerExit(PointerEventData pointerEventData)
        {
            base.DoPointerExit(pointerEventData);
            CurrentArgs.PointerData = pointerEventData;
            TriggerInovke(TriggerTypeEnum.OnPointerExit);
        }

        protected override void DoPointerClick(PointerEventData pointerEventData)
        {
            base.DoPointerClick(pointerEventData);
            CurrentArgs.PointerData = pointerEventData;
            TriggerInovke(TriggerTypeEnum.OnPointerEnter);
        }
    }
}