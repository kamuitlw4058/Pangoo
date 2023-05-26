using Sirenix.OdinInspector;
using UnityEngine;
using LitJson;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using System.IO;
using System.Text;
using UnityEditor;
using OfficeOpenXml;
#endif

namespace Pangoo
{
    public abstract class ExcelTableOverview : GameConfigBase
    {
        [ShowInInspector]
        [FolderPath]
        public  string PackageDir {get;set;}
        [FormerlySerializedAs("csvDirPath")] [FolderPath(ParentFolder = "$PackageDir")]
        public string ExcelDirPath;
        [ShowInInspector]
        public string Namespace { get; set; }

        public virtual Type GetDataType()
        {
            return GetType();
        }

        public virtual ExcelTableBase GetExcelTableBase()
        {
            return null;
        }
        public virtual int GetRowCount()
        {
            return 0;
        }

        public virtual string GetName()
        {
            return this.GetType().Name;
        }

        public virtual string GetJsonPath()
        {
            return "";
        }
        
        
        


#if UNITY_EDITOR


        public virtual void LoadFromJson()
        {

        }

        public virtual void SaveJson()
        {
        }

        public virtual void SaveExcel()
        {
            
        }

        public virtual void LoadExcelFile()
        {
            
        }

        
        
        public virtual void BuildExcelFile()
        {
            
        }
        
        public  void BuildExcelFile(ExcelTableBase Data)
        {
            string excelDirPath = Path.Join(PackageDir,ExcelDirPath);
            string outExcelFilePath = excelDirPath+ "/" + this.name + ".xlsx";
            string sheetName = "Sheet1";

            VerifyCSVDirectory(excelDirPath);
            CreateTableHeadToFile(outExcelFilePath,Data.GetTableHeadList(),sheetName);
            AppendTableDataToFile(outExcelFilePath,Data.GetTableRowDataList(),sheetName);
            
            AssetDatabase.Refresh();
            Debug.Log($"Excel文件创建成功:{outExcelFilePath}");
        }
        
        /// <summary>
        /// 验证CSV文件夹
        /// </summary>
        public void VerifyCSVDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// 创建含有表头的文件
        /// </summary>
        /// <param name="filePath">输出的文件路径</param>
        /// <param name="tableHeadList">表头列表</param>
        public void CreateTableHeadToFile(string filePath,List<string[]> tableHeadList,string sheetName)
        {
            //-------------------------------
            WriteTextToExcel(filePath,tableHeadList,sheetName,true);
        }
        /// <summary>
        /// 追加数据到文件
        /// </summary>
        /// <param name="filePath">追加的文件路径</param>
        /// <param name="tableRowDataList">每行数据</param>
        public void AppendTableDataToFile(string filePath,List<string[]> tableRowDataList,string sheetName)
        {
            //--------------------------------
            WriteTextToExcel(filePath,tableRowDataList,sheetName);
        }
        /// <summary>
        /// 向文件流写入文本
        /// </summary>
        /// <param name="sw">指定文件流</param>
        /// <param name="RowTextList">写入的行文本</param>
        private void WriteTextToStreamWriter(StreamWriter sw,List<string[]> RowTextList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var tableHead in RowTextList)
            {
                string finalString = AssetDatabaseUtility.CombiningStrings(tableHead,",");
                sb.AppendLine(finalString);
            }
            sw.Write(sb.ToString());
        }

        private void WriteTextToExcel(string filePath,List<string[]> RowTextList,string sheetName,bool isCreate=false)
        {
            var fileInfo = new FileInfo(filePath);
            using (ExcelPackage excelPackage=new ExcelPackage(fileInfo))
            {
                //添加一张表格
                ExcelWorksheet worksheet;
                if (isCreate)
                {
                    try
                    {
                        worksheet = excelPackage.Workbook.Worksheets.Add(sheetName);
                    }
                    catch (Exception e)
                    {
                        worksheet = excelPackage.Workbook.Worksheets[sheetName];
                    }
                }
                else
                {
                    worksheet = excelPackage.Workbook.Worksheets[sheetName];
                }
                
                for (int row = 0; row < RowTextList.Count; row++)
                {
                    for (int col = 0; col < RowTextList[row].Length; col++)
                    {
                        if (isCreate)
                        {
                            worksheet.Cells[row+1, col+1].Value = RowTextList[row][col];
                        }
                        else
                        {
                            worksheet.Cells[row+4, col+1].Value = RowTextList[row][col];
                        }
                    }
                }
                excelPackage.Save();//写入后保存表格
            }
        }
#endif
    }
}

