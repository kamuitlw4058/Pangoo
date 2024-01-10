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
        [CreateAssetMenu(fileName = "UnityDialogueRow", menuName = "MetaTable/UnityDialogueRow")]
    public partial class UnityDialogueRow : MetaTableUnityRow
    {

        [HideLabel]
        public DialogueRow Row = new();

        public override MetaTableRow BaseRow => Row;

#if UNITY_EDITOR

        public override void SetRow(MetaTableRow row)
        {
           Row = row as DialogueRow;
        }

        public override MetaTableRow CloneRow()
        {
           return CopyUtility.Clone<DialogueRow>(Row);
        }
#endif
    }
}

