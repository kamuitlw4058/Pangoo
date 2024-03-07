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
                probe.settingsRaw.cameraSettings.customRenderingSettings = true;
                probe.frameSettingsOverrideMask.mask[(int)FrameSettingsField.ShadowMaps] = true;
                probe.frameSettings.SetEnabled(FrameSettingsField.ShadowMaps, false);
                // probe.enabled = val;
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
                LogError("Entity data is invalid.");
                return;
            }
            SceneData.EntityScene = this;
            SceneData.Hide = SceneData.sceneInfo.SceneRow.HideDefault;

            Name = Utility.Text.Format("{0}[{1}]", SceneData.Name, SceneData.UuidShort);
            // UpdateDefaultTransform();
#if USE_HDRP
            PlanarProbes = GetComponentsInChildren<PlanarReflectionProbe>();
            SetProbeEnabled(false);
#endif
        }

        public void SetModelsActive(bool val)
        {
            foreach (var model in SceneData.Models)
            {
                model.gameObject.SetActive(val);
            }
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
            if (SceneData.ShowModels)
            {
                SetModelsActive(true);
            }
            else
            {
                SetModelsActive(false);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                EnterCollider = other;
                EventHelper.FireNow(this, EnterStaticSceneEventArgs.Create(SceneData.AssetPathUuid));
            }

        }


        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                EnterCollider = null;
                EventHelper.FireNow(this, ExitStaticSceneEventArgs.Create(SceneData.AssetPathUuid));
            }
        }




    }
}