using System;
using System.Collections.Generic;

using UnityEngine;
using Pangoo.Core.Common;
using UnityEngine.EventSystems;
using LitJson;
using Pangoo.MetaTable;

namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public Action<Args> TriggerOnPointerEnter;

        List<DynamicObjectMouseInteractInfo> m_DynamicObjectMouseInteractInfos;

        public List<DynamicObjectMouseInteract> dynamicObjectMouseInteracts = new List<DynamicObjectMouseInteract>();

        void DoAwakeMouseInteract()
        {
            if (Row.MouseInteractList.IsNullOrWhiteSpace()) return;
            try
            {
                m_DynamicObjectMouseInteractInfos = JsonMapper.ToObject<List<DynamicObjectMouseInteractInfo>>(Row.MouseInteractList);
            }
            catch
            {

            }

            if (m_DynamicObjectMouseInteractInfos != null)
            {
                foreach (var mouseInteractInfo in m_DynamicObjectMouseInteractInfos)
                {
                    var collider = CachedTransfrom.Find(mouseInteractInfo.Path)?.GetComponent<Collider>();
                    if (collider != null)
                    {
                        collider.gameObject.layer = LayerMask.NameToLayer("RayHitObj");
                        var com = collider.transform.GetOrAddComponent<DynamicObjectMouseInteract>();
                        if (com != null)
                        {
                            com.dynamicObject = this;
                            com.Path = mouseInteractInfo.Path;
                            IHotspotRow row = HotspotRowExtension.GetByUuid(mouseInteractInfo.HotSpotUuid, m_HotspotHandler);
                            com.HotspotRow = row;
                            com.HotSpotOffset = mouseInteractInfo.HotSpotOffset;
                            com.InteractType = mouseInteractInfo.MouseInteractType;
                            com.InteractOffset = mouseInteractInfo.InteractOffset;
                            com.InteractAngle = mouseInteractInfo.InteractAngle;
                            dynamicObjectMouseInteracts.Add(com);
                        }
                    }

                }
            }
        }


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

        public void ExtraPointerEnter(PointerEventData pointerEventData, string pointerPath = null)
        {
            CurrentArgs.PointerData = pointerEventData;
            CurrentArgs.PointerPath = pointerPath;
            TriggerInovke(TriggerTypeEnum.OnExtraPointerEnter);
        }

        public void ExtraPointerExit(PointerEventData pointerEventData, string pointerPath = null)
        {
            CurrentArgs.PointerData = pointerEventData;
            CurrentArgs.PointerPath = pointerPath;
            TriggerInovke(TriggerTypeEnum.OnExtraPointerExit);
        }
        public void ExtraPointerClick(PointerEventData pointerEventData, string pointerPath = null)
        {
            CurrentArgs.PointerData = pointerEventData;
            CurrentArgs.PointerPath = pointerPath;
            TriggerInovke(TriggerTypeEnum.OnExtraPointerClick);
        }


        public void ExtraBeginDrag(PointerEventData pointerEventData, string pointerPath = null)
        {
            CurrentArgs.PointerData = pointerEventData;
            CurrentArgs.PointerPath = pointerPath;
            TriggerInovke(TriggerTypeEnum.OnExtraBeginDrag);
        }


        public void ExtraDrag(PointerEventData pointerEventData, string pointerPath = null)
        {
            CurrentArgs.PointerData = pointerEventData;
            CurrentArgs.PointerPath = pointerPath;
            TriggerInovke(TriggerTypeEnum.OnExtraDrag);
        }

        public void ExtraEndDrag(PointerEventData pointerEventData, string pointerPath = null)
        {
            CurrentArgs.PointerData = pointerEventData;
            CurrentArgs.PointerPath = pointerPath;
            TriggerInovke(TriggerTypeEnum.OnExtraEndDrag);
        }
    }
}