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

    [Category("Common/预览交互")]

    [Serializable]
    public class ConditionPreviewInteract : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionEmptyParams ParamRaw = new ConditionEmptyParams();

        public override IParams Params => ParamRaw;

        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;



        public ConditionPreviewInteract()
        { }


        protected override int Run(Args args)
        {
            var variableUuid = args.Main.DefaultPreviewInteraceVariableUuid;
            if (!variableUuid.IsNullOrWhiteSpace())
            {
                return args.Main.RuntimeData.GetVariable<int>(variableUuid);
            }
            Debug.LogError($"预览交互条件变量获取失败");
            return -1;
        }



    }
}
