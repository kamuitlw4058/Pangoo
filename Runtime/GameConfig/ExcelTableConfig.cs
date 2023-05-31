using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif

namespace Pangoo
{

    [CreateAssetMenu(fileName = "ExcelTable", menuName = "Pangoo/ExcelTable", order = 0)]
    public class ExcelTableConfig : GameConfigBase
    {
        const string ModuleName = "ExcelTable";

        public PackageConfig PackConfig;
        [ShowInInspector]
        [LabelText("语言版本")]
        public string Lang
        {
            get
            {
                if (PackConfig == null)
                {
                    return string.Empty;
                }
                return PackConfig.Lang;
            }
        }


        [ShowInInspector]
        [LabelText("命名空间")]
        public string Namespace
        {
            get
            {
                if (PackConfig == null)
                {
                    return string.Empty;
                }
                return PackConfig.MainNamespace;
            }
        }

        public ExcelDirInfo DirInfo = null;

        [TableList]
         public List<ExcelEntry> ExcelList = new List<ExcelEntry>();
         
        
        [FormerlySerializedAs("Headers")]
        public List<string> UsingNamespace = new List<string>()
        {
            "System",
            "System.IO",
            "System.Collections.Generic",
            "LitJson",
            "Pangoo",
            "UnityEngine",
            "Sirenix.OdinInspector",
        };

        void InitDir(PackageConfig config, ref ExcelDirInfo entry)
        {
            var scriptDir = Path.Join(config.PackageDir, config.ScriptsMainDir, ModuleName).Replace("\\", "/");
            var scriptGenerateDir = Path.Join(scriptDir, "Generate").Replace("\\", "/");
            var scriptCustomDir = Path.Join(scriptDir, "Custom").Replace("\\", "/");
            var scriptOverviewDir = Path.Join(scriptDir, "Overview").Replace("\\", "/");
            var streamResDir = Path.Join(config.PackageDir, config.StreamResDir, ModuleName).Replace("\\", "/");
            var moduleRelativeDir = Path.Join( config.StreamResDir, ModuleName).Replace("\\", "/");
            var jsonRelativeDir = Path.Join(moduleRelativeDir, "Json",Lang).Replace("\\", "/");
            var jsonDir = Path.Join(streamResDir, "Json", Lang).Replace("\\", "/");
            var excelDir = Path.Join(streamResDir, "Excel", Lang).Replace("\\", "/");
            var scriptableObjectDir = Path.Join(streamResDir, "ScriptableObject").Replace("\\", "/");

            DirectoryUtility.ExistsOrCreate(scriptDir);
            DirectoryUtility.ExistsOrCreate(scriptGenerateDir);
            DirectoryUtility.ExistsOrCreate(scriptCustomDir);
            DirectoryUtility.ExistsOrCreate(scriptOverviewDir);

            DirectoryUtility.ExistsOrCreate(streamResDir);
            DirectoryUtility.ExistsOrCreate(jsonDir);
            DirectoryUtility.ExistsOrCreate(excelDir);
            DirectoryUtility.ExistsOrCreate(scriptableObjectDir);

            entry.PackageDir = config.PackageDir;

            entry.ScriptDir = scriptDir;
            entry.ScriptCustomDir = scriptCustomDir;
            entry.ScriptGenerateDir = scriptGenerateDir;
            entry.ScriptOverviewDir = scriptOverviewDir;

            entry.StreamResDir = streamResDir;
            entry.JsonRelativeDir = jsonRelativeDir;
            entry.JsonDir = jsonDir;
            entry.ExcelDir = excelDir;
            entry.ScriptableObjectDir = scriptableObjectDir;

        }

        void InitDirInfo()
        {
            if (PackConfig == null)
            {
                Debug.LogError("Load Config Failed!");
                return;
            }

            if (DirInfo == null)
            {
                DirInfo = new ExcelDirInfo();
            }
            InitDir(PackConfig, ref DirInfo);

        }
#if UNITY_EDITOR
        [Button("刷新Excel列表", 30)]
        void Refresh()
        {
            if (PackConfig == null)
            {
                Debug.LogError("Load Config Failed!");
                return;
            }

            InitDirInfo();
            ExcelList.Clear();
            DirInfo.NameSpace = PackConfig.MainNamespace;
            var files = Directory.GetFiles(DirInfo.ExcelDir, "*.xlsx");
            foreach (var filePath in files)
            {
                var regularFilePath = filePath.Replace("\\", "/");
                var fileName = Path.GetFileNameWithoutExtension(regularFilePath);
                if (!fileName.StartsWith("~"))
                {
                    if (ExcelList.Find(o => o.ExcelName == fileName) == null)
                    {
                        ExcelList.Add(new ExcelEntry()
                        {
                            ExcelName = fileName,
                            BaseNamespace = string.Empty,
                        });
                    }

                }
            }
        }

        [FoldoutGroup("生成文件或SO")]
        [Button("Excel生成Table代码", 30)]
        public void ExcelBuildTableCode()
        {
            InitDirInfo();
            foreach (var entry in ExcelList)
            {
                if (!string.IsNullOrEmpty(entry.BaseNamespace) && entry.BaseNamespace != Namespace)
                {
                    continue;
                }
                
                var excelFilePath = Path.Join(DirInfo.ExcelDir, $"{entry.ExcelName}.xlsx").Replace("\\", "/");
                Debug.Log($"Start Build:{excelFilePath}");
                
                var classBaseName = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}");
                var className = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}Table");
                ExcelTableData ExcelData = ExcelTableData.ParserEPPlus(excelFilePath, classBaseName);

                GeneratorCode(ExcelData,className);
            }
            AssetDatabase.Refresh();
        }
        
        [FoldoutGroup("生成文件或SO")]
        [Button("Excel生成SO",30)]
        public void ExcelBuildOverviewSo()
        {
            InitDirInfo();
            foreach (ExcelEntry excelEntry in ExcelList)
            {
                var className = ($"{excelEntry.ExcelName}Table");
                var classNamesapce = string.IsNullOrEmpty(excelEntry.BaseNamespace) ? Namespace : excelEntry.BaseNamespace;
                ExcelTableOverview so = ScriptableObject.CreateInstance($"{classNamesapce}.{className}Overview") as ExcelTableOverview;
                var path = Path.Join(DirInfo.ScriptableObjectDir, $"{excelEntry.ExcelName}.asset");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                so.Namespace = Namespace;
                so.PackageDir = PackConfig.PackageDir;
                so.ExcelDirPath ="StreamRes/ExcelTable/Excel/"+PackConfig.Lang;
                AssetDatabase.CreateAsset(so, path);
                AssetDatabase.SaveAssets();
                so.LoadExcelFile();
                AssetDatabase.Refresh();
                Debug.Log($"创建SO成功：{path}");
            }
        }
        
        [FoldoutGroup("生成文件或SO")]
        [Button("生成Excel文件",30)]
        public void BuildExcelFile()
        {
            string[] dirPath = new string[] { PackConfig.PackageDir};
            foreach (ExcelTableOverview excelTableOverviewSo in AssetDatabaseUtility.FindAsset<ExcelTableOverview>(dirPath))
            {
                excelTableOverviewSo.BuildExcelFile();
            }
        }
        
        [FoldoutGroup("打开或定位文件夹")]
        [Button("打开Excel文件夹",30)]
        public void OpenExcelFileDir()
        {
            var filePath = DirInfo.ExcelDir;
            //EditorUtility.RevealInFinder(filePath);
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(filePath));
        }
        [FoldoutGroup("打开或定位文件夹")]
        [Button("定位SO文件夹",30)]
        public void OpenSODir()
        {
            var filePath = DirInfo.ScriptableObjectDir;
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(filePath);
            Selection.activeObject = obj;
        }
        
        /// <summary>
        /// 根据ExcelData数据生成代码
        /// </summary>
        /// <param name="ExcelData">传入的数据</param>
        /// <param name="className">生成的脚本名</param>
        public void GeneratorCode(ExcelTableData ExcelData,string className)
        {
            var codeJson = DataTableCodeGenerator.BuildTableCodeJson(ExcelData);
            if (codeJson != null)
            {
                var codePath = Path.Join(DirInfo.ScriptGenerateDir, $"{className}.cs");
                JsonClassGenerator.GeneratorCodeString(codeJson, Namespace, new CSharpCodeWriter(UsingNamespace, ExcelData), className, codePath);
                var codeCustomPath = Path.Join(DirInfo.ScriptCustomDir, $"{className}.Custom.cs");
                if (!File.Exists(codeCustomPath))
                {
                    JsonClassGenerator.GeneratorCodeString("{}", Namespace, new CSharpCodeCustomWriter(UsingNamespace, ExcelData), className, codeCustomPath);
                }
                var overviewPath = Path.Join(DirInfo.ScriptOverviewDir, $"{className}Overview.cs");
                JsonClassGenerator.GeneratorCodeString("{}", Namespace, new CSharpCodeTableOverviewWriter(UsingNamespace, ExcelData), className, overviewPath);
                Debug.Log($"Build Class:{className}");
            }
        }

#endif
    }

    [Serializable]
    public class ExcelDirInfo
    {
        public string PackageDir;
        public string NameSpace;

        public string ScriptDir;

        public string ScriptGenerateDir;

        public string ScriptCustomDir;

        public string ScriptOverviewDir;

        public string StreamResDir;


        public string JsonRelativeDir;
        public string JsonDir;

        [FolderPath]
        public string ExcelDir;

        public string ScriptableObjectDir;

    }

    [Serializable]
    public class ExcelEntry
    {
        public string ExcelName;

        [ValueDropdown("GetNamespaces")]
        public string BaseNamespace;


        public bool LoadAtRuntime = true;

#if UNITY_EDITOR
        IEnumerable GetNamespaces()
        {
            return GameSupportEditorUtility.GetNamespaces();

        }

#endif
    }
    
    [Serializable]
    public class CSVEntry
    {
        public string CSVName;

        [ValueDropdown("GetNamespaces")]
        public string BaseNamespace;


        public bool LoadAtRuntime = true;

#if UNITY_EDITOR
        IEnumerable GetNamespaces()
        {
            return GameSupportEditorUtility.GetNamespaces();

        }

#endif
    }
}