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

namespace Pangoo.MetaTable
{
    public partial class VariablesOverview 
    {
        public static IEnumerable GetVariableUuidDropdown(string valueType, string variableType = null)
        {
            var ret = new ValueDropdownList<string>();
            var overviews = AssetDatabaseUtility.FindAsset<VariablesOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {
                    if (variableType != null && !row.Row.VariableType.Equals(variableType))
                    {
                        continue;
                    }

                    bool flag = valueType.IsNullOrWhiteSpace() ? true : valueType.Equals(row.Row.ValueType) ? true : false;
                    if (flag)
                    {
                        ret.Add($"{row.UuidShort}-{row.Name}", row.Uuid);
                    }


                }
            }
            return ret;
        }
    }
}
#endif

