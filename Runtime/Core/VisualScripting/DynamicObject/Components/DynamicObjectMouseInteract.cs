using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    public class DynamicObjectMouseInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [ShowInInspector]
        public DynamicObject dynamicObject { get; set; } = null;

        [ShowInInspector]
        public string Path { get; set; } = null;

        [ShowInInspector]
        public DynamicObjectMouseInteractType InteractType { get; set; }


        [ShowInInspector]
        public IHotspotRow HotspotRow { get; set; }

        [ShowInInspector]
        public HotSpot hotSpot { get; set; }

        [ShowInInspector]
        public bool PointerEnter { get; set; }

        void Start()
        {

            if (dynamicObject != null && HotspotRow != null)
            {
                hotSpot = ClassUtility.CreateInstance<HotSpot>(HotspotRow.HotspotType);
                if (hotSpot == null)
                {
                    return;
                }
                hotSpot.Row = HotspotRow;
                hotSpot.dynamicObject = dynamicObject;
                hotSpot.Master = dynamicObject;
                hotSpot.Target = gameObject;
                hotSpot.LoadParamsFromJson(HotspotRow.Params);
            }
        }


        private void Update()
        {
            if (hotSpot != null)
            {
                hotSpot.Update();
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter = true;
            Debug.Log($"OnPointerEnter");
            if (dynamicObject != null)
            {
                switch (InteractType)
                {
                    case DynamicObjectMouseInteractType.Base:
                        // dynamicObject.
                        dynamicObject.PointerEnter(eventData);
                        break;
                    case DynamicObjectMouseInteractType.Extra:
                        dynamicObject.ExtraPointerEnter(eventData, Path);
                        break;
                }
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerEnter = false;
            Debug.Log($"OnPointerExit");
            if (dynamicObject != null)
            {
                switch (InteractType)
                {
                    case DynamicObjectMouseInteractType.Base:
                        // dynamicObject.
                        dynamicObject.PointerExit(eventData);
                        break;
                    case DynamicObjectMouseInteractType.Extra:
                        dynamicObject.ExtraPointerExit(eventData, Path);
                        break;
                }
            }

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (dynamicObject != null)
            {
                switch (InteractType)
                {
                    case DynamicObjectMouseInteractType.Base:
                        dynamicObject.PointerClick(eventData);
                        break;
                    case DynamicObjectMouseInteractType.Extra:
                        dynamicObject.ExtraPointerClick(eventData, Path);
                        break;
                }
            }
        }


    }
}