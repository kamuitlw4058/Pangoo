using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityGameFramework.Editor.ResourceTools;

namespace Pangoo.Editor
{
    public static class PlatformUtility
    {
        public static Platform GetCurrentPlatform()
        {
            var platform = Platform.Windows64;
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    platform = Platform.Android;
                    break;
                case BuildTarget.StandaloneWindows64:
                    platform = Platform.Windows64;
                    break;
                case BuildTarget.iOS:
                    platform = Platform.IOS;
                    break;

            }

            return platform;
        }

        public static void SwitchPlatform(Platform platform)
        {
            if (platform == GetCurrentPlatform())
            {
                return;
            }

            switch (platform)
            {
                case Platform.Android:
                    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
                    break;
                case Platform.Windows64:
                    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
                    break;
                case Platform.MacOS:
                    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
                    break;
                case Platform.IOS:
                    EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
                    break;
            }
        }


        public static ValueDropdownList<Platform> GetAllSupportPlatform()
        {
            return new ValueDropdownList<Platform> { Platform.Android, Platform.Windows64, Platform.IOS };
        }


        public static void SwitchMainScene()
        {
            // if (SceneManager.GetActiveScene().name != "Client")
            // {
            //     EditorSceneManager.OpenScene($"{CommonConstant.SCENE_DIR}/Client.unity");
            // }
        }
    }
}