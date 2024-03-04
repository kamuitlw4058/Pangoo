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
    public partial class VariablesOverview
    {

        public static bool CheckExistsKey(string key)
        {
            if (key.IsNullOrWhiteSpace()) return true;

            var overviews = AssetDatabaseUtility.FindAsset<VariablesOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {
                    if (key.Equals(row.Row.Key))
                    {
                        return true;
                    }


                }
            }
            return false;
        }


        public static bool EqualValueType(string rowVal, string inputVal)
        {
            if (inputVal.IsNullOrWhiteSpace())
            {
                return true;
            }

            if (inputVal.Equals(rowVal))
            {
                return true;
            }

            return false;
        }

        public static bool EqualVariableType(string rowVal, string inputVal)
        {
            if (inputVal.IsNullOrWhiteSpace())
            {
                return true;
            }

            if (inputVal.Equals(rowVal) || (inputVal.Equals(VariableTypeEnum.DynamicObject.ToString()) && rowVal.IsNullOrWhiteSpace()))
            {
                return true;
            }


            return false;
        }


        public static IEnumerable GetVariableUuidDropdown(string valueType, string variableType = null, bool defaultOptions = false)
        {
            var ret = new ValueDropdownList<string>();
            if (defaultOptions)
            {
                ret.Add(ConstString.Default);
            }
            var overviews = AssetDatabaseUtility.FindAsset<VariablesOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {
                    if (EqualValueType(row.Row.ValueType, valueType)
                    && EqualVariableType(row.Row.VariableType, variableType))
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

