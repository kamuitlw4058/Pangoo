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
    public partial class PangooEventsTableOverview : ExcelTableOverview
    {


         [TableList(IsReadOnly = true, AlwaysExpanded = true)]
       public PangooEventsTable Data;

       public override ExcelTableBase GetExcelTableBase()
       {
           return CopyUtility.Clone<PangooEventsTable>(Data);
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
           return "PangooEventsTable";
       }


       public override string GetName()
       {
           return "PangooEvents";
       }

#if UNITY_EDITOR

       [Button("从Json重构",30)]
       public override void LoadFromJson()
       {
          var path = $"{PackageDir}/StreamRes/ExcelTable/Json/cn/{GetJsonPath()}.json";
          string json = File.ReadAllText(path);
          Data =  JsonMapper.ToObject<PangooEventsTable>(json);
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
          string[] CSVDirPath = new string[] { PackageDir, csvDirPath };
          string outCSVPath = AssetDatabaseUtility.CombiningStrings(CSVDirPath, "/") + "/" + this.name + ".csv";
          tableHeadList = new List<string[]>();
          tableRowDataList = new List<string[]>();
          tableHeadList.Add(Data.GetHeadNames());
          tableHeadList.Add(Data.GetTypeNames());
          tableHeadList.Add(Data.GetDescNames());
          base.VerifyCSVDirectory();
          base.CreateTableHeadToFile(outCSVPath, tableHeadList);
          foreach (var item in Data.Rows)
          {
              string[] texts = new string[item.GetType().GetFields().Length];
              for (int i = 0; i < texts.Length; i++)
              {
                  texts[i] = item.GetType().GetFields()[i].GetValue(item).ToString();
              }
              tableRowDataList.Add(texts);
          }
        base.AppendTableDataToFile(outCSVPath, tableRowDataList);
        AssetDatabase.Refresh();
        }

#endif
    }
}

