using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.VisualScripting;
using Plugins.Pangoo.Plugins.PangooCommon.Helper;
using Sirenix.OdinInspector;
using UnityEngine;

public class MouseImmersed : BaseImmersed
{
    public bool UseMouseLeftEvent=true;

    public bool UseMouseRightEvent;
    
    [ReadOnly]
    public bool isDragTarget;

    protected bool isOnlyTarget;
    
    public RayHelper rayHelper = new RayHelper();
    
    public override void OnUpdate()
    {
        if (IsRunning)
        {
            if (UseMouseLeftEvent)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnMouseLeftDownConditionAction();
                }

                if (Input.GetMouseButton(0))
                {
                    OnMouseLeftPressedConditionAction();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    OnMouseLeftUpConditionAction();
                }
            }

            if (UseMouseRightEvent)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    OnMouseRightDownEvent();
                }
                if (Input.GetMouseButton(1))
                {
                    OnMouseRightPressedEvent();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    OnMouseRightUpEvent();
                }
            }
        }
        
    }

    public void DivergentRay()
    {
        rayHelper.ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayHelper.ray, out rayHelper.hit, rayHelper.rayLength, rayHelper.layerMask,
                rayHelper.queryTriggerInteraction))
        {
            if (rayHelper.hit.collider != null)
            {
                rayHelper.HitCollider = rayHelper.hit.collider;
            }
            else
            {
                rayHelper.HitCollider = null;
            }
        }
    }
    
    #region MouseLeft

    public virtual void OnMouseLeftDownConditionAction()
    {
        DivergentRay();

        if (isOnlyTarget && rayHelper.HitCollider == rayHelper.TargetCollider)
        {
            OnMouseLeftDownEvent();
        }
    }

    

    public virtual void OnMouseLeftPressedConditionAction()
    {
        if (isOnlyTarget)
        {
            OnMouseLeftPressedEvent();
        }
    }

    public virtual void OnMouseLeftUpConditionAction()
    {
        OnMouseLeftUpEvent();
    }

    public virtual void OnMouseLeftDownEvent()
    {
        Debug.Log("鼠标左键按下");
        Debug.Log("dy:"+dynamicObject);
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseImmersedLeftDown);
    }

    public virtual void OnMouseLeftPressedEvent()
    {
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseImmersedLeft);
    }

    public virtual void OnMouseLeftUpEvent()
    {
        rayHelper.HitCollider = null;
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseImmersedLeftUp);
    }

    #endregion

    #region MouseRight

    public virtual void OnMouseRightDownConditionAction()
    {
        OnMouseRightDownEvent();
    }
    
    public virtual void OnMouseRightPressedConditionAction()
    {
        OnMouseRightPressedEvent();
    }

    public virtual void OnMouseRightUpConditionAction()
    {
        OnMouseRightUpEvent();
    }

    public virtual void OnMouseRightDownEvent()
    {
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseImmersedRightDown);
    }

    public virtual void OnMouseRightPressedEvent()
    {
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseImmersedRight);
    }

    public virtual void OnMouseRightUpEvent()
    {
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseImmersedRightUp);
    }

    #endregion
}
