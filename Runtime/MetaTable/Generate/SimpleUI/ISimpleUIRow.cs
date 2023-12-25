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
    public partial interface ISimpleUIRow : IMetaTableRow
    {

        public int AssetPathId{ get; set; }

        public string SortingLayer{ get; set; }

        public int SortingOrder{ get; set; }

        public string UIParamsType{ get; set; }

        public string Params{ get; set; }

        public string AssetPathUuid{ get; set; }

    }
}

