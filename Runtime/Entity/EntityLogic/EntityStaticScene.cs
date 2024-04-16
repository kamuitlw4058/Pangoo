using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using Sirenix.OdinInspector;
using GameFramework;
using System;
using Pangoo.Common;
using Pangoo.Core.VisualScripting;

#if USE_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace Pangoo
{
    public class EntityStaticScene : EntityBase
    {

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



        [ShowInInspector]
        public bool Hide { get; set; }

        Transform[] m_Models;
        public Transform[] Models
        {
            get
            {
                if (SceneData == null) return null;

                if (m_Models == null)
                {
                    var modelPathList = SceneData.sceneInfo.SceneRow.ModelList.ToSplitArr<string>();
                    if (modelPathList != null && modelPathList.Length > 0)
                    {
                        List<Transform> ModelList = new List<Transform>();
                        foreach (var modelPath in modelPathList)
                        {
                            var trans = CachedTransform.Find(modelPath);
                            if (trans != null)
                            {
                                ModelList.Add(trans);
                            }
                        }
                        m_Models = ModelList.ToArray();
                    }
                    else
                    {
                        var trans = CachedTransform.Find(ConstString.DefaultModel);
                        if (trans == null)
                        {
                            m_Models = new Transform[0];
                        }
                        else
                        {
                            m_Models = new Transform[1] { trans };
                        }

                    }
                }
                return m_Models;
            }
        }

        string[] m_LoadSceneUuids;

        public string[] LoadSceneUuids
        {
            get
            {
                if (SceneData == null) return null;

                if (m_LoadSceneUuids == null)
                {
                    m_LoadSceneUuids = SceneData.sceneInfo.SceneRow.LoadSceneUuids.ToSplitArr<string>();
                }
                return m_LoadSceneUuids;
            }
        }


        public bool ShowModels
        {
            get
            {
                if (SceneData == null) return false;

                switch (SceneData.sceneInfo.SceneRow.ShowType.ToEnum<SceneShowType>())
                {
                    case SceneShowType.Always:
                        return true;
                    case SceneShowType.Auto:
                        if (Hide)
                        {
                            return false;
                        }
                        break;
                    case SceneShowType.ManualAlways:
                        if (Hide)
                        {
                            return false;
                        }
                        return true;
                }


                if (LoadSceneUuids == null || (LoadSceneUuids != null && LoadSceneUuids.Length == 0))
                {
                    return true;
                }

                if (SceneData.sceneInfo.SceneRow.ShowOnNoPlayerEnter && SceneData.Service.EnterAssetCount == 0)
                {
                    return true;
                }

                if (SceneData.Service.CheckEnterScenes(LoadSceneUuids, SceneData.sceneInfo.SceneRow.Uuid))
                {
                    return true;
                }


                return false;
            }
        }
        public void ResetStatcSceneData()
        {
            m_Models = null;
            m_LoadSceneUuids = null;

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

            Hide = SceneData.sceneInfo.SceneRow.HideDefault;
            ResetStatcSceneData();

            Name = Utility.Text.Format("{0}[{1}]", SceneData.Name, SceneData.UuidShort);
            // UpdateDefaultTransform();
#if USE_HDRP
            PlanarProbes = GetComponentsInChildren<PlanarReflectionProbe>();
            SetProbeEnabled(false);
#endif
        }

        public void SetModelsActive(bool val)
        {
            var models = Models;
            if (models == null) return;


            foreach (var model in Models)
            {
                var go = model?.gameObject;
                if (go != null)
                {
                    go.SetActive(val);
                }
            }
        }

        public override void OnUpdateEntityData(EntityData entityData)
        {
            if (entityData == null) return;
            var data = entityData as EntityStaticSceneData;
            if (data == null) return;

            SceneData = data;
            ResetStatcSceneData();
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
            if (ShowModels)
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


        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            if (EnterCollider != null)
            {
                EventHelper.FireNow(this, ExitStaticSceneEventArgs.Create(SceneData.AssetPathUuid));
            }
        }

    }
}