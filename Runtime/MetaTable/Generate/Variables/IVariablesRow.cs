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
    public partial interface IVariablesRow : IMetaTableRow
    {

        public string VariableType{ get; set; }

        public string Key{ get; set; }

        public string ValueType{ get; set; }

        public string DefaultValue{ get; set; }

        public bool NeedSave{ get; set; }

    }
}

