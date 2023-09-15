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
        CharacterContainer characterContainer;

        [SerializeField][HideInPlayMode] bool m_IsPlayer;
        [ShowInInspector, ReadOnly] bool IsInited;

        [SerializeField][HideInPlayMode] MotionInfo m_MotionInfo;

        // [ShowInInspector] Vector3 MoveDirection{
        //     get{
        //         if(characterContainer == null){
        //             return Vector3.zero;
        //         }
        //         var val =characterContainer?.GetVariable<Vector3>("MoveDirection");
        //         return val != null ? val.Value : Vector3.zero;
        //     }
        // }

        public void Init()
        {
            if (IsInited)
            {
                return;
            }
            Debug.Log($"On Init:{characterContainer}");
            characterContainer = new CharacterContainer(gameObject);
            characterContainer.SetIsPlayer(m_IsPlayer);
            characterContainer.SetMotionInfo(m_MotionInfo);
            characterContainer.Awake(null);

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

        [Button("强制初始化")]
        public void ForceAwake()
        {
            characterContainer = new CharacterContainer(gameObject);
            characterContainer.Awake(null);
            IsInited = true;
        }
        [Button("设置移动")]
        public void SetMotionDirection()
        {
            characterContainer?.SetVariable<Vector3>("MoveDirection", new Vector3(0, 1, 0));
        }

    }
}
#endif