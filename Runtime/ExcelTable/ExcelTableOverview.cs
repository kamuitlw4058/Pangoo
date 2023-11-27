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
        public PackageConfig Config;


        public const string ExcelDirPath = "StreamRes/ExcelTable/Excel";
        [ShowInInspector]
        public string Namespace
        {
            get
            {
                if (Config != null)
                {
                    return Config.MainNamespace;
                }
                return null;
            }
        }

        public virtual ExcelTableBase Table
        {
            get
            {
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
            return name.Substring(0, name.Length - 5);
        }

        [ShowInInspector]
        public string Name
        {
            get
            {
                return GetName();
            }
        }

        public virtual string GetJsonPath()
        {
            return "";
        }






#if UNITY_EDITOR

        public int GetMaxId()
        {
            int ret = 0;
            foreach (var row in Table.BaseRows)
            {
                if (row.Id > ret)
                {
                    ret = row.Id;
                }
            }

            return ret;
        }

        public int GetNextId(int offset = 1)
        {
            return GetMaxId() + 1;
        }


        public virtual void LoadFromJson()
        {

        }

        public virtual void SaveJson()
        {
        }

        public virtual void SaveExcel()
        {

        }

        [Button("从Excel文件重构数据", 30)]
        public void LoadExcelFile()
        {
            LoadExcelFile(true);
        }


        public virtual void LoadExcelFile(bool save = true)
        {
        }


        [Button("选择Excel资源", 30)]
        public void SelectExcelFile()
        {
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(ExcelPath);
            if (obj != null)
            {
                Selection.activeObject = obj;
            }
        }


        [Button("生成Excel文件", 30)]
        public virtual void BuildExcelFile()
        {
            Table.BuildExcelFile(ExcelPath);
        }

        public string ExcelPath
        {
            get
            {
                string excelDirPath = PathUtility.Join(Config.PackageDir, ExcelDirPath);
                return excelDirPath + "/" + this.name + ".xlsx";
            }
        }
#endif
    }
}

