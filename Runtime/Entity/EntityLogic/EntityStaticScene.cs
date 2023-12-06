using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;
using System;

#if USE_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace Pangoo
{
    public class EntityStaticScene : EntityBase
    {
        [ShowInInspector]
        public EntityInfo Info
        {
            get
            {
                if (SceneData != null)
                {
                    return SceneData.EntityInfo;
                }
                return null;
            }
        }

        [SerializeField] Collider EnterCollider;

        [ShowInInspector]
        public EntityStaticSceneData SceneData;

        [SerializeField]
        bool IsOpenProbe = true;
#if USE_HDRP
        [field: NonSerialized]
        [ShowInInspector]
        PlanarReflectionProbe[] PlanarProbes;

        public void SetProbeEnabled(bool val)
        {
            if (PlanarProbes == null) return;

            foreach (var probe in PlanarProbes)
            {
                probe.enabled = val;
            }
        }

#endif

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            SceneData = userData as EntityStaticSceneData;
            if (SceneData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }

            Name = Utility.Text.Format("{0}[{1}]", SceneData.Name, Id);
#if USE_HDRP
            PlanarProbes = GetComponentsInChildren<PlanarReflectionProbe>();
            SetProbeEnabled(false);
#endif
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
#if USE_HDRP
            if (EnterCollider != null && !IsOpenProbe)
            {
                SetProbeEnabled(true);
                IsOpenProbe = true;
            }

            if (EnterCollider == null && IsOpenProbe)
            {
                SetProbeEnabled(false);
                IsOpenProbe = false;
            }
#endif

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                EnterCollider = other;
                EventHelper.Fire(this, EnterStaticSceneEventArgs.Create(SceneData.AssetPathId));
            }

        }


        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                EnterCollider = null;
                EventHelper.Fire(this, ExitStaticSceneEventArgs.Create(SceneData.AssetPathId));
            }
        }




    }
}