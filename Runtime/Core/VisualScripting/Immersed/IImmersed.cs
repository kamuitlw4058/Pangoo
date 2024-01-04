using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IImmersed
{
    public bool IsRunning { get; set; }
    public bool IsInteractionEnd { get; set; }
    
    public bool CanMidwayExit { get; set; }
    public void OnEnter();
    public void OnExit();

    public void OnUpdate();
}
