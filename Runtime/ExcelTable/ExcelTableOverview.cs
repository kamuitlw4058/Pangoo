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

        public void CreateFile(string[] headStrings, string fileName)
        {
            using (StreamWriter sw = File.CreateText(PackageDir+"/"+csvDirPath + "/" + fileName + ".csv"))
            {
                string finalString = "";
                foreach (string header in headStrings)
                {
                    if (finalString != "")
                    {
                        finalString += ",";
                    }
                    finalString += header;
                }
                sw.WriteLine(finalString);
            }
        }
        
        public void AppendToFile(string[] strings,string fileName)
        {
            using (StreamWriter sw = File.AppendText(PackageDir+"/"+csvDirPath + "/" + fileName + ".csv"))
            {
                string finalString = "";
                foreach (string text in strings)
                {
                    if (finalString != "")
                    {
                        finalString += ",";
                    }
                    finalString += text;
                }
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

