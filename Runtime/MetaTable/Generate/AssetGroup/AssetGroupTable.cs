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
    [Serializable]
    public partial class AssetGroupTable : MetaTableBase,IAssetGroupTable
    {


        public IAssetGroupRow GetRowByUuid(string uuid)
        {
            return GetRowByUuid<IAssetGroupRow>(uuid);
        }

        public IAssetGroupRow GetRowById(int id)
        {
            return GetRowById<IAssetGroupRow>(id);
        }

        public override string TableName => "AssetGroup";
    }
}

