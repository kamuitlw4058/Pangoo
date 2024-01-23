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

    // [Common.Title("Condition Variable")]

    [Category("Common/目标映射")]

    [Serializable]
    public class ConditionTargetMapping : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionStringMappingParams ParamRaw = new ConditionStringMappingParams();

        public override IParams Params => ParamRaw;

        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;


        public ConditionTargetMapping()
        { }


        protected override int Run(Args args)
        {
            if (args.TargetPath.IsNullOrWhiteSpace()) return -1;
            if (ParamRaw.StringMapper.TryGetValue(args.TargetPath, out int ret))
            {
                return ret;
            }
            return -1;
        }



    }
}
