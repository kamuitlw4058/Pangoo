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
        [CreateAssetMenu(fileName = "UnityActorsLinesRow", menuName = "MetaTable/UnityActorsLinesRow")]
    public partial class UnityActorsLinesRow : MetaTableUnityRow
    {

        [HideLabel]
        public ActorsLinesRow Row = new();

        public override MetaTableRow BaseRow => Row;

#if UNITY_EDITOR

        public override void SetRow(MetaTableRow row)
        {
           Row = row as ActorsLinesRow;
        }

        public override MetaTableRow CloneRow()
        {
           return CopyUtility.Clone<ActorsLinesRow>(Row);
        }
#endif
    }
}

