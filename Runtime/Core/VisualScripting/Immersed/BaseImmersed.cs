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
        public bool ShowDynamicObject;
        [ShowInInspector]
        [ShowIf("@this.ShowDynamicObject")]
        public DynamicObject dynamicObject = null;

        [ShowInInspector]
        [ShowIf("@this.ShowDynamicObject")]
        public EntityDynamicObject entityDynamicObject = null;

        [ShowInInspector]
        public bool IsRunning { get; set; }

        protected virtual void Start()
        {
            entityDynamicObject = GetComponent<EntityDynamicObject>();
            dynamicObject = entityDynamicObject?.DynamicObj;
        }


        public virtual void OnEnter()
        {
            dynamicObject = GetComponent<EntityDynamicObject>()?.DynamicObj;
            if (!GetComponent<EntityDynamicObject>())
            {
                Debug.LogError("BaseImmersed接口没有获取到EntityDynamicObject组件");
            }
            IsRunning = true;
        }



        public virtual void OnExit()
        {
            IsRunning = false;
            Debug.Log("OnExit");
        }

        public virtual void OnUpdate()
        {
            if (dynamicObject == null)
            {
                entityDynamicObject = GetComponent<EntityDynamicObject>();
                dynamicObject = entityDynamicObject?.DynamicObj;
            }

        }
    }
}