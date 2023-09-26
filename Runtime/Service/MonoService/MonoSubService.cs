using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;

namespace Pangoo.Core.Service
{

    public abstract class MonoSubService<TMaster> : NestedServiceBase where TMaster : MonoMasterService
    {

        public TMaster Master { get; protected set; }


        public override float Time => Master.Time;

        public override float DeltaTime => Master.DeltaTime;

        public Transform Transform
        {
            get
            {
                if (Master != null)
                {
                    return Master.CachedTransfrom;
                }
                return null;
            }
        }

        public void InitParent(INestedService parent)
        {
            if (parent != null)
            {
                if (parent is TMaster)
                {
                    Master = parent as TMaster;
                }

                if (parent is MonoSubService<TMaster>)
                {
                    Master = (parent as MonoSubService<TMaster>).Master;
                }


            }
        }

        public static MonoSubService<TMaster> Create<TSub>(TMaster master) where TSub : MonoSubService<TMaster>, new()
        {
            var subService = new TSub();
            subService.InitParent(master);
            return subService;
        }

        public static MonoSubService<TMaster> Create<TSub>(MonoSubService<TMaster> sub) where TSub : MonoSubService<TMaster>, new()
        {
            var subService = new TSub();
            subService.InitParent(sub);
            return subService;
        }

    }

}

