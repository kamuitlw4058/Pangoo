using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;
using Pangoo.Core.Common;

namespace Pangoo
{
    public sealed class DebugWindowInstructions : DebuggerComponent.ScrollableDebuggerWindowBase
    {
        protected override void OnDrawScrollableWindow()
        {
            var main = PangooEntry.Service.mainService;
            if (main == null)
            {
                return;
            }

            GUILayout.Label("<b>进入的场景资源Id</b>");
            GUILayout.BeginVertical("box");
            var InstructionsIds = main.GameConfig.GetGameMainConfig().DebuggerInstructions;
            var InstructionTable = main.GetInstructionTable();
            for (int i = 0; i < InstructionsIds.Length; i++)
            {
                var row = InstructionTable.GetRowById(InstructionsIds[i]);
                DrawButtonItem(row.Id.ToString(), row.Name, "运行", () =>
                {
                    var instruction = row.ToInstruction(InstructionTable);
                    if (instruction.InstructionType == Core.VisualScripting.InstructionType.Immediate)
                    {
                        var args = new Args();
                        args.Main = PangooEntry.Service.mainService;
                        instruction.RunImmediate(args);
                    }
                    Debug.Log($"运行指令");
                });
            }
            // var KeyValues = StaticScene.EnterAssetCountDict;
            // var Keys = KeyValues.Keys.ToList();

            // for (int i = 0; i < Keys.Count; i++)
            // {
            //     DrawItem(Keys[i].ToString(), KeyValues[Keys[i]].ToString());
            // }
            // GUILayout.EndVertical();

            // var LoadedSceneAssetDict = StaticScene.LoadedSceneAssetDict;
            // GUILayout.Label($"<b>已经加载的场景Ids:{LoadedSceneAssetDict.Count}</b>");
            // GUILayout.BeginVertical("box");
            // var LoadedSceneAssetDictKeys = LoadedSceneAssetDict.Keys.ToList();
            // for (int i = 0; i < LoadedSceneAssetDictKeys.Count; i++)
            // {
            //     var entity = LoadedSceneAssetDict[LoadedSceneAssetDictKeys[i]];
            //     DrawItem(entity.Name, entity.ToString());
            // }
            GUILayout.EndVertical();



        }

    }
}