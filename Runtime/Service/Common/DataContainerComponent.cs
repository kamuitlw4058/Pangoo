using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo
{
    public class DataContainerComponent : Singleton<DataContainerComponent>
    {
        [ShowInInspector]
        public DataContainerService dataContainerService= new DataContainerService();
    }
}

