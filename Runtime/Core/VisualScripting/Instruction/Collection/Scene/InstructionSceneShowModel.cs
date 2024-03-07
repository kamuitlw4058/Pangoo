using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using System;
using LitJson;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace Pangoo.Core.VisualScripting
{
    // [Common.Title("PlayTimeline")]
    [Category("场景/场景开关模型")]
    [Serializable]
    public class InstructionSceneShow : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionSceneBoolParams ParamsRaw = new InstructionSceneBoolParams();

        public override IParams Params => this.ParamsRaw;

        protected override IEnumerator Run(Args args)
        {
            RunImmediate(args);
            yield break;
        }

        public override void RunImmediate(Args args)
        {
            var main = args.Main;
            main.StaticScene.SetSceneModelActive(ParamsRaw.SceneUuid, ParamsRaw.Val);
        }


    }
}
