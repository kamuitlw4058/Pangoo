using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo.Core.VisualScripting
{
    public enum TriggerTypeEnum
    {
        Unknown,
        BeforeInteract,
        OnInteract,

        EndInteract,
        OnStart,
        OnTriggerEnter3D,
        OnTriggerExit3D,

        OnPlayTimeline,
        OnMouseLeftDown,

        OnExecute,
        OnExternalInstruction,

        OnPointerEnter,
        OnPointerExit,
        OnPointerClick,

        OnPointerDown,
        OnPointerUp,

        OnBeginDrag,
        OnDrag,

        OnEndDrag,
        OnDrop,

        OnSelect,
        OnDeselect,

        OnScroll,
        OnUpdateSelect,
        OnMove,
        OnSubmit,
        OnCanel,

        OnExit,

        OnButtonE, // 尝试废弃

        OnPointerClickLeft,
        OnPointerClickRight,

        OnTimelineSignal,

        OnUpdate,
        OnInteractionSuccess,
        OnInteractionFailed,
        OnPreview,
        OnMouseLeftUp, // 尝试废弃
        OnMouseLeft, // 尝试废弃
        OnMouseRight, // 尝试废弃
        OnMouseRightUp, // 尝试废弃
        OnMouseRightDown, // 尝试废弃
        OnMouseImmersedLeftDown,
        OnMouseImmersedLeft,
        OnMouseImmersedLeftUp,
        OnMouseImmersedRightDown,
        OnMouseImmersedRight,
        OnMouseImmersedRightUp,
        OnButtonF, // 尝试废弃

        OnExtraTriggerEnter3D,
        OnExtraTriggerExit3D,

        OnExtraPointerEnter,
        OnExtraPointerExit,

        OnExtraPointerClick,

        OnExtraBeginDrag,

        OnExtraDrag,
        OnExtraEndDrag,

        OnTriggerStayTimeOut,
        OnTriggerStayTimeOutExit,

        OnDialogueSignal,


    }
}


