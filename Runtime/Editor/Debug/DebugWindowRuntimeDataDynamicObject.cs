using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;
using Pangoo.Core.Common;
using Pangoo.Core.Services;

namespace Pangoo
{
    public sealed class DebugWindowRuntimeDataDynamicObject : DebuggerComponent.ScrollableDebuggerWindowBase
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
                if (KeyValues[Keys[i]] as DynamicObjectValue != null)
                {
                    DrawDynamicObject(Keys[i], KeyValues[Keys[i]] as DynamicObjectValue, main.RuntimeData);
                }
            }
            GUILayout.EndVertical();

        }



        protected static void DrawDynamicObject(string title, DynamicObjectValue content, RuntimeDataService runtimeDataService, float titleWidth = TitleWidth)
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(title);
                GUILayout.Label($"kv Count:{content.KeyValueDict.Count}");

            }
            GUILayout.EndHorizontal();
            var keyList = content.KeyValueDict.Keys.ToList();
            for (int i = 0; i < content.KeyValueDict.Count; i++)
            {
                var key = keyList[i];
                var value = content.KeyValueDict[key];
                DrawDynamicObjectVariable(key, value, content);
            }

            GUILayout.EndVertical();


        }

        public static void DrawDynamicObjectVariable(string title, object value, DynamicObjectValue dynamicObjectValue, float titleWidth = TitleWidth)
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(title, GUILayout.Width(titleWidth));
                if (value.GetType() == typeof(bool))
                {
                    var toggleValue = GUILayout.Toggle((bool)value, "开关");
                    dynamicObjectValue.Set<bool>(title, toggleValue);
                }

                if (value.GetType() == typeof(int))
                {
                    var intStrValue = GUILayout.TextField(value.ToString());
                    if (int.TryParse(intStrValue, out int intValue))
                    {
                        dynamicObjectValue.Set<int>(title, intValue);

                    }
                    else
                    {
                        dynamicObjectValue.Set<int>(title, 0);

                    }

                }

                if (value.GetType() == typeof(float))
                {
                    var floatStrValue = GUILayout.TextField(value.ToString());
                    if (float.TryParse(floatStrValue, out float floatValue))
                    {
                        dynamicObjectValue.Set<float>(title, floatValue);

                    }
                    else
                    {
                        dynamicObjectValue.Set<float>(title, 0);
                    }

                }

            }
            GUILayout.EndHorizontal();
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