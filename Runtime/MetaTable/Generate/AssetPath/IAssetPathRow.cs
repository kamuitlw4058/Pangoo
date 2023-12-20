// 本文件使用工具自动生成，请勿进行手动修改！

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
    public partial interface IAssetPathRow : IMetaTableRow
    {

        public string AssetPackageDir{ get; set; }

        public string AssetType{ get; set; }

        public string AssetPath{ get; set; }

        public string AssetGroup{ get; set; }

    }
}

