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
    public partial interface IDynamicObjectPreviewRow : IMetaTableRow
    {

        public string Title{ get; set; }

        public string OnShowInstructions{ get; set; }

        public string OnGrabInstructions{ get; set; }

        public string OnPreviewInstructions{ get; set; }

        public string OnCloseInstructions{ get; set; }

        public string GrabSoundUuid{ get; set; }

        public string ExitKeyCodes{ get; set; }

        public string PreviewInteractKeyCodes{ get; set; }

        public string Params{ get; set; }

    }
}

