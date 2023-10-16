using UnityEngine;

namespace Pangoo.Core.Common
{
    public static class ShortcutMainCamera
    {
#if UNITY_EDITOR

        [UnityEditor.InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode() => _Instance = null;

#endif

        // MEMBERS: -------------------------------------------------------------------------------

        private static Camera _Instance;

        // PROPERTIES: ----------------------------------------------------------------------------

        public static Camera Instance
        {
            get
            {
                if (_Instance == null) LocateCamera();
                return _Instance;
            }
            private set => _Instance = value;
        }

        public static Transform Transform => Instance != null ? Instance.transform : null;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        // public static TComponent Get<TComponent>() where TComponent : Component
        // {
        //     return Instance != null ? Instance.Get<TComponent>() : null;
        // }

        public static void Change(Camera camera)
        {
            Instance = camera != null ? camera : null;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private static void LocateCamera()
        {
            if (_Instance != null) return;

            GameObject cameraTag = GameObject.FindWithTag("MainCamera");
            if (cameraTag != null)
            {
                var camera = cameraTag.GetComponent<Camera>();
                if (camera != null)
                {
                    _Instance = camera;
                    return;
                }

            }

            Camera cameraComponent = Object.FindAnyObjectByType<Camera>();
            if (cameraComponent != null)
            {
                _Instance = cameraComponent;
                return;
            }

            Debug.LogWarning("No 'Main Camera' found");
        }
    }
}