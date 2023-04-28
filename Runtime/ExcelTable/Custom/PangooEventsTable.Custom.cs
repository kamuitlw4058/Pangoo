// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo
{
    public partial class PangooEventsTable
    {


        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
        }

        public override void Merge(ExcelTableBase val)
        {
            var table = val as PangooEventsTable;
            Rows.AddRange(table.Rows);
        }
    }
}

