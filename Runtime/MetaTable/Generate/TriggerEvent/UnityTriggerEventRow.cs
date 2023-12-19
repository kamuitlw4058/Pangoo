// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections;
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
        [CreateAssetMenu(fileName = "UnityTriggerEventRow", menuName = "MetaTable/UnityTriggerEventRow")]
    public partial class UnityTriggerEventRow : MetaTableUnityRow
    {

        [HideLabel]
        public TriggerEventRow Row = new();

        public override MetaTableRow BaseRow => Row;

#if UNITY_EDITOR

        public override void SetRow(MetaTableRow row)
        {
           Row = row as TriggerEventRow;
        }

        public override MetaTableRow CloneRow()
        {
           return CopyUtility.Clone<TriggerEventRow>(Row);
        }
#endif
    }
}

