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

        OnButtonE,

        OnPointerClickLeft,
        OnPointerClickRight,

        OnTimelineSignal,

        OnUpdate,
        OnInteractionSuccess,
        OnInteractionFailed,
        OnPreview,
        OnMouseLeftUp,
        OnMouseLeft,
        OnMouseRight,
        OnMouseRightUp,
        OnMouseRightDown,
        OnMouseImmersedLeftDown,
        OnMouseImmersedLeft,
        OnMouseImmersedLeftUp,
        OnMouseImmersedRightDown,
        OnMouseImmersedRight,
        OnMouseImmersedRightUp,
        OnButtonF,

        OnExtraTriggerEnter3D,
        OnExtraTriggerExit3D,
    }
}


