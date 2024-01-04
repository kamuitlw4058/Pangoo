using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IImmersed
{
    public bool IsRunning { get;}
    public bool IsInteractionEnd { get; }
    public void OnEnter();
    public void OnExit();

    public void OnUpdate();
}
