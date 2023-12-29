using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseImmersed : MonoBehaviour,IImmersed
{
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
