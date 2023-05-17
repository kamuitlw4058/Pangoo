using Sirenix.OdinInspector;
using UnityEngine;
using LitJson;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Pangoo
{
    public abstract class ExcelTableOverview : GameConfigBase
    {
        [ShowInInspector]
        [FolderPath]
        public  string PackageDir {get;set;}
        [FolderPath(ParentFolder = "$PackageDir")]
        public string csvDirPath;
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
        
        
        public virtual void BuildCSVFile()
        {
            
        }
        
        /// <summary>
        /// 验证文件夹
        /// </summary>
        public void VerifyCSVDirectory()
        {
            string directory = PackageDir+"/"+csvDirPath;
            Debug.Log(directory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public List<string[]> tableHeadList=new List<string[]>();
        public List<string[]> tableRowDataList=new List<string[]>();
        
        /// <summary>
        /// 创建含有表头的文件
        /// </summary>
        /// <param name="filePath">输出的文件路径</param>
        /// <param name="tableHeadList">表头列表</param>
        public void CreateTableHeadToFile(string filePath,List<string[]>tableHeadList)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                WriteTextToStreamWriter(sw,tableHeadList);
            }
        }
        /// <summary>
        /// 追加数据到文件
        /// </summary>
        /// <param name="filePath">追加的文件路径</param>
        /// <param name="tableRowDataList">每行数据</param>
        public void AppendTableDataToFile(string filePath,List<string[]> tableRowDataList)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                WriteTextToStreamWriter(sw,tableRowDataList);
            }
        }
        /// <summary>
        /// 向文件流写入文本
        /// </summary>
        /// <param name="sw">指定文件流</param>
        /// <param name="RowTextList">写入的行文本</param>
        private void WriteTextToStreamWriter(StreamWriter sw,List<string[]> RowTextList)
        {
            foreach (var tableHead in RowTextList)
            {
                string finalString = AssetDatabaseUtility.CombiningStrings(tableHead,",");
                sw.WriteLine(finalString);
            }
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
#endif
    }
}

