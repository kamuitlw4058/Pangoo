#if UNITY_EDITOR

using System;
using System.IO;
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
    [Serializable]
    public partial class VariablesNewRowWrapper : MetaTableNewRowWrapper<VariablesOverview, UnityVariablesRow>
    {
        [ShowInInspector]
        [InfoBox("已经有对应的Key", InfoMessageType.Error, "CheckExistsKey")]

        public string Key
        {
            get
            {
                return UnityRow.Row.Key;
            }
            set
            {
                UnityRow.Row.Key = value;
            }
        }

        bool CheckExistsKey()
        {
            return VariablesOverview.CheckExistsKey(Key);
        }


        [ShowInInspector]
        [LabelText("变量类型")]
        public VariableTypeEnum VariableType
        {
            get
            {
                return UnityRow.Row.VariableType.ToEnum<VariableTypeEnum>();
            }
            set
            {
                UnityRow.Row.VariableType = value.ToString();
            }
        }

        public override void Create()
        {
            if (UnityRow != null)
            {
                var variableType = UnityRow?.Row?.VariableType;
                if (variableType.IsNullOrWhiteSpace())
                {
                    UnityRow.Row.VariableType = VariableTypeEnum.DynamicObject.ToString();
                }
            }
            if (CheckExistsKey())
            {
                Debug.LogError($"Row: Key:{Key}  exists！");
                return;
            }


            base.Create();
        }
    }
}
#endif

