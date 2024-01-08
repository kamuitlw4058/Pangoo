#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    public partial class ConditionOverview
    {
        public static IEnumerable GetConditionUuidDropdown(ConditionTypeEnum type)
        {
            var ret = new ValueDropdownList<string>();
            var overviews = AssetDatabaseUtility.FindAsset<ConditionOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {
                    var instrance = ClassUtility.CreateInstance(row.Row.ConditionType) as Condition;
                    if (instrance == null || (instrance != null && instrance.ConditionType != type))
                    {
                        continue;
                    }

                    ret.Add($"{row.UuidShort}-{row.Name}", row.Uuid);
                }
            }
            return ret;
        }
    }
}
#endif

