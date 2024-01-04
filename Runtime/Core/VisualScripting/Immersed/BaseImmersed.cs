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
    public bool IsInteractionEnd { get; set; }

    public virtual void Start()
    {
        dynamicObject=GetComponent<EntityDynamicObject>().DynamicObj;
    }

    public virtual void OnEnter()
    {
        Debug.Log("OnEnter");
    }

    public virtual void OnExit()
    {
        Debug.Log("OnExit");
    }

    public virtual void OnUpdate()
    {
        Debug.Log("OnUpdate");
    }
}
