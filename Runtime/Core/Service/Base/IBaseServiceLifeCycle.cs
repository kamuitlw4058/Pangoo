
using UnityEngine.EventSystems;


namespace Pangoo.Core.Services
{
    public interface IBaseServiceLifeCycle : IBaseServiceTime
    {
        int Priority { get; }

        IBaseServiceContainer Parent { get; set; }
        void Awake();
        void Start();

        void Enable();

        void FixedUpdate();
        void Update();

        void Disable();

        void Destroy();

        void DrawGizmos();

        void PointerEnter(PointerEventData pointerEventData);

        void PointerExit(PointerEventData pointerEventData);
    }
}