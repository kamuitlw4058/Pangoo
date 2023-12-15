// 本文件使用工具自动生成，请勿进行手动修改！

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
        [CreateAssetMenu(fileName = "UnityVariablesRow", menuName = "MetaTable/UnityVariablesRow")]
    public partial class UnityVariablesRow : MetaTableUnityRow
    {

        [HideLabel]
        public VariablesRow Row = new();

        public override MetaTableRow BaseRow => Row;

#if UNITY_EDITOR

        public override void SetRow(MetaTableRow row)
        {
           Row = row as VariablesRow;
        }

        public override MetaTableRow CloneRow()
        {
           return CopyUtility.Clone<VariablesRow>(Row);
        }
#endif
    }
}

