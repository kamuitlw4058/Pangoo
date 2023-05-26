using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using OfficeOpenXml;
using System.Xml.Serialization;

namespace Pangoo
{
    public partial class UiConfigInfoTable
    {
        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
        }

        public override void Merge(ExcelTableBase val)
        {
            var table = val as UiConfigInfoTable;
            Rows.AddRange(table.Rows);
        }
    }
}