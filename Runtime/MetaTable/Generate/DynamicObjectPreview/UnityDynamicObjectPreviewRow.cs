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
        [CreateAssetMenu(fileName = "UnityDynamicObjectPreviewRow", menuName = "MetaTable/UnityDynamicObjectPreviewRow")]
    public partial class UnityDynamicObjectPreviewRow : MetaTableUnityRow
    {

        [HideLabel]
        public DynamicObjectPreviewRow Row = new();

        public override MetaTableRow BaseRow => Row;

#if UNITY_EDITOR

        public override void SetRow(MetaTableRow row)
        {
           Row = row as DynamicObjectPreviewRow;
        }

        public override MetaTableRow CloneRow()
        {
           return CopyUtility.Clone<DynamicObjectPreviewRow>(Row);
        }
#endif
    }
}

