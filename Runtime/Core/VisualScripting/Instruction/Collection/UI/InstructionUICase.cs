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



        CaseContent BuildCaseData(Args args)
        {
            var ret = new CaseContent();
            ret.args = args.Clone;
            ret.CaseRow = args.Main.MetaTable.GetCaseByUuid(ParamsRaw.CaseUuid);
            return ret;
        }


        protected override IEnumerator Run(Args args)
        {
            // if (args.dynamicObject == null)
            // {
            //     Debug.LogError($"Preview DynamicObject Is Failed! DynamicObject is null");
            //     yield break;
            // }

            // IsUICloed = false;
            // args?.Main?.UI?.ShowPreview(BuildPreviewData(args), () =>
            // {
            //     Debug.Log($"UI Closed");
            //     IsUICloed = true;
            // });
            // while (!IsUICloed)
            // {
            //     yield return null;
            // }
            yield break;
        }

        public override void RunImmediate(Args args)
        {

            args?.Main?.UI?.ShowCase(BuildCaseData(args));
        }
    }
}
