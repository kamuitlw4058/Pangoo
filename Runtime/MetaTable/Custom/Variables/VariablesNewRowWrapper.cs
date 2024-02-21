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

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class VariablesNewRowWrapper : MetaTableNewRowWrapper<VariablesOverview,UnityVariablesRow>
    {
        public override void Create()
        {
            if (UnityRow!=null)
            {
                UnityRow.Row.VariableType = "DynamicObject";
            }
            base.Create();
        }
    }
}
#endif

