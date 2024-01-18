using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.VisualScripting;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine;

public class MouseImmersed : BaseImmersed
{
    public bool UseMouseLeftEvent;
    
    [ShowIf("UseMouseLeftEvent")]
    [ValueDropdown("TriggerUuidDropDown")]
    public string MouseLeftDownTriggerUUid;
    [ShowIf("UseMouseLeftEvent")]
    [ValueDropdown("TriggerUuidDropDown")]
    public string MouseLeftPressedTriggerUuid;
    [ShowIf("UseMouseLeftEvent")]
    [ValueDropdown("TriggerUuidDropDown")]
    public string MouseLeftUpTriggerUuid;

    public bool UseMouseRightEvent;
    
    [ShowIf("UseMouseRightEvent")]
    [ValueDropdown("TriggerUuidDropDown")]
    public string MouseRightDownTriggerUUid;
    [ShowIf("UseMouseRightEvent")]
    [ValueDropdown("TriggerUuidDropDown")]
    public string MouseRightPressedTriggerUuid;
    [ShowIf("UseMouseRightEvent")]
    [ValueDropdown("TriggerUuidDropDown")]
    public string MouseRightUpTriggerUuid;

    protected Ray ray;
    protected RaycastHit hit;
    public LayerMask layerMask;
    public QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.Ignore;

    protected bool isOnlyTarget;
    protected Collider TargetCollider;
    [ReadOnly]
    protected Collider HitCollider;
    [ReadOnly]
    public bool isDragTarget;
    
    public override void OnUpdate()
    {
        if (IsRunning)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
            if (Physics.Raycast(ray, out hit, 100, layerMask, queryTriggerInteraction))
            {
                HitCollider = hit.collider;
            }

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
        if (MouseLeftDownTriggerUUid!=null)
        {
            Debug.Log("鼠标左键按下");
            Debug.Log("dy:"+dynamicObject);
            dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseLeftDown);
        }
    }

    public virtual void OnMouseLeftPressedEvent()
    {
        if (MouseLeftPressedTriggerUuid!=null)
        {
            dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseLeft);
        }
    }

    public virtual void OnMouseLeftUpEvent()
    {
        if (MouseLeftUpTriggerUuid!=null)
        {
            dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseLeftUp);
        }
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
        if (MouseLeftDownTriggerUUid!=null)
        {
            dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseRightDown,MouseRightDownTriggerUUid);
        }
    }

    public virtual void OnMouseRightPressedEvent()
    {
        if (MouseLeftPressedTriggerUuid!=null)
        {
            dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseRight,MouseRightPressedTriggerUuid);
        }
    }

    public virtual void OnMouseRightUpEvent()
    {
        if (MouseLeftUpTriggerUuid!=null)
        {
            dynamicObject?.TriggerInovke(TriggerTypeEnum.OnMouseRightUp,MouseRightUpTriggerUuid);
        }
    }

    #endregion

    [Button("清空UUID赋值")]
    public void ClearAllUuid()
    {
        MouseLeftDownTriggerUUid = String.Empty;
        MouseLeftPressedTriggerUuid = String.Empty;
        MouseLeftUpTriggerUuid = String.Empty;

        MouseRightDownTriggerUUid = String.Empty;
        MouseRightPressedTriggerUuid=String.Empty;
        MouseRightUpTriggerUuid=String.Empty;
    }

    
    
#if UNITY_EDITOR
    IEnumerable TriggerUuidDropDown()
    {
        return TriggerEventOverview.GetUuidDropdown();
    }
#endif
}
