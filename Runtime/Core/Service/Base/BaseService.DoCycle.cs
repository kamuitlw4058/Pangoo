using Pangoo.Common;
using UnityEngine.EventSystems;

namespace Pangoo.Core.Services
{
    public abstract partial class BaseService : JsonSerializer
    {


        protected virtual void DoAwake()
        {
        }

        protected virtual void DoStart()
        {
        }

        protected virtual void DoUpdate()
        {

        }


        protected virtual void DoDestroy()
        {

        }

        protected virtual void DoEnable()
        {

        }

        protected virtual void DoDisable()
        {

        }

        protected virtual void DoFixedUpdate()
        {

        }

        protected virtual void DoDrawGizmos()
        {

        }

        protected virtual void DoPointerEnter(PointerEventData pointerEventData)
        {

        }

        protected virtual void DoPointerExit(PointerEventData pointerEventData)
        {

        }

        protected virtual void DoPointerClick(PointerEventData pointerEventData)
        {

        }
    }
}