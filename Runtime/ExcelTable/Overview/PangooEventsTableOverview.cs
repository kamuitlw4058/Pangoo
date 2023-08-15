// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pangoo
{
    public partial class PangooEventsTableOverview : ExcelTableOverview
    {


       public PangooEventsTable Data;

       public override ExcelTableBase GetExcelTableBase()
       {
           return CopyUtility.Clone<PangooEventsTable>(Data);
       }


       public override Type GetDataType()
       {
           return Data.GetType();
       }


       public override string GetJsonPath()
       {
           return "PangooEventsTable";
       }


       public override string GetName()
       {
           return "PangooEvents";
       }

#if UNITY_EDITOR

       [Button("从Excel文件重构数据",30)]
        /// <summary> 加载Excel文件</summary>
        public override void LoadExcelFile()
        {
          Data=new();
          Data.LoadExcelFile(ExcelPath);
        }

#endif
    }
}

