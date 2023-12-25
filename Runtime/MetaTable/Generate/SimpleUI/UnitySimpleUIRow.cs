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
        [CreateAssetMenu(fileName = "UnitySimpleUIRow", menuName = "MetaTable/UnitySimpleUIRow")]
    public partial class UnitySimpleUIRow : MetaTableUnityRow
    {

        [HideLabel]
        public SimpleUIRow Row = new();

        public override MetaTableRow BaseRow => Row;

#if UNITY_EDITOR

        public override void SetRow(MetaTableRow row)
        {
           Row = row as SimpleUIRow;
        }

        public override MetaTableRow CloneRow()
        {
           return CopyUtility.Clone<SimpleUIRow>(Row);
        }
#endif
    }
}

