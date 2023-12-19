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
    public partial class VariablesRowWrapper : MetaTableRowWrapper<VariablesOverview, VariablesNewRowWrapper, UnityVariablesRow>
    {
        [ShowInInspector]
        public int Id
        {
            get
            {
                return UnityRow.Row.Id;
            }
        }

        [ShowInInspector]
        // [LabelText("变量值类型")]

        public VariableTypeEnum VariableType
        {
            get
            {
                return UnityRow.Row.VariableType.ToEnum<VariableTypeEnum>();
            }

        }
    }
}
#endif

