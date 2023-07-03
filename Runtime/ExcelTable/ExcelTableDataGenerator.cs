using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using LitJson;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public static class DataTableDataGenerator
    {
        public static string BuildTableDataJson(ExcelTableData excelData)
        {

            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();
            writer.WritePropertyName(excelData.ClassBaseName);
            writer.WriteArrayStart();
            //读取数据
            for (int i = excelData.HeadCount; i < excelData.Rows; i++)
            {
                if (!string.IsNullOrEmpty(excelData.DataTable.Rows[i][0].ToString()))
                {
                    Debug.Log("输出:"+excelData.DataTable.Rows[i][0].ToString());
                    //准备一个字典存储每一行的数据
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    writer.WriteObjectStart();

                    for (int j = 0; j < excelData.Cols; j++)
                    {
                        //读取第1行数据作为表头字段
                        string field = excelData.NameList[j].ToString();
                        string type = excelData.TypeList[j].ToString().ToLower();

                        if (!string.IsNullOrEmpty(field))
                        {
                            writer.WritePropertyName(field);
                            switch (type)
                            {
                                case "int":
                                    var valStr = excelData.DataTable.Rows[i][j].ToString();
                                    if (string.IsNullOrEmpty(valStr))
                                    {
                                        writer.Write(0);
                                    }
                                    else
                                    {
                                        valStr = valStr.Trim('[').Trim(']');
                                        writer.Write(Convert.ToInt32(valStr));
                                    }
                                    break;
                                case "string":
                                    writer.Write(Convert.ToString(excelData.DataTable.Rows[i][j]));
                                    break;
                                case "bool":
                                    if (string.IsNullOrEmpty(excelData.DataTable.Rows[i][j].ToString()))
                                    {
                                        writer.Write(false);
                                    }
                                    else
                                    {
                                        writer.Write(Convert.ToBoolean(excelData.DataTable.Rows[i][j]));
                                    }
                                    break;
                                case "float":
                                case "double":
                                    if (string.IsNullOrEmpty(excelData.DataTable.Rows[i][j].ToString()))
                                    {
                                        writer.Write(0.0f);
                                    }
                                    else
                                    {
                                        writer.Write(Convert.ToDouble(excelData.DataTable.Rows[i][j]));
                                    }
                                    break;
                            }

                        }

                    }
                    writer.WriteObjectEnd();
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
            return sb.ToString();
        }
        public static string BuildCSVTableDataJson(ExcelTableData excelData)
        {

            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);
            writer.WriteObjectStart();
            writer.WritePropertyName(excelData.ClassBaseName);
            writer.WriteArrayStart();
            //读取数据
            for (int i = excelData.HeadCount; i < excelData.Rows; i++)
            {
                if (!string.IsNullOrEmpty(excelData.result[i][0].ToString()))
                {
                    //Debug.Log(mSheet.Rows[i][0].ToString());
                    //准备一个字典存储每一行的数据
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    writer.WriteObjectStart();

                    for (int j = 0; j < excelData.Cols; j++)
                    {
                        //读取第1行数据作为表头字段
                        string field = excelData.NameList[j].ToString();
                        string type = excelData.TypeList[j].ToString().ToLower();

                        if (!string.IsNullOrEmpty(field))
                        {
                            writer.WritePropertyName(field);
                            switch (type)
                            {
                                case "int":
                                    var valStr = excelData.result[i][j].ToString();
                                    if (string.IsNullOrEmpty(valStr))
                                    {
                                        writer.Write(0);
                                    }
                                    else
                                    {
                                        valStr = valStr.Trim('[').Trim(']');
                                        writer.Write(Convert.ToInt32(valStr));
                                    }
                                    break;
                                case "string":
                                    writer.Write(Convert.ToString(excelData.result[i][j]));
                                    break;
                                case "bool":
                                    if (string.IsNullOrEmpty(excelData.result[i][j].ToString()))
                                    {
                                        writer.Write(false);
                                    }
                                    else
                                    {
                                        writer.Write(Convert.ToBoolean(excelData.result[i][j]));
                                    }
                                    break;
                                case "float":
                                case "double":
                                    if (string.IsNullOrEmpty(excelData.result[i][j].ToString()))
                                    {
                                        writer.Write(0.0f);
                                    }
                                    else
                                    {
                                        writer.Write(Convert.ToDouble(excelData.result[i][j]));
                                    }
                                    break;
                            }

                        }

                    }
                    writer.WriteObjectEnd();
                }
            }
            writer.WriteArrayEnd();
            writer.WriteObjectEnd();
            return sb.ToString();
        }

        public static string BuildDataJson(ExcelTableData excelData, int headCount = 3)
        {

            StringBuilder sb = new StringBuilder();
            JsonWriter writer = new JsonWriter(sb);

            writer.WriteArrayStart();
            //读取数据
            for (int i = headCount; i < excelData.Rows; i++)
            {
                if (!string.IsNullOrEmpty(excelData.DataTable.Rows[i][0].ToString()))
                {
                    //Debug.Log(mSheet.Rows[i][0].ToString());
                    //准备一个字典存储每一行的数据
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    writer.WriteObjectStart();

                    for (int j = 0; j < excelData.Cols; j++)
                    {
                        //读取第1行数据作为表头字段
                        string field = excelData.NameList[j].ToString();
                        string type = excelData.TypeList[j].ToString().ToLower();

                        if (!string.IsNullOrEmpty(field))
                        {
                            writer.WritePropertyName(field);
                            switch (type)
                            {
                                case "int":
                                    var valStr = excelData.DataTable.Rows[i][j].ToString();
                                    if (string.IsNullOrEmpty(valStr))
                                    {
                                        writer.Write(0);
                                    }
                                    else
                                    {
                                        valStr = valStr.Trim('[').Trim(']');
                                        writer.Write(Convert.ToInt32(valStr));
                                    }
                                    break;
                                case "string":
                                    writer.Write(Convert.ToString(excelData.DataTable.Rows[i][j]));
                                    break;
                                case "bool":
                                    if (string.IsNullOrEmpty(excelData.DataTable.Rows[i][j].ToString()))
                                    {
                                        writer.Write(false);
                                    }
                                    else
                                    {
                                        writer.Write(Convert.ToBoolean(excelData.DataTable.Rows[i][j]));
                                    }
                                    break;
                                case "float":
                                case "double":
                                    if (string.IsNullOrEmpty(excelData.DataTable.Rows[i][j].ToString()))
                                    {
                                        writer.Write(0.0f);
                                    }
                                    else
                                    {
                                        writer.Write(Convert.ToDouble(excelData.DataTable.Rows[i][j]));
                                    }
                                    break;
                            }

                        }

                    }
                    writer.WriteObjectEnd();
                }
            }
            writer.WriteArrayEnd();
            Debug.Log(sb.ToString());
            return sb.ToString();
        }

    }
}