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
        OnMouseLeft,

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
    }
}


