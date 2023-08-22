//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Entity;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityGameFramework.Runtime;
using UnityEngine.Rendering;
using UnityEditor;

namespace Pangoo
{


    /// <summary>
    /// 场景组件。
    /// </summary>
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public sealed class VolumeComponent : GameFrameworkComponent
    {
        private const int DefaultPriority = 0;
        private const string  VolumeEntityGroup = "Volume";

        private IVolumeManager m_VolumeManager = null;
        private EventComponent m_EventComponent = null;
        private EntityComponent m_EntityComponent = null;

        [SerializeField]
        List<AssetPathTable.AssetPathRow> Prefabs;


        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

           m_VolumeManager = GameFrameworkEntry.GetModule<IVolumeManager>();
            if (m_VolumeManager == null)
            {
                Log.Fatal("volume manager is invalid.");
                return;
            }
        }

        private void Start()
        {
            BaseComponent baseComponent = GameEntry.GetComponent<BaseComponent>();
            if (baseComponent == null)
            {
                Log.Fatal("Base component is invalid.");
                return;
            }

            m_EventComponent = GameEntry.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }

            m_EntityComponent =  GameEntry.GetComponent<EntityComponent>();
            if(m_EntityComponent == null){
                Log.Fatal("Entity component is invalid.");
                return;
            }

            m_VolumeManager.SetEntityManager(GameFrameworkEntry.GetModule<IEntityManager>());
            m_VolumeManager.Init();

            IEntityGroup entityGroup = m_EntityComponent.GetEntityGroup(VolumeEntityGroup);
            if(entityGroup == null){
                m_EntityComponent.AddEntityGroup(VolumeEntityGroup,60,1000,float.MaxValue,DefaultPriority);
            }


        }

#if UNITY_EDITOR
        [Button("Apply")]
        public void ApplyVolume([ValueDropdown("GetVolumeRow")]int id){
            if(id == 0){
                return;
            }

            
        }

        [Button("test")]
        public void Test(){
            
        }        

        public IEnumerable GetVolumeRow(){
            return GameSupportEditorUtility.GetVolumeRow();
        }

#endif
    }
}
