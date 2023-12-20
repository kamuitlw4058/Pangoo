using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;
using Pangoo.Core.Common;
using Pangoo.Core.VisualScripting;

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
            var InstructionHandler = main.GetInstructionRowByIdHandler();
            for (int i = 0; i < InstructionsIds.Length; i++)
            {
                var row = InstructionHandler(InstructionsIds[i]);
                DrawButtonItem(row.Id.ToString(), row.Name, "运行", () =>
                {
                    var instruction = Instruction.BuildFromRow(row);
                    if (instruction.InstructionType == Core.VisualScripting.InstructionType.Immediate)
                    {
                        var args = new Args();
                        args.Main = PangooEntry.Service.mainService;
                        instruction.RunImmediate(args);
                    }
                    Debug.Log($"运行指令");
                });
            }
            GUILayout.EndVertical();



        }

    }
}