using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pangoo
{
    public class Test : MonoBehaviour
    {
        public DataContainerService dataContainerService;
        public float obj;
        public int testObj;
        public void Awake()
        {
            dataContainerService = new DataContainerService();
        }

        public void Update()
        {
        }
    }
}

