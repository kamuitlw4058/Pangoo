
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
            if (gameConfig == null)
            {
                Debug.LogError("当前 GameMainConfig！还没有建立请先建立GameMainConfig");
                return null;
            }


            return gameConfig;
        }




    }
}
#endif