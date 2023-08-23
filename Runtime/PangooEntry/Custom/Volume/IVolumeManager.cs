using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Resource;
using GameFramework.Entity;

namespace Pangoo{

    public interface IVolumeManager
    {
        //  void SetResourceManager(IResourceManager resourceManager);
        void SetEntityManager(IEntityManager entityManager);

        void Init();
    }
}
