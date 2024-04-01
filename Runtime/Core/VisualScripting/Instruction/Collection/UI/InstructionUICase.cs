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
    [Category("UI/案件UI")]
    [Serializable]
    public class InstructionUICase : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionUICaseParams ParamsRaw = new InstructionUICaseParams();
        public override IParams Params => this.ParamsRaw;

        public override InstructionType InstructionType
        {
            get
            {
                return ParamsRaw.WaitClosed ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }

        CaseContent BuildCaseContent(Args args)
        {
            var ret = new CaseContent();
            ret.args = args.Clone;
            ret.CaseUuid = ParamsRaw.CaseUuid;
            return ret;
        }


        public bool IsUICloed = false;

        protected override IEnumerator Run(Args args)
        {
            IsUICloed = false;
            args?.Main?.Case?.ShowCase(ParamsRaw.CaseUuid, (o) =>
            {
                Debug.Log($"UI Closed");
                IsUICloed = true;
            });
            while (!IsUICloed)
            {
                yield return null;
            }
        }

        public override void RunImmediate(Args args)
        {
            args?.Main?.Case?.ShowCase(ParamsRaw.CaseUuid);
        }
    }
}
