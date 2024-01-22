using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
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

    protected Ray ray;
    protected RaycastHit hit;
    protected float RayLength=100f;
    public LayerMask layerMask;


    private QueryTriggerInteractionType m_QueryTriggerInteractionType;
    [LabelText("设置射线检测条件")]
    [ShowInInspector]
    public QueryTriggerInteractionType QueryTriggerInteractionType
    {
        get => m_QueryTriggerInteractionType;
        set
        {
            switch (value)
            {
                case QueryTriggerInteractionType.Ignore:
                    queryTriggerInteraction = QueryTriggerInteraction.Ignore;
                    break;
                case QueryTriggerInteractionType.Collider:
                    queryTriggerInteraction = QueryTriggerInteraction.Collide;
                    break;
                case QueryTriggerInteractionType.UseGlobal:
                    queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
                    break;
            }

            m_QueryTriggerInteractionType=value;
        }
    }

    [ShowInInspector]
    protected QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

    protected bool isOnlyTarget;
    [ReadOnly]
    public Collider TargetCollider;
    [ReadOnly]
    public Collider HitCollider;
    [ReadOnly]
    public bool isDragTarget;
    
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

    #region MouseLeft

    public virtual void OnMouseLeftDownConditionAction()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, RayLength, layerMask, queryTriggerInteraction))
        {
            if (hit.collider!=null)
            {
                HitCollider = hit.collider;
            }
            else
            {
                HitCollider = null;
            }
        }
        
        if (isOnlyTarget && HitCollider == TargetCollider)
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
        HitCollider = null;
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
