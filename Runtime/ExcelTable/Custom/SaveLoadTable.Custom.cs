// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Pangoo
{
    public partial class SaveLoadTable 
    {


        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
        }

public override void Merge(ExcelTableBase val){
 var table = val as SaveLoadTable;
 Rows.AddRange(table.Rows);
}
    }
}

