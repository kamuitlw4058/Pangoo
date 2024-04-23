using Sirenix.OdinInspector;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;
using System;

namespace Pangoo
{
    public class ServiceComponent : GameFrameworkComponent
    {
        [NonSerialized]
        MainService m_mainService = null;

        [ShowInInspector]
        public MainService mainService
        {
            get
            {
                return m_mainService;
            }
            set
            {
                m_mainService = value;
            }
        }

        [NonSerialized]
        SideEffectMainService m_SideEffectMainService = null;
        [ShowInInspector]
        public SideEffectMainService SideEffectMainSrv
        {
            get
            {
                return m_SideEffectMainService;
            }
            set
            {
                m_SideEffectMainService = value;
            }
        }

    }
}