using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Category("Variable/大相框格子左键条件判断")]
    [Serializable]
    public class ConditionDaXiangKuangGeZiLeft : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        private ConditionDaXiangKuangGeZiLeftParams ParamsRaw = new ConditionDaXiangKuangGeZiLeftParams();
        public override IParams Params => ParamsRaw;
        public override ConditionTypeEnum ConditionType => ConditionTypeEnum.StateCondition;
        protected override int Run(Args args)
        {
            bool isStatic = args.dynamicObject.GetVariable<bool>(ParamsRaw.StaticVariableUuid);
            bool isHas = args.dynamicObject.GetVariable<bool>(ParamsRaw.HasVariableUuid);
            bool isUsed = args.dynamicObject.GetVariable<bool>(ParamsRaw.UsedVariableUuid);

            if (isStatic)
            {
                //播放点击静态照片音频
                return 0;
            }
            else
            {
                if (!isHas)
                {
                    //播放没有照片的音频
                    return 1;
                }
                else
                {
                    if (isUsed)
                    {
                        //播放已经使用过照片的音频
                        return 2;
                    }
                    else
                    {
                        //执行贴照片的逻辑指令
                        return 3;
                    }
                }
            }
        }
    }
}
