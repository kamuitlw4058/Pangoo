using System.Collections;
using System.Collections.Generic;
using Pangoo;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseImmersed : MonoBehaviour,IImmersed
{
    [ReadOnly]
    public MainService mainService = null;

    public bool IsRunning { get; set; }

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
