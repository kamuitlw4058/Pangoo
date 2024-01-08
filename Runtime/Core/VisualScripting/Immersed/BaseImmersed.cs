using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo;
using Pangoo.Core.Services;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseImmersed : MonoBehaviour,IImmersed
{
    [ReadOnly]
    public DynamicObject dynamicObject = null;

    public bool IsRunning { get; set; }

    public virtual void OnEnter()
    {
        dynamicObject=GetComponent<EntityDynamicObject>().DynamicObj;
    }

    public virtual void OnExit()
    {
        Debug.Log("OnExit");
    }

    public virtual void OnUpdate()
    {
        
    }
}
