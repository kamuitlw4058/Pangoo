// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
using System.IO;
using System.Linq;
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
        [CreateAssetMenu(fileName = "UnityCluesRow", menuName = "MetaTable/UnityCluesRow")]
    public partial class UnityCluesRow : MetaTableUnityRow
    {

        [HideLabel]
        public CluesRow Row = new();

        public override MetaTableRow BaseRow => Row;

#if UNITY_EDITOR

        public override void SetRow(MetaTableRow row)
        {
           Row = row as CluesRow;
        }

        public override MetaTableRow CloneRow()
        {
           return CopyUtility.Clone<CluesRow>(Row);
        }
#endif
    }
}

