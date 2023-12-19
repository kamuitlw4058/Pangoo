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
        [CreateAssetMenu(fileName = "UnityStaticSceneRow", menuName = "MetaTable/UnityStaticSceneRow")]
    public partial class UnityStaticSceneRow : MetaTableUnityRow
    {

        [HideLabel]
        public StaticSceneRow Row = new();

        public override MetaTableRow BaseRow => Row;

#if UNITY_EDITOR

        public override void SetRow(MetaTableRow row)
        {
           Row = row as StaticSceneRow;
        }

        public override MetaTableRow CloneRow()
        {
           return CopyUtility.Clone<StaticSceneRow>(Row);
        }
#endif
    }
}

