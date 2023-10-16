using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Resource;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;
using Pangoo.Service;
using Sirenix.OdinInspector;

namespace Pangoo
{
    public class ServiceComponent : GameFrameworkComponent
    {
        [ShowInInspector]
        [TableList]
        public List<IBaseServiceContainer> ServiceContainers = new List<IBaseServiceContainer>();


    }
}