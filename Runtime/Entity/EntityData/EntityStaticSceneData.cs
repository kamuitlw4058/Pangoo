
using System;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;
using Pangoo.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;

namespace Pangoo
{
    [Serializable]
    public class EntityStaticSceneData : EntityData
    {
        StaticSceneInfoRow m_sceneInfo;
        // public EntityInfo Info;
        public StaticSceneInfoRow sceneInfo
        {
            get
            {
                return m_sceneInfo;
            }
            set
            {
                if (m_sceneInfo != value)
                {
                    m_sceneInfo = value;
                    m_LoadSceneUuids = null;
                }
            }
        }

        public EntityInfo EntityInfo;

        public string AssetPathUuid
        {
            get
            {
                return EntityInfo.AssetPathUuid;
            }
        }

        public string Uuid
        {
            get
            {
                return sceneInfo.Uuid;
            }
        }
        public string UuidShort
        {
            get
            {
                return sceneInfo.Uuid.ToShortUuid();
            }
        }

        public string Name
        {
            get
            {
                return sceneInfo.Name;
            }
        }

        public override Vector3 Position
        {
            get
            {
                return sceneInfo.m_StaticSceneRow.Position;
            }
        }

        public override Quaternion Rotation
        {
            get
            {
                return Quaternion.Euler(sceneInfo.m_StaticSceneRow.Rotation);
            }
        }

        [ShowInInspector]
        public bool Hide { get; set; }


        string[] m_LoadSceneUuids;

        public string[] LoadSceneUuids
        {
            get
            {
                if (m_LoadSceneUuids == null)
                {
                    m_LoadSceneUuids = sceneInfo.SceneRow.LoadSceneUuids.ToSplitArr<string>();
                }
                return m_LoadSceneUuids;
            }
        }

        public void ClearModel()
        {
            m_Models = null;
        }

        Transform[] m_Models;
        public Transform[] Models
        {
            get
            {
                if (m_Models == null)
                {
                    var modelPathList = sceneInfo.SceneRow.ModelList.ToSplitArr<string>();
                    if (modelPathList != null && modelPathList.Length > 0)
                    {
                        List<Transform> ModelList = new List<Transform>();
                        foreach (var modelPath in modelPathList)
                        {
                            var trans = EntityScene.CachedTransform.Find(modelPath);
                            if (trans != null)
                            {
                                ModelList.Add(trans);
                            }
                        }
                        m_Models = ModelList.ToArray();
                    }
                    else
                    {
                        var trans = EntityScene.CachedTransform.Find(ConstString.DefaultModel);
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


        public bool ShowModels
        {
            get
            {
                switch (sceneInfo.SceneRow.ShowType.ToEnum<SceneShowType>())
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

                if (sceneInfo.SceneRow.ShowOnNoPlayerEnter && Service.EnterAssetCount == 0)
                {
                    return true;
                }

                if (Service.CheckEnterScenes(LoadSceneUuids, sceneInfo.SceneRow.Uuid))
                {
                    return true;
                }


                return false;
            }
        }

        public EntityStaticScene EntityScene { get; set; }


        public StaticSceneService Service;
        public EntityStaticSceneData() : base()
        {
        }

        public static EntityStaticSceneData Create(StaticSceneInfoRow staticSceneInfo, EntityInfo Info, StaticSceneService service, object userData = null)
        {
            EntityStaticSceneData entityData = ReferencePool.Acquire<EntityStaticSceneData>();
            entityData.EntityInfo = Info;
            entityData.Service = service;
            entityData.UserData = userData;
            entityData.sceneInfo = staticSceneInfo;
            return entityData;
        }

        public override void Clear()
        {
            base.Clear();
            Service = null;
            EntityInfo = null;
        }
    }
}
