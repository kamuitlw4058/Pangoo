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
    public partial class DynamicObjectTableOverview : ExcelTableOverview
    {


       public DynamicObjectTable Data;

       public override ExcelTableBase GetExcelTableBase()
       {
           return CopyUtility.Clone<DynamicObjectTable>(Data);
       }


       public override Type GetDataType()
       {
           return Data.GetType();
       }

       public override ExcelTableBase Table{
          get{
           return Data;
          }
       }

       public override string GetJsonPath()
       {
           return "DynamicObjectTable";
       }


       public override string GetName()
       {
           return "DynamicObject";
       }

#if UNITY_EDITOR

        /// <summary> 加载Excel文件</summary>
        public override void LoadExcelFile(bool save = true)
        {
           if(Data == null){
              Data=new();
           }
           Data.LoadExcelFile(ExcelPath);
           if(save){
              SaveConfig();
           } else {
               EditorUtility.SetDirty(this);
           }
        }

#endif
    }
}

