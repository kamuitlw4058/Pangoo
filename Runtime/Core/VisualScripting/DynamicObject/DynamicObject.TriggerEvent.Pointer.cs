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
            // Debug.Log($"DoPointerEnter:{gameObject.name}");
            base.DoPointerEnter(pointerEventData);
            CurrentArgs.PointerData = pointerEventData;
            TriggerInovke(TriggerTypeEnum.OnPointerEnter);
        }

        protected override void DoPointerExit(PointerEventData pointerEventData)
        {
            // Debug.Log($"DoPointerExit:{gameObject.name}");

            base.DoPointerExit(pointerEventData);
            CurrentArgs.PointerData = pointerEventData;
            TriggerInovke(TriggerTypeEnum.OnPointerExit);
        }

        protected override void DoPointerClick(PointerEventData pointerEventData)
        {
            base.DoPointerClick(pointerEventData);
            CurrentArgs.PointerData = pointerEventData;
            TriggerInovke(TriggerTypeEnum.OnPointerClick);

            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                TriggerInovke(TriggerTypeEnum.OnPointerClickLeft);
            }

            if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                TriggerInovke(TriggerTypeEnum.OnPointerClickRight);
            }
        }


    }
}