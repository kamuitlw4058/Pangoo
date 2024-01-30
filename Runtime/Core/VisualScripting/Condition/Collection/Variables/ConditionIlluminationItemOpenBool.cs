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

    [Category("Variable/照明道具开启条件")]

    [Serializable]
    public class ConditionIlluminationItemOpenBool : Condition
    {
        [SerializeField]
        [HideLabel]
        [HideReferenceObjectPicker]
        public ConditionIlluminationItemOpenBoolParams ParamRaw = new ConditionIlluminationItemOpenBoolParams();

        public override IParams Params => ParamRaw;
        protected override int Run(Args args)
        {
            ParamRaw.CurrentGameSection = args.Main?.GetService<GameSectionService>().LatestUuid;
            if (ParamRaw.CurrentGameSection==null)
            {
                //Debug.Log("没有获取到章段落");
                return 0;
            }
            bool isContainSection = ParamRaw.GameSectionUuidList.Find(x => x == ParamRaw.CurrentGameSection)!=null;
            var canOpenVariable=args.dynamicObject.GetVariable<bool>(ParamRaw.CanOpenVariableUuid);
            ParamRaw.CanOpen = canOpenVariable;
            var openStateVariable = args.dynamicObject.GetVariable<bool>(ParamRaw.OpenStateVariableUuid);
            ParamRaw.CurrentOpenState = openStateVariable;

            if (isContainSection && canOpenVariable && !openStateVariable)
            {
                //Debug.Log("开启照明道具");
                return 1;
            }
            //Debug.Log("关闭照明道具");
            return 0;
        }
    }
}

