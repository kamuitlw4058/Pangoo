using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{

    public class DynamicObjectMouseInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [ShowInInspector]
        public DynamicObject dynamicObject { get; set; } = null;

        [ShowInInspector]
        public string Path { get; set; } = null;

        [ShowInInspector]
        public DynamicObjectMouseInteractType InteractType { get; set; }

        [ShowInInspector]
        public Vector3 InteractOffset;

        public float InteractAngle;


        public float Angle;

        public bool EnabledPointer = true;



        [ShowInInspector]
        public IHotspotRow HotspotRow { get; set; }

        [ShowInInspector]
        public HotSpot hotSpot { get; set; }

        [ShowInInspector]
        public bool PointerEnter { get; set; }

        [ShowInInspector]
        public Vector3 HotSpotOffset { get; set; }

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
                hotSpot.Offset = HotSpotOffset;
                hotSpot.LoadParamsFromJson(HotspotRow.Params);
            }
        }

        private void Update()
        {
            if (InteractOffset != Vector3.zero && dynamicObject.PlayerCameraTransform != null && InteractAngle <= 1)
            {
                var doDirection = (dynamicObject.CachedTransfrom.TransformPoint(InteractOffset) -
                                   dynamicObject.CachedTransfrom.position).normalized;
                var cameraDirection = (dynamicObject.PlayerCameraTransform.position - dynamicObject.CachedTransfrom.position).normalized;
                Angle = Vector3.Dot(doDirection, cameraDirection);

                if (hotSpot != null)
                {
                    hotSpot.Hide = !(Angle > InteractAngle);
                }
                EnabledPointer = Angle > InteractAngle;
            }
            else
            {
                EnabledPointer = true;
                hotSpot.Hide = false;
            }


            if (hotSpot != null)
            {
                hotSpot.Update();
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter = true;
            Debug.Log($"OnPointerEnter");
            if (dynamicObject != null && EnabledPointer)
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
            if (dynamicObject != null && EnabledPointer)
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
            if (dynamicObject != null && EnabledPointer)
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

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (dynamicObject != null && EnabledPointer)
            {
                switch (InteractType)
                {
                    case DynamicObjectMouseInteractType.Base:
                        // dynamicObject.(eventData);
                        break;
                    case DynamicObjectMouseInteractType.Extra:
                        dynamicObject.ExtraBeginDrag(eventData, Path);
                        break;
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (dynamicObject != null && EnabledPointer)
            {
                switch (InteractType)
                {
                    case DynamicObjectMouseInteractType.Base:
                        // dynamicObject.(eventData);
                        break;
                    case DynamicObjectMouseInteractType.Extra:
                        dynamicObject.ExtraDrag(eventData, Path);
                        break;
                }
            }
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            if (dynamicObject != null && EnabledPointer)
            {
                switch (InteractType)
                {
                    case DynamicObjectMouseInteractType.Base:
                        // dynamicObject.(eventData);
                        break;
                    case DynamicObjectMouseInteractType.Extra:
                        dynamicObject.ExtraEndDrag(eventData, Path);
                        break;
                }
            }
        }



    }
}