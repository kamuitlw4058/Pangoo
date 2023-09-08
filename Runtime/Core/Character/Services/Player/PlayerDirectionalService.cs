using System;
using UnityEngine;
using Pangoo.Service;
using Pangoo.Core.Common;

namespace Pangoo.Core.Character
{

    [Serializable]
    public class PlayerDirectionalService : PlayerService
    {
        [SerializeField] private InputValueVector2Base m_InputMove;

        protected PlayerDirectionalService() : base()
        {
            m_InputMove = InputValueVector2MotionPrimary.Create();
        }

        public override void DoAwake(IServiceContainer services)
        {
            base.DoAwake(services);
            m_InputMove.OnAwake();
        }

        public override void DoDestroy()
        {
            m_InputMove.OnDestroy();
            base.DoDestroy();
        }


        public override void DoEnable()
        {
            base.DoEnable();
            // this.m_InputMove

        }

        public override void DoDisable()
        {
            // base.OnDisable();
            // this.m_InputMove.Disable();

            // this.Character.Motion?.MoveToDirection(Vector3.zero, Space.World, 0);
        }



    }

}

