using UnityEngine;
using Pangoo.Service;
using Sirenix.OdinInspector;
using Pangoo.Core.Common;

namespace Pangoo.Core.Service
{

    public abstract class MonoSubService<TMaster> : NestedBaseService where TMaster : MonoMasterService
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

        public void InitParent()
        {
            if (Parent != null)
            {
                if (Parent is TMaster)
                {
                    Master = Parent as TMaster;
                }

                if (Parent is MonoSubService<TMaster>)
                {
                    Master = (Parent as MonoSubService<TMaster>).Master;
                }
            }
        }

        public static MonoSubService<TMaster> Create<TSub>(TMaster master) where TSub : MonoSubService<TMaster>, new()
        {
            var subService = new TSub();
            subService.Parent = master;
            subService.InitParent();
            return subService;
        }

        public static MonoSubService<TMaster> Create<TSub>(MonoSubService<TMaster> sub) where TSub : MonoSubService<TMaster>, new()
        {
            var subService = new TSub();
            subService.Parent = sub.Parent;
            subService.InitParent();
            return subService;
        }

    }

}

