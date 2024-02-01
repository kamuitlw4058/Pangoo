using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;

namespace Pangoo
{
    public sealed class DebugWindowDynamicObject : DebuggerComponent.ScrollableDebuggerWindowBase
    {
        protected override void OnDrawScrollableWindow()
        {
            var main = PangooEntry.Service.mainService;
            if (main == null || (main != null && main.StaticScene == null))
            {
                return;
            }
            var DynamicObject = main.DynamicObject;

            GUILayout.Label("<b>加载动态物体变量</b>");
            GUILayout.BeginVertical("box");
            var KeyValues = DynamicObject.m_LoadedAssetDict;
            var Keys = KeyValues.Keys.ToList();
            for (int i = 0; i < Keys.Count; i++)
            {
                DrawItem(Keys[i].ToString(), KeyValues[Keys[i]].ToString());
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