using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;
using System;
using Pangoo.Core.VisualScripting;

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
            var KeyValues = DynamicObject.m_LoadedAssetDict;
            var Keys = KeyValues.Keys.ToList();
            for (int i = 0; i < Keys.Count; i++)
            {
                var key = Keys[i];
                DrawDynamicObject(KeyValues[key]);
            }



        }

        static void LabelB(string content)
        {
            GUILayout.Label($"<b>{content}</b>");
        }

        public static void DrawDynamicObject(EntityDynamicObject entityDynamicObject, float titleWidth = TitleWidth)
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            {
                LabelB(entityDynamicObject.Name);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUILayout.Label("触发器");
            GUILayout.Space(2);
            foreach (var triggerKV in entityDynamicObject.DynamicObj.TriggerDict)
            {
                var triggerList = triggerKV.Value;
                var first = true;
                foreach (var trigger in triggerList)
                {
                    string key = triggerKV.Key.ToString();
                    if (!first)
                    {
                        key = string.Empty;
                    }
                    DrawTrigger(key, trigger);
                }

            }
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("EnterTriggerCount", GUILayout.Width(TitleWidth));
            var intStrValue = GUILayout.TextField(entityDynamicObject.DynamicObj.EnterTriggerCount.ToString());
            if (int.TryParse(intStrValue, out int intValue))
            {
                entityDynamicObject.DynamicObj.EnterTriggerCount = intValue;

            }
            else
            {
                entityDynamicObject.DynamicObj.EnterTriggerCount = 0;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        public const float TriggerKeyWidth = 100f;

        static void DrawTrigger(string key, TriggerEvent trigger)
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label(key, GUILayout.Width(TriggerKeyWidth));
                GUILayout.BeginVertical("box");
                GUILayout.BeginHorizontal();
                var triggerEnabled = GUILayout.Toggle(trigger.Enabled, trigger.Name);
                trigger.Enabled = triggerEnabled;
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                var triggerIsRunning = GUILayout.Toggle(trigger.IsRunning, "是否在运行");
                GUILayout.EndHorizontal();
                GUILayout.Label("指令");
                GUILayout.Space(2);
                GUILayout.BeginHorizontal();
                GUILayout.Label($"触发条件:{trigger.ConditionType}");
                GUILayout.EndHorizontal();
                foreach (var kv in trigger.ConditionInstructions)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"State:{kv.Key}", GUILayout.Width(60));
                    var isInstructionRunning = GUILayout.Toggle(kv.Value.IsRunning, "是否在运行", GUILayout.Width(100));
                    GUILayout.Label($"指令列表长度:{kv.Value.Length} 指令Index:{kv.Value.RunningIndex}");
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
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