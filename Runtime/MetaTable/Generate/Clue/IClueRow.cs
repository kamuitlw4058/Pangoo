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
    public partial interface IClueRow : IMetaTableRow
    {

        public string DynamicObjectUuid{ get; set; }

        public string ClueTitle{ get; set; }

        public string Desc{ get; set; }

        public string ClueKey{ get; set; }

        public string ClueBackTitle{ get; set; }

        public string ClueBackDesc{ get; set; }

    }
}

