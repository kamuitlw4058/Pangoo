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
    public partial interface IDynamicObjectRow : IMetaTableRow
    {

        public int AssetPathId{ get; set; }

        public string NameCn{ get; set; }

        public string Space{ get; set; }

        public Vector3 Position{ get; set; }

        public Vector3 Rotation{ get; set; }

        public Vector3 Scale{ get; set; }

        public string TriggerEventIds{ get; set; }

        public bool UseHotspot{ get; set; }

        public bool DefaultHideHotspot{ get; set; }

        public float HotspotRadius{ get; set; }

        public string HotspotIds{ get; set; }

        public Vector3 HotspotOffset{ get; set; }

        public string DirectInstructions{ get; set; }

        public string SubDynamicObject{ get; set; }

        public float InteractRadius{ get; set; }

        public Vector3 InteractOffset{ get; set; }

        public float InteractRadian{ get; set; }

        public string InteractTarget{ get; set; }

        public bool DefaultHideModel{ get; set; }

        public string AssetPathUuid{ get; set; }

        public bool DefaultDisableInteract{ get; set; }

        public string TriggerEventUuids{ get; set; }

        public string HotspotUuids{ get; set; }

    }
}

