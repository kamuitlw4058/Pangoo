
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
    public partial class TriggerEventRow
    {
        public List<string> GetConditionList()
        {
            if (ConditionUuidList.IsNullOrWhiteSpace())
            {
                return new List<string>();
            }
            return ConditionUuidList.ToSplitList<string>();
        }

    }
}

