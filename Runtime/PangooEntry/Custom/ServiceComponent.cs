using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Resource;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;

namespace Pangoo
{
    public class ServiceComponent : GameFrameworkComponent
    {

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


    }
}