using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;
using Pangoo.Core.Common;
using Pangoo.Core.Services;

namespace Pangoo
{
    public sealed class DebugWindowRuntimeDataNormal : DebuggerComponent.ScrollableDebuggerWindowBase
    {
        protected override void OnDrawScrollableWindow()
        {
            var main = PangooEntry.Service.mainService;
            if (main == null || (main != null && main.RuntimeData == null))
            {
                return;
            }

            GUILayout.Label("<b>运行数据</b>");
            GUILayout.BeginVertical("box");
            var KeyValues = main.RuntimeData.KeyValues;
            var Keys = KeyValues.Keys.ToList();

            for (int i = 0; i < Keys.Count; i++)
            {
                if (KeyValues[Keys[i]] as DynamicObjectValue == null)
                {
                    DrawVariable(Keys[i], KeyValues[Keys[i]], main.RuntimeData);

                    // DrawItem(Keys[i], KeyValues[Keys[i]].ToString());

                }

            }
            GUILayout.EndVertical();
        }

        public static void DrawVariable(string title, object value, RuntimeDataService runtimeDataService, float titleWidth = TitleWidth)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(title, GUILayout.Width(titleWidth));
                if (value.GetType() == typeof(bool))
                {
                    var toggleValue = GUILayout.Toggle((bool)value, "开关");
                    runtimeDataService.Set<bool>(title, toggleValue);
                }

                if (value.GetType() == typeof(int))
                {
                    var intStrValue = GUILayout.TextField(value.ToString());
                    if (int.TryParse(intStrValue, out int intValue))
                    {
                        runtimeDataService.Set<int>(title, intValue);

                    }
                    else
                    {
                        runtimeDataService.Set<int>(title, 0);
                    }

                }


                if (value.GetType() == typeof(float))
                {
                    var floatStrValue = GUILayout.TextField(value.ToString());
                    if (float.TryParse(floatStrValue, out float floatValue))
                    {
                        runtimeDataService.Set<float>(title, floatValue);

                    }
                    else
                    {
                        runtimeDataService.Set<float>(title, 0);
                    }

                }

            }
            GUILayout.EndHorizontal();
        }
        // protected static void DrawItem(string title, string content, float titleWidth = TitleWidth)
        // {
        //     GUILayout.BeginHorizontal();
        //     {
        //         GUILayout.Label(title, GUILayout.Width(titleWidth));
        //         if (GUILayout.Button(content, "label"))
        //         {
        //             CopyToClipboard(content);
        //         }
        //     }
        //     GUILayout.EndHorizontal();
        // }

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