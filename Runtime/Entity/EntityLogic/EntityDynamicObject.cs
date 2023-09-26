using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo.Service;
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

        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

        }



    }
}