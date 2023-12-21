
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
    public partial interface IDynamicObjectRow
    {
        List<int> GetHotspotIdList();
        List<string> GetHotspotUuidList();


        List<int> GetTriggerEventIdList();

        List<string> GetTriggerEventUuidList();

    }
}
