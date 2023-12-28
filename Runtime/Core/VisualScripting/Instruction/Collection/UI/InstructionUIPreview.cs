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
    [Category("UI/预览UI")]
    [Serializable]
    public class InstructionUIPreview : Instruction
    {
        [SerializeField]
        [LabelText("参数")]
        [HideReferenceObjectPicker]
        public InstructionUIPreviewParams ParamsRaw = new InstructionUIPreviewParams();
        public override IParams Params => this.ParamsRaw;

        public bool IsUICloed = false;

        [ShowInInspector]
        public override InstructionType InstructionType
        {
            get
            {
                return ParamsRaw.WaitClosed ? InstructionType.Coroutine : InstructionType.Immediate;
            }
        }

        PreviewData BuildPreviewData(Args args)
        {
            var ret = new PreviewData();
            ret.args = args;
            ret.DynamicObject = args.dynamicObject;
            ret.PreviewRow = args.Main.MetaTable.GetDynamicObjectPreviewByUuid(ParamsRaw.PreivewUuid);
            ret.OldPosition = ret.CurrentPosition;
            ret.OldRotation = ret.CurrentRotation;
            ret.OldScale = ret.CurrentScale;
            return ret;
        }


        protected override IEnumerator Run(Args args)
        {
            if (args.dynamicObject == null)
            {
                Debug.LogError($"Preview DynamicObject Is Failed! DynamicObject is null");
                yield break;
            }

            IsUICloed = false;
            args?.Main?.UI?.ShowPreview(BuildPreviewData(args), () =>
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
            if (args.dynamicObject == null)
            {
                Debug.LogError($"Preview DynamicObject Is Failed! DynamicObject is null");
            }

            args?.Main?.UI?.ShowPreview(BuildPreviewData(args));
        }
    }
}
