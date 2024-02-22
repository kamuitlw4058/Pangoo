using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo;
using Pangoo.Core.Services;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{



    public class BaseImmersed : MonoBehaviour, IImmersed
    {
        [HideInInspector]
        public DynamicObject dynamicObject = null;

        [ShowInInspector]
        public bool IsRunning { get; set; }

        public virtual void OnEnter()
        {
            dynamicObject = GetComponent<EntityDynamicObject>()?.DynamicObj;
            IsRunning = true;
        }

        public virtual void OnExit()
        {
            IsRunning = false;
            Debug.Log("OnExit");
        }

        public virtual void OnUpdate()
        {

        }
    }
}