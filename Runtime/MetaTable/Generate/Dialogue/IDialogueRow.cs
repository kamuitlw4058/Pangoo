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
    public partial interface IDialogueRow : IMetaTableRow
    {

        public string DialogueType{ get; set; }

        public string ActorsLinesUuid{ get; set; }

        public string NextDialogueUuid{ get; set; }

        public string Options{ get; set; }

    }
}

