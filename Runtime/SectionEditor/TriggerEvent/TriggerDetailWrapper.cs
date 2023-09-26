#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using System;

namespace Pangoo
{
    [Serializable]
    public class TriggerDetailWrapper : ExcelTableRowDetailWrapper<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>
    {

        [ShowInInspector]
        [PropertyOrder(0)]
        public TriggerTypeEnum TriggerType
        {
            get
            {
                if (Row == null)
                {
                    return TriggerTypeEnum.Unknown;
                }

                if (Row.TriggerType == null)
                {
                    Row.TriggerType = string.Empty;
                }

                switch (Row.TriggerType)
                {
                    case "OnInteract":
                        return TriggerTypeEnum.OnInteract;
                    default:
                        return TriggerTypeEnum.Unknown;
                }
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    switch (value)
                    {
                        case TriggerTypeEnum.Unknown:
                            Row.TriggerType = string.Empty;
                            break;
                        case TriggerTypeEnum.OnInteract:
                            Row.TriggerType = TriggerTypeEnum.OnInteract.ToString();
                            break;
                    }
                    Save();
                }

            }
        }


        [LabelText("指令Ids")]
        [ValueDropdown("InstructionIdValueDropdown", IsUniqueList = true)]

        // [OnValueChanged("OnDynamicSceneIdsChanged")]
        [ListDrawerSettings(Expanded = true)]

        [ShowInInspector]
        [PropertyOrder(1)]
        public int[] InstructionIds
        {
            get
            {
                return Row?.InstructionList?.ToArrInt() ?? new int[0];
            }
            set
            {

                if (Row != null && Overview != null)
                {
                    Row.InstructionList = value.ToList().ToListString();
                    Save();
                }

            }
        }

        public IEnumerable InstructionIdValueDropdown()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<InstructionTableOverview>();
        }


    }
}
#endif