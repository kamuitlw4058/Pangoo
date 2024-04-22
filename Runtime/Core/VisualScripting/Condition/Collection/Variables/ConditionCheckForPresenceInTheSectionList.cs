using System;
using System.Collections;
using System.Collections.Generic;
using Pangoo.Core.Common;
using Pangoo.Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo.Core.VisualScripting
{
    [Common.Title("Condition Variable")]

    [Category("Variable/检查当前场景是否在条件的场景列表中")]

    [Serializable]
    public class ConditionCheckForPresenceInTheSectionList : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionCheckForPresenceInTheSectionListParams ParamRaw = new ConditionCheckForPresenceInTheSectionListParams();

        public override IParams Params => ParamRaw;
        protected override int Run(Args args)
        {
            var currentGameSection = args.Main?.GetService<GameSectionService>().CurrentUuid;
            if (currentGameSection == null)
            {
                //Debug.Log("没有获取到章段落");
                return 0;
            }
            for (int i = 0; i < ParamRaw.GameSectionUuidArray.Length; i++)
            {
                if (ParamRaw.GameSectionUuidArray[i] == currentGameSection)
                {
                    //Debug.Log("包含当前章节段落");
                    return 1;
                }
            }
            //Debug.Log("不包含当前章节段落");
            return 0;
        }
    }
}

