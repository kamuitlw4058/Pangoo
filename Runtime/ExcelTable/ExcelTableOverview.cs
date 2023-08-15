using Sirenix.OdinInspector;
using UnityEngine;
using System;
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
        public  PackageConfig Config {get;set;}


        public const string ExcelDirPath =  "StreamRes/ExcelTable/Excel";
        [ShowInInspector]
        public string Namespace {
            get{
                return Config.MainNamespace;
            }
        }

        public virtual ExcelTableBase Table{
            get{
                return null;
            }
        }

        public virtual Type GetDataType()
        {
            return Table.GetType();
        }

        public virtual ExcelTableBase GetExcelTableBase()
        {
            return null;
        }

        public virtual string GetName()
        {
            //类型名是表名去掉结尾的Table
            var name = Table.GetType().Name;
            return name.Substring(0,name.Length -5);
        }

        [ShowInInspector]
        public string Name{
            get{
                return GetName();
            }
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

        
        [Button("生成Excel文件",30)]
        public virtual void BuildExcelFile()
        {
             Table.BuildExcelFile(ExcelPath);
        }

        public string ExcelPath{
            get{
                string excelDirPath = Path.Join(Config.PackageDir,ExcelDirPath);
                return  excelDirPath+ "/" + this.name + ".xlsx";
            }
        }
#endif
    }
}

