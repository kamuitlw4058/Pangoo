using System;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo;
using Pangoo.Core.VisualScripting;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using Pangoo.Core.Services;


#if USE_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace Pangoo
{
    public class EntityDynamicObject : EntityBase
    {
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

        [SerializeField] Collider EnterCollider;

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

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            DoData = userData as EntityDynamicObjectData;
            if (DoData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }

            transform.position = DoData.InfoRow.Position;
            transform.rotation = DoData.InfoRow.Rotation;
            Name = Utility.Text.Format("{0}[{1}]", DoData.EntityInfo.AssetName, DoData.InfoRow.Id);

            Debug.Log("Create DynamicObject");
            DynamicObj = DynamicObject.Create(gameObject);
            DynamicObj.Row = DoData.InfoRow.m_DynamicObjectRow;
            DynamicObj.TableService = DoData?.Service?.TableService;
            DynamicObj.CurrentArgs = new Core.Common.Args();
            DynamicObj.Main = DoData.Service.Parent as MainService;
            DynamicObj.Awake();
            DynamicObj.Start();

        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
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
            Debug.Log($"EntityDynamicObject OnTriggerEnter");
            DynamicObj?.TriggerEnter3d(other);
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"EntityDynamicObject OnTriggerExit");
            DynamicObj?.TriggerExit3d(other);
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            DynamicObj?.PointerEnter(pointerEventData);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            DynamicObj?.PointerExit(pointerEventData);
        }


    }
}