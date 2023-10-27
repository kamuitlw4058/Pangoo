
using UnityEngine;
using Sirenix.OdinInspector;

using System;


namespace Pangoo.Core.Characters
{
    // [SelectionBase]
    // [DisallowMultipleComponent]
    // [DefaultExecutionOrder(ApplicationManager.EXECUTION_ORDER_DEFAULT_EARLIER)]


    // [ExecuteInEditMode]
    public class CharacterHelper : MonoBehaviour
    {

        [HideInEditorMode]
        [ShowInInspector]
        Character characterContainer;

        [SerializeField]
        [HideInPlayMode]
        bool m_IsPlayer;

        [ShowInInspector]
        [HideInEditorMode]
        bool IsInited;

        [SerializeField]
        [HideInPlayMode]
        bool m_CameraOnly;

        [SerializeField]
        [HideInPlayMode]
        MotionInfo m_MotionInfo;

        [SerializeField]
        [HideInPlayMode]
        public Vector3 m_CameraOffset;

        [SerializeField][HideInPlayMode] public float m_MaxPitch;




        public void Init()
        {
            if (IsInited)
            {
                return;
            }
            Debug.Log($"On Init:{characterContainer}");
            characterContainer = new Character(gameObject, m_CameraOnly);
            characterContainer.SetIsPlayer(m_IsPlayer);
            characterContainer.SetMotionInfo(m_MotionInfo);
            characterContainer.CameraOffset = m_CameraOffset;
            characterContainer.MaxPitch = m_MaxPitch;
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

        void OnDrawGizmos()
        {
            characterContainer?.DrawGizmos();
        }

        void ResetCameraDirection()
        {
            characterContainer.ResetCameraDirection();
        }


    }
}
