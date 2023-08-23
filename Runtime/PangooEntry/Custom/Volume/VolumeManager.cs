using GameFramework;
using GameFramework.Entity;
using GameFramework.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo{

    internal sealed class VolumeManager : GameFrameworkModule,IVolumeManager
    {

        private IEntityManager m_EntityManager;
        private const string  VolumeEntityGroup = "Volume";

        public VolumeManager(){
        }




        public override void Shutdown()
        {
            // throw new System.NotImplementedException();
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            // throw new System.NotImplementedException();
        }


        /// <summary>
        /// 设置实体管理。
        /// </summary>
        /// <param name="entityManager">实体管理器</param>
        public void SetEntityManager(IEntityManager entityManager)
        {
            if (entityManager == null)
            {
                throw new GameFrameworkException("Resource manager is invalid.");
            }

            m_EntityManager = entityManager;

        }

        public void Init(){

            if (m_EntityManager == null)
            {
                throw new GameFrameworkException("You must set entity manager first.");
            }

            IEntityGroup entityGroup = m_EntityManager.GetEntityGroup(VolumeEntityGroup);
            if(entityGroup == null){
                // m_EntityManager.AddEntityGroup(VolumeEntityGroup,60,100,-1,0,)
            }

         
        }
    }
}
