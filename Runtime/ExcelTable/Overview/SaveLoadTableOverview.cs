// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pangoo
{
    public partial class SaveLoadTableOverview : ExcelTableOverview
    {


         [TableList(IsReadOnly = true, AlwaysExpanded = true)]
       public SaveLoadTable Data;

       public override ExcelTableBase GetExcelTableBase()
       {
           return CopyUtility.Clone<SaveLoadTable>(Data);
       }


       public override Type GetDataType()
       {
           return Data.GetType();
       }


       public override int GetRowCount()
       {
           return Data != null ? Data.Rows.Count : 0;
       }


       public override string GetJsonPath()
       {
           return "SaveLoadTable";
       }


       public override string GetName()
       {
           return "SaveLoad";
       }

#if UNITY_EDITOR

       [Button("从Excel文件重构数据",30)]
        /// <summary> 加载Excel文件</summary>
        public override void LoadExcelFile()
        {
          Data=new();
          string excelDirPath = Path.Join(PackageDir,ExcelDirPath);
          string excelFile=excelDirPath+ "/" + this.name + ".xlsx";
          Data.LoadExcelFile(excelFile);
        }


       [Button("生成Excel文件",30)]
        /// <summary> 生成Excel文件</summary>
        public override void BuildExcelFile()
        {
          BuildExcelFile(Data);
        }

#endif
    }
}
