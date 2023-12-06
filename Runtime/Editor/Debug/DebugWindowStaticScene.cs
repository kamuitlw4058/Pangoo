using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;

namespace Pangoo
{
    public sealed class DebugWindowStaticScene : DebuggerComponent.ScrollableDebuggerWindowBase
    {
        protected override void OnDrawScrollableWindow()
        {
            var main = PangooEntry.Service.mainService;
            if (main == null || (main != null && main.StaticScene == null))
            {
                return;
            }
            var StaticScene = main.StaticScene;

            GUILayout.Label("<b>进入的场景资源Id</b>");
            GUILayout.BeginVertical("box");
            var KeyValues = StaticScene.EnterAssetCountDict;
            var Keys = KeyValues.Keys.ToList();

            for (int i = 0; i < Keys.Count; i++)
            {
                DrawItem(Keys[i].ToString(), KeyValues[Keys[i]].ToString());
            }
            GUILayout.EndVertical();

            var LoadedSceneAssetDict = StaticScene.LoadedSceneAssetDict;
            GUILayout.Label($"<b>已经加载的场景Ids:{LoadedSceneAssetDict.Count}</b>");
            GUILayout.BeginVertical("box");
            var LoadedSceneAssetDictKeys = LoadedSceneAssetDict.Keys.ToList();
            for (int i = 0; i < LoadedSceneAssetDictKeys.Count; i++)
            {
                var entity = LoadedSceneAssetDict[LoadedSceneAssetDictKeys[i]];
                DrawItem(entity.Name, $"静态场景:{entity.SceneData.Id},资源Id:{entity.SceneData.AssetPathId}", titleWidth: 120);
            }
            GUILayout.EndVertical();



        }

        private string GetBatteryLevelString(float batteryLevel)
        {
            if (batteryLevel < 0f)
            {
                return "Unavailable";
            }

            return batteryLevel.ToString("P0");
        }
    }
}