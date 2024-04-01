using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Pangoo.MetaTable;

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
            var KeyValues = main.RuntimeData.DynamicObjectValueDict;
            var Keys = KeyValues.Keys.ToList();

            for (int i = 0; i < Keys.Count; i++)
            {
                if (KeyValues[Keys[i]] != null)
                {
                    IDynamicObjectRow dynamicObjectRow = main.MetaTable.GetDynamicObjectRow(Keys[i]);
                    DrawDynamicObject(Keys[i], KeyValues[Keys[i]], dynamicObjectRow, main.RuntimeData);
                }
            }
            GUILayout.EndVertical();

        }



        protected static void DrawDynamicObject(string dynamicObjectUuid, DynamicObjectValue content, IDynamicObjectRow dynamicObjectRow, RuntimeDataService runtimeDataService)
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(dynamicObjectRow.Name);
                GUILayout.Label($"kv Count:{content.KeyValueDict.Count}");

            }
            GUILayout.EndHorizontal();
            var keyList = content.KeyValueDict.Keys.ToList();
            for (int i = 0; i < content.KeyValueDict.Count; i++)
            {
                var key = keyList[i];
                var value = content.KeyValueDict[key];
                DrawDynamicObjectVariable(key, value, content, runtimeDataService);
            }

            GUILayout.EndVertical();
        }

        public static void DrawDynamicObjectVariable(string variableUuid, object value, DynamicObjectValue dynamicObjectValue, RuntimeDataService runtimeDataService, float titleWidth = TitleWidth)
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            {
                var variableRow = runtimeDataService.GetVariablesRow(variableUuid);
                GUILayout.Label(variableRow.Name, GUILayout.Width(titleWidth));
                if (value.GetType() == typeof(bool))
                {
                    var toggleValue = GUILayout.Toggle((bool)value, "开关");
                    dynamicObjectValue.Set<bool>(variableUuid, toggleValue);
                }

                if (value.GetType() == typeof(int))
                {
                    var intStrValue = GUILayout.TextField(value.ToString());
                    if (int.TryParse(intStrValue, out int intValue))
                    {
                        dynamicObjectValue.Set<int>(variableUuid, intValue);

                    }
                    else
                    {
                        dynamicObjectValue.Set<int>(variableUuid, 0);

                    }

                }

                if (value.GetType() == typeof(float))
                {
                    var floatStrValue = GUILayout.TextField(value.ToString());
                    if (float.TryParse(floatStrValue, out float floatValue))
                    {
                        dynamicObjectValue.Set<float>(variableUuid, floatValue);

                    }
                    else
                    {
                        dynamicObjectValue.Set<float>(variableUuid, 0);
                    }

                }

            }
            GUILayout.EndHorizontal();
        }


    }
}