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
    public partial class TriggerEventTable : MetaTableBase
    {

        public TriggerEventRow GetRowByUuid(string uuid)
        {
            return GetRowByUuid<TriggerEventRow>(uuid);
        }
        public override string TableName => "TriggerEvent";
    }
}

