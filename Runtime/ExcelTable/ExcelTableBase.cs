using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using OfficeOpenXml;
#endif

using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;
using Object = UnityEngine.Object;

namespace Pangoo
{
    public abstract class ExcelTableBase
    {
        public virtual void CustomInit()
        {

        }
        
        public virtual string[] GetHeadNames()
        {
            return null;
        }

        public virtual string[] GetDescNames()
        {
            return null;
        }

        public virtual string[] GetTypeNames()
        {
            return null;
        }


        public virtual void Merge(ExcelTableBase table)
        {

        }
        public virtual void GetFieldInfos()
        {
            
        }
        
        public virtual string GetFieldDesc(string field)
        {
            return field;
        }

        public virtual List<string[]> GetTableHeadList()
        {
            List<string[]> tableHeadList = new List<string[]>();
            tableHeadList.Add(GetHeadNames());
            tableHeadList.Add(GetTypeNames());
            tableHeadList.Add(GetDescNames());
            
            return tableHeadList;
        }
        public virtual List<string[]> GetTableRowDataList()
        {
            return null;
        }

        public void tmpRowDataListAdd(List<string[]> tmpRowDataList,object item)
        {
            string[] texts = new string[item.GetType().GetFields().Length];
            for (int i = 0; i < texts.Length; i++)
            {
                object valueText = item.GetType().GetFields()[i].GetValue(item);
                texts[i] = valueText != null ?valueText.ToString(): string.Empty;
            }
            tmpRowDataList.Add(texts);
        }

#if UNITY_EDITOR
        public virtual List<T> LoadExcelFile<T>(string excelFilePath)where T:new()
        {
            var Rows = new List<T>();
            var fileInfo = new FileInfo(excelFilePath);
            ExcelPackage excelPackage = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
            for (int i = 3; i < worksheet.Dimension.Rows; i++)
            {
                T  eventsRow = new T();
                var eventRowFieldInfos = eventsRow.GetType().GetFields();
                for (int j = 0; j < worksheet.Dimension.Columns; j++)
                {
                    var cellValue = worksheet.Cells[i + 1, j + 1].Value;
                    var value = StringConvert.ToValue(eventRowFieldInfos[j].FieldType, cellValue!=null ? cellValue.ToString() : string.Empty);  //将字符串解析成指定类型
                    eventRowFieldInfos[j].SetValue(eventsRow,value);
                }
                Rows.Add(eventsRow);
            }

            return Rows;
        }
#endif
    }

}
