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
    public partial class EntityGroupTable : MetaTableBase,IEntityGroupTable
    {


        public IEntityGroupRow GetRowByUuid(string uuid)
        {
            return GetRowByUuid<IEntityGroupRow>(uuid);
        }

        public IEntityGroupRow GetRowById(int id)
        {
            return GetRowById<IEntityGroupRow>(id);
        }

        public override string TableName => "EntityGroup";
    }
}

