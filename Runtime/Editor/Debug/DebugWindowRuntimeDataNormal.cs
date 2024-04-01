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
                DrawVariable(Keys[i], KeyValues[Keys[i]], main.RuntimeData);
            }
            GUILayout.EndVertical();
        }

        public static void DrawVariable(string uuid, object value, RuntimeDataService runtimeDataService, float titleWidth = TitleWidth)
        {
            GUILayout.BeginHorizontal();
            {
                var row = runtimeDataService.GetVariablesRow(uuid);
                GUILayout.Label(row.Name, GUILayout.Width(titleWidth));
                if (value.GetType() == typeof(bool))
                {
                    var toggleValue = GUILayout.Toggle((bool)value, "开关");
                    runtimeDataService.Set<bool>(uuid, toggleValue);
                }

                if (value.GetType() == typeof(int))
                {
                    var intStrValue = GUILayout.TextField(value.ToString());
                    if (int.TryParse(intStrValue, out int intValue))
                    {
                        runtimeDataService.Set<int>(uuid, intValue);

                    }
                    else
                    {
                        runtimeDataService.Set<int>(uuid, 0);
                    }

                }


                if (value.GetType() == typeof(float))
                {
                    var floatStrValue = GUILayout.TextField(value.ToString());
                    if (float.TryParse(floatStrValue, out float floatValue))
                    {
                        runtimeDataService.Set<float>(uuid, floatValue);

                    }
                    else
                    {
                        runtimeDataService.Set<float>(uuid, 0);
                    }

                }

            }
            GUILayout.EndHorizontal();
        }
    }
}