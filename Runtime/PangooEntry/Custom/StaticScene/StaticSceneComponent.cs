
using System;
using System.Collections.Generic;
using System.Linq;

using GameFramework;
using GameFramework.ObjectPool;
using GameFramework.Resource;
using GameFramework.Scene;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;


namespace Pangoo
{
    /// <summary>
    /// FGUI组件。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed  class StaticSceneComponent : GameFrameworkComponent
    {
        private IStaticSceneManager m_StaticSceneManager = null;

        [ShowInInspector]
        public List<int> LoadingScene {
            get{
                if(m_StaticSceneManager != null){
                    return m_StaticSceneManager.LoadingScene;
                }

                return null;
            }
        }


        [ShowInInspector]
         public Dictionary<int,EntityStaticScene> LoadedStaticSceneDict{get{
                if(m_StaticSceneManager != null){
                    return m_StaticSceneManager.LoadedStaticSceneDict;
                }

                return null;
         }}

        protected override void Awake()
        {
            base.Awake();

            m_StaticSceneManager = GameFrameworkEntry.GetModule<IStaticSceneManager>();
            if (m_StaticSceneManager == null)
            {
                Log.Fatal("Entity manager is invalid.");
                return;
            }

            // m_StaticSceneManager.ShowEntitySuccess += OnShowEntitySuccess;
            // m_StaticSceneManager.ShowEntityFailure += OnShowEntityFailure;

            // if (m_EnableShowEntityUpdateEvent)
            // {
            //     m_StaticSceneManager.ShowEntityUpdate += OnShowEntityUpdate;
            // }

            // if (m_EnableShowEntityDependencyAssetEvent)
            // {
            //     m_StaticSceneManager.ShowEntityDependencyAsset += OnShowEntityDependencyAsset;
            // }

            // m_StaticSceneManager.HideEntityComplete += OnHideEntityComplete;
        }

        public void ShowStaticScene(int id){
            m_StaticSceneManager.ShowStaticScene(id);
        }
    }
}