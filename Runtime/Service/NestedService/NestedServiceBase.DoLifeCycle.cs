using System;
using GameFramework;
using Sirenix.OdinInspector;


namespace Pangoo.Service
{
    public abstract partial class NestedServiceBase
    {

        public virtual void DoAwake(INestedService parent)
        {
        }

        public virtual void DoStart()
        {
        }

        public virtual void DoUpdate()
        {

        }


        public virtual void DoDestroy()
        {

        }

        public virtual void DoEnable()
        {

        }

        public virtual void DoDisable()
        {

        }

        public virtual void DoFixedUpdate()
        {

        }

        public virtual void DoDrawGizmos()
        {

        }
    }
}