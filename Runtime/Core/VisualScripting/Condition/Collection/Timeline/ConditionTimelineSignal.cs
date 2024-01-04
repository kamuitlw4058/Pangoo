using System;
using System.Collections;
using System.Threading.Tasks;
using GameFramework;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework;

namespace Pangoo.Core.VisualScripting
{

    // [Version(0, 1, 1)]

    [Common.Title("Condition Variable")]

    [Category("Timeline/Signal")]

    [Serializable]
    public class ConditionTimelineSignal : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionStringMapperParams ParamRaw = new ConditionStringMapperParams();

        public override IParams Params => ParamRaw;

        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;


        public ConditionTimelineSignal()
        { }


        protected override int Run(Args args)
        {
            if (args.signalAssetName.IsNullOrWhiteSpace())
            {
                return 0;
            }
            if (ParamRaw.StringMapper.TryGetValue(args.signalAssetName, out int ret))
            {
                return ret;
            }
            return 0;
        }



    }
}
