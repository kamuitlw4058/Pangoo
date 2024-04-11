
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("游戏段落/设置段落int变量")]
    public class InstructionSetGameSectionIntVariable : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSetGameSectionIntVariableParams ParamsRaw =
            new InstructionSetGameSectionIntVariableParams();
        public override IParams Params => this.ParamsRaw;

        public override void RunImmediate(Args args)
        {
            args.Main.RuntimeData.SetGameSectionVariable<int>(ParamsRaw.GameSectionUuid, ParamsRaw.VariableUuid, ParamsRaw.Value);
        }
    }
}

