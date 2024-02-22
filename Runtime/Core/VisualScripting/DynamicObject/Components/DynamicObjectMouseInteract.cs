using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    public enum DynamicObjectMouseInteractType
    {
        Extra,
        Base,
    }

    public class DynamicObjectMouseInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        public DynamicObject dynamicObject { get; set; } = null;

        public string Path { get; set; } = null;

        public DynamicObjectMouseInteractType InteractType { get; set; }

        public IHotspotRow HotspotRow { get; set; }

        public HotSpot hotSpot { get; set; }

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