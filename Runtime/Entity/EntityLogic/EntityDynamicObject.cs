using System;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo;
using Pangoo.Core.VisualScripting;

using UnityEngine;
using UnityEngine.EventSystems;
using Pangoo.Core.Services;


#if USE_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace Pangoo
{
    public class EntityDynamicObject : EntityBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public override string EntityName => "EntityDynamicObject";
        [ShowInInspector]
        public EntityInfo Info
        {
            get
            {
                if (DoData != null)
                {
                    return DoData.EntityInfo;
                }
                return null;
            }
        }


        [ShowInInspector]
        public EntityDynamicObjectData DoData;


        [ShowInInspector]
        [field: NonSerialized]
        [LabelText("动态物体")]
        [HideReferenceObjectPicker]
        public DynamicObject DynamicObj { get; set; }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

        }

        public bool IsStarted;

        public void UpdateDefaultTransform()
        {
            switch (DoData.InfoRow.m_DynamicObjectRow.Space.ToEnum<Space>())
            {
                case Space.World:
                    transform.position = DoData.InfoRow.Position;
                    transform.rotation = DoData.InfoRow.Rotation;
                    if (DoData.InfoRow.Scale == Vector3.zero)
                    {
                        transform.localScale = Vector3.one;
                    }
                    else
                    {
                        transform.localScale = DoData.InfoRow.Scale;
                    }


                    break;
                case Space.Self:
                    transform.localPosition = DoData.InfoRow.Position;
                    transform.localRotation = DoData.InfoRow.Rotation;
                    if (DoData.InfoRow.Scale == Vector3.zero)
                    {
                        transform.localScale = Vector3.one;
                    }
                    else
                    {
                        transform.localScale = DoData.InfoRow.Scale;
                    }
                    break;
            }
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            DoData = userData as EntityDynamicObjectData;
            if (DoData == null)
            {
                LogError("Entity data is invalid.");
                return;
            }
            UpdateDefaultTransform();


            Name = Utility.Text.Format("{0}[{1}]", DoData.InfoRow.Name, DoData.InfoRow.UuidShort);

            Log($"OnShow DynamicObject:{DoData.InfoRow.UuidShort}-{DoData.InfoRow.Name}");
            DynamicObj = DynamicObject.Create(gameObject);
            DynamicObj.Row = DoData.InfoRow.m_DynamicObjectRow;
            DynamicObj.TableService = DoData?.Service?.TableService;
            DynamicObj.CurrentArgs = new Core.Common.Args();
            DynamicObj.Main = DoData.Service.Parent as MainService;
            DynamicObj.Entity = this;
            DynamicObj.Awake();
            IsStarted = false;

        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {

            if (!IsStarted)
            {
                DynamicObj.Start();
                IsStarted = true;
            }
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            DynamicObj?.Update();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            DynamicObj.Disable();
            ReferencePool.Release(DynamicObj);
            base.OnHide(isShutdown, userData);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                Log($"EntityDynamicObject OnTriggerEnter,{DoData.InfoRow.Name},{other.gameObject.name}");
                DynamicObj?.TriggerEnter3d(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                Log($"EntityDynamicObject OnTriggerExit");
                DynamicObj?.TriggerExit3d(other);
            }
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            DynamicObj?.PointerEnter(pointerEventData);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            DynamicObj?.PointerExit(pointerEventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            DynamicObj?.PointerClick(eventData);
        }
    }
}