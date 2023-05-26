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
    public partial class StaticSceneTableOverview : ExcelTableOverview
    {


         [TableList(IsReadOnly = true, AlwaysExpanded = true)]
       public StaticSceneTable Data;

       public override ExcelTableBase GetExcelTableBase()
       {
           return CopyUtility.Clone<StaticSceneTable>(Data);
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
           return "StaticSceneTable";
       }


       public override string GetName()
       {
           return "StaticScene";
       }

#if UNITY_EDITOR

       [Button("从Json重构",30)]
       public override void LoadFromJson()
       {
          var path = $"{PackageDir}/StreamRes/ExcelTable/Json/cn/{GetJsonPath()}.json";
          string json = File.ReadAllText(path);
          Data =  JsonMapper.ToObject<StaticSceneTable>(json);
       }


       [Button("生成Json",30)]
       public override void SaveJson()
       {
          var path = $"{PackageDir}/StreamRes/ExcelTable/Json/cn/{GetJsonPath()}.json";
          var json = JsonMapper.ToJson(Data);
          using (var sw = new StreamWriter(path))
           {
              sw.WriteLine(json);
           }
           SaveConfig();
       }



#endif
    }
}

