using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Excel;
using LitJson;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public static class DataTableCodeGenerator
    {
        public static string BuildTableCodeJson(ExcelTableData ExcelData, int headCount = 3)
        {

            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();

            writer.WritePropertyName(ExcelData.ClassBaseName);
            writer.WriteArrayStart();
            writer.WriteObjectStart();

            for (int i = 0; i < ExcelData.Cols; i++)
            {
                writer.WritePropertyName(ExcelData.NameList[i]);
                writer.Write(ExcelData.TypeList[i]);
            }
            writer.WriteObjectEnd();
            writer.WriteArrayEnd();

            writer.WriteObjectEnd();

            return sb.ToString();
        }

        public static string BuildCodeJson(string excelFile, string name, int headCount = 3)
        {
            var ExcelData = ExcelTableData.Parser(excelFile, name, headCount);

            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();

            for (int i = 0; i < ExcelData.Cols; i++)
            {
                writer.WritePropertyName(ExcelData.NameList[i]);
                writer.Write(ExcelData.TypeList[i]);
            }

            writer.WriteObjectEnd();

            return sb.ToString();
        }

        public static string BuildTableOverviewCodeJson(ExcelTableData excelData, string name)
        {
            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();

            for (int i = 0; i < excelData.Cols; i++)
            {
                writer.WritePropertyName(excelData.NameList[i]);
                writer.Write(excelData.TypeList[i]);
            }

            writer.WriteObjectEnd();

            return sb.ToString();
        }

        public static string BuildOverviewCodeJson(ExcelTableData excelData, string name)
        {
            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();

            for (int i = 0; i < excelData.Cols; i++)
            {
                writer.WritePropertyName(excelData.NameList[i]);
                writer.Write(excelData.TypeList[i]);
            }

            writer.WriteObjectEnd();

            return sb.ToString();
        }

    }
}