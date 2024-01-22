using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Common;
using Pangoo.Core.VisualScripting;
using Sirenix.OdinInspector;
using UnityEngine;

public enum QueryTriggerInteractionType
{
    [LabelText("只检测Collider")]
    Ignore,
    [LabelText("检测Collider和Trigger")]
    Collider,
    [LabelText("使用Unity项目设置")]
    UseGlobal
}

public class MouseImmersed : BaseImmersed
{
    public bool UseMouseLeftEvent=true;

    public bool UseMouseRightEvent;
    
    [ReadOnly]
    public bool isDragTarget;

    protected bool isOnlyTarget;
    
    public RayUtility rayUtility = new RayUtility();
    
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
        rayUtility.ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayUtility.ray, out rayUtility.hit, rayUtility.rayLength, rayUtility.layerMask,
                rayUtility.queryTriggerInteraction))
        {
            if (rayUtility.hit.collider != null)
            {
                rayUtility.HitCollider = rayUtility.hit.collider;
            }
            else
            {
                rayUtility.HitCollider = null;
            }
        }
    }
    
    #region MouseLeft

    public virtual void OnMouseLeftDownConditionAction()
    {
        DivergentRay();

        if (isOnlyTarget && rayUtility.HitCollider == rayUtility.TargetCollider)
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
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseLeftDown);
    }

    public virtual void OnMouseLeftPressedEvent()
    {
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseLeft);
    }

    public virtual void OnMouseLeftUpEvent()
    {
        rayUtility.HitCollider = null;
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseLeftUp);
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
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseRightDown);
    }

    public virtual void OnMouseRightPressedEvent()
    {
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseRight);
    }

    public virtual void OnMouseRightUpEvent()
    {
        dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseRightUp);
    }

    #endregion
}
