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
    public partial interface ICasesRow : IMetaTableRow
    {

        public string DynamicObjectUuid{ get; set; }

        public string CaseTitle{ get; set; }

        public string CaseVariables{ get; set; }

        public string CaseStates{ get; set; }

        public string CaseClues{ get; set; }

        public string CluesIntegrate{ get; set; }

        public string StateModelOnOff{ get; set; }

    }
}

