using System;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo;
using Pangoo.Core.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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
        public DynamicObjectService DynamicObjectService { get; set; }

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
            Name = Utility.Text.Format("{0}[{1}]", DoData.EntityInfo.AssetName, Id);

            DynamicObjectService = DynamicObjectService.Create(gameObject);
            DynamicObjectService.Row = DoData.InfoRow.m_DynamicObjectRow;
            DynamicObjectService.TableService = DoData.Service.TableService;
            DynamicObjectService.Awake();
            DynamicObjectService.Start();

        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            DynamicObjectService?.Update();
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            ReferencePool.Release(DynamicObjectService);
            base.OnHide(isShutdown, userData);

        }



    }
}