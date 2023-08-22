
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
namespace Pangoo.Editor
{

    public static class GameMainConfigEditorUtility
    {

        public static GameMainConfig GetGameMainConfig()
        {
            var gameConfig = AssetDatabaseUtility.FindAssetFirst<GameMainConfig>();
            if(gameConfig == null){
                Debug.LogError( "当前 GameMainConfig！还没有建立请先建立GameMainConfig");
                return null;
            }


            return gameConfig;
        }

        public static string GetDefaultScenePath(){
            var gameConfig = GetGameMainConfig();
            if(gameConfig == null){
                return null;
            }

            if(string.IsNullOrWhiteSpace(gameConfig.DefaultJumpScene)){
                Debug.Log("DefaultJumpScene is null or white space");
                return null;
            }
            return AssetUtility.GetScene(gameConfig.PackageDir,gameConfig.DefaultJumpScene);
        }


        public static void SwitchDefaultScene(){
            var path = GetDefaultScenePath();
            SceneAsset sceneAsset = AssetDatabaseUtility.LoadAssetAtPath<SceneAsset>(path);
            var sceneAssetPath = AssetDatabase.GetAssetPath(sceneAsset);
            var activeScene = EditorSceneManager.GetActiveScene();
            if(activeScene.path == sceneAssetPath){
                // Debug.Log($"activeScene.path:{activeScene.path} path:{path} sceneAssetPath:{sceneAssetPath}");
                return;
            }

            // 保存当前场景
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            // 关闭当前场景
            EditorSceneManager.OpenScene(path);
        }

    }
}
#endif