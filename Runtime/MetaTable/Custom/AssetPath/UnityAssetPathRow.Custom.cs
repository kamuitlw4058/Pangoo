
using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;

namespace Pangoo.MetaTable
{
    public partial class UnityAssetPathRow
    {
        public string ToPrefabPath()
        {
            return Row.ToPrefabPath();
        }

        public string ToDirPath()
        {
            return Row.ToDirPath();
        }
    }
}

