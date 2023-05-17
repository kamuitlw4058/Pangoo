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
    public partial class UiConfigInfoTableOverview : ExcelTableOverview
    {


         [TableList(IsReadOnly = true, AlwaysExpanded = true)]
       public UiConfigInfoTable Data;

       public override ExcelTableBase GetExcelTableBase()
       {
           return CopyUtility.Clone<UiConfigInfoTable>(Data);
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
           return "UiConfigInfoTable";
       }


       public override string GetName()
       {
           return "UiConfigInfo";
       }

#if UNITY_EDITOR

       [Button("从Json重构",30)]
       public override void LoadFromJson()
       {
          var path = $"{PackageDir}/StreamRes/ExcelTable/Json/cn/{GetJsonPath()}.json";
          string json = File.ReadAllText(path);
          Data =  JsonMapper.ToObject<UiConfigInfoTable>(json);
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


       [Button("生成CSV文件",30)]
        /// <summary> 生成CSV文件</summary>
        public override void BuildCSVFile()
        {
          base.VerifyCSVDirectory();
          base.CreateFile(Data.GetHeadNames(),this.name);
          base.AppendToFile(Data.GetTypeNames(),this.name);
          base.AppendToFile(Data.GetDescNames(),this.name);
          foreach (var item in Data.Rows)
          {
              string[] texts = new string[item.GetType().GetFields().Length];
              for (int i = 0; i < texts.Length; i++)
              {
                  texts[i] = item.GetType().GetFields()[i].GetValue(item).ToString();
              }
              base.AppendToFile(texts,this.name);
          }
        AssetDatabase.Refresh();
        }

#endif
    }
}

