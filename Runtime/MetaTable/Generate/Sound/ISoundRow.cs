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
    public partial interface ISoundRow : IMetaTableRow
    {

        public string PackageDir{ get; set; }

        public string SoundType{ get; set; }

        public string AssetPath{ get; set; }

    }
}

