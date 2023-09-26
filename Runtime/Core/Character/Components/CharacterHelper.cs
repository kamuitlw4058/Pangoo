#if UNITY_EDITOR

using UnityEngine;
using Sirenix.OdinInspector;

using System;


namespace Pangoo.Core.Character
{
    // [SelectionBase]
    // [DisallowMultipleComponent]
    // [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_DEFAULT_EARLIER)]


    // [ExecuteInEditMode]
    public class CharacterHelper : MonoBehaviour
    {

        [SerializeField]
        [HideInEditorMode]
        CharacterService characterContainer;

        [SerializeField][HideInPlayMode] bool m_IsPlayer;
        [ShowInInspector][HideInEditorMode] bool IsInited;

        [SerializeField][HideInPlayMode] bool m_CameraOnly;

        [SerializeField][HideInPlayMode] MotionInfo m_MotionInfo;


        public void Init()
        {
            if (IsInited)
            {
                return;
            }
            Debug.Log($"On Init:{characterContainer}");
            characterContainer = new CharacterService(gameObject, m_CameraOnly);
            characterContainer.SetIsPlayer(m_IsPlayer);
            characterContainer.SetMotionInfo(m_MotionInfo);
            characterContainer.Awake();

            IsInited = true;
        }

        private void Awake()
        {
            Debug.Log($"On Awake:{characterContainer}");
            Init();
        }

        private void OnEnable()
        {
            Debug.Log($"On Enable");
            Init();
            characterContainer.Enable();
        }

        private void Start()
        {
            characterContainer.Start();
        }

        private void Update()
        {
            characterContainer?.Update();
        }


        private void OnDisable()
        {
            characterContainer.Disable();
        }

        private void OnDestroy()
        {
            characterContainer.Destroy();

        }

        [Button("设置移动")]
        public void SetMotionDirection()
        {
            characterContainer?.SetVariable<Vector3>("MoveDirection", new Vector3(0, 1, 0));
        }

        void OnDrawGizmos()
        {
            characterContainer?.DrawGizmos();
        }

    }
}
#endif