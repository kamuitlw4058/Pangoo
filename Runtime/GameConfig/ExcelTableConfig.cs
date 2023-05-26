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
        
        [AssetList(AutoPopulate = true,Path = "/Plugins/Pangoo/StreamRes/ExcelTable/CSV")]
        public List<TextAsset> CSVFileList;

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


        [Button("生成ExcelTable代码", 30)]
        public void Build()
        {
            InitDirInfo();
            foreach (var entry in ExcelList)
            {
                if (!string.IsNullOrEmpty(entry.BaseNamespace) && entry.BaseNamespace != Namespace)
                {
                        }

                var excelPath = Path.Join(DirInfo.ExcelDir, $"{entry.ExcelName}.xlsx").Replace("\\", "/");
                Debug.Log($"Start Build:{excelPath}");
                var classBaseName = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}");
                var className = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}Table");
                ExcelTableData ExcelData = ExcelTableData.Parser(excelPath, classBaseName);
                var json = DataTableDataGenerator.BuildTableDataJson(ExcelData);
                GeneratorCode(ExcelData,className,json);
            }
            AssetDatabase.Refresh();
        }

        [Button("CSV生成CSVTable代码", 30)]
        public void CSVBuildCSVTableCode()
        {
            InitDirInfo();
            foreach (var entry in CSVFileList)
            {
                var csvFilePath = Path.Join(DirInfo.CSVDir,PackConfig.Lang, $"{entry.name}.csv").Replace("\\", "/");
                Debug.Log($"Start Build:{csvFilePath}");
                
                var classBaseName = JsonClassGenerator.ToTitleCase($"{entry.name}");
                var className = JsonClassGenerator.ToTitleCase($"{entry.name}Table");
                ExcelTableData ExcelData = ExcelTableData.ParserCSV(csvFilePath, classBaseName);
                 var json = DataTableDataGenerator.BuildCSVTableDataJson(ExcelData);
                GeneratorCode(ExcelData,className,json);
            }
            AssetDatabase.Refresh();
        }

        [Button("Excel生成SO",30)]
        public void BuildOverviewSo()
        {
            InitDirInfo();
            foreach (ExcelEntry excelEntry in ExcelList)
            {
                var className = JsonClassGenerator.ToTitleCase($"{excelEntry.ExcelName}Table");
                var classNamesapce = string.IsNullOrEmpty(excelEntry.BaseNamespace) ? Namespace : excelEntry.BaseNamespace;
                var so = ScriptableObject.CreateInstance($"{classNamesapce}.{className}Overview") as ExcelTableOverview;
                var path = Path.Join(DirInfo.ScriptableObjectDir, $"{excelEntry.ExcelName}.asset");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                so.Namespace = Namespace;
                so.PackageDir = PackConfig.PackageDir;
                so.csvDirPath ="StreamRes/ExcelTable/CSV/"+PackConfig.Lang;
                so.LoadFromJson();
                AssetDatabase.CreateAsset(so, path);
            }

            AssetDatabase.Refresh();
        }
        
        [Button("CSV生成SO",30)]
        public void CSVBuildOverviewSo()
        {
            InitDirInfo();
            foreach (TextAsset excelEntry in CSVFileList)
            {
                var className = ($"{excelEntry.name}Table");
                var classNamesapce = Namespace;
                ExcelTableOverview so = ScriptableObject.CreateInstance($"{classNamesapce}.{className}Overview") as ExcelTableOverview;
                var path = Path.Join(DirInfo.ScriptableObjectDir, $"{excelEntry.name}.asset");
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                so.Namespace = Namespace;
                so.PackageDir = PackConfig.PackageDir;
                so.csvDirPath ="StreamRes/ExcelTable/CSV/"+PackConfig.Lang;
                AssetDatabase.CreateAsset(so, path);
                AssetDatabase.Refresh();
                so.LoadCSVFile();
            }
        }

        [Button("从Excel生成Json", 30)]
        public void ReloadFromExcel(){
            InitDirInfo();
            foreach (var entry in ExcelList)
            {
                // if (!string.IsNullOrEmpty(entry.BaseNamespace) && entry.BaseNamespace != Namespace)
                // {
                //     continue;
                // }

                var excelPath = Path.Join(DirInfo.ExcelDir, $"{entry.ExcelName}.xlsx").Replace("\\", "/");
                Debug.Log($"Start Build:{excelPath}");
                var classBaseName = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}");
                var className = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}Table");
                var ExcelData = ExcelTableData.Parser(excelPath, classBaseName);
                var json = DataTableDataGenerator.BuildTableDataJson(ExcelData);
                var jsonPath = Path.Join(DirInfo.JsonDir, $"{className}.json").Replace("\\", "/");
                Debug.Log($"Wrte json at:{jsonPath}");
                if (json != null)
                {

                    using (FileStream fileStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                    {
                        using (TextWriter textWriter = new StreamWriter(fileStream, Encoding.UTF8))
                        {
                            textWriter.Write(json);
                        }
                    }
                }

        }}

        [Button("打开Excel文件夹",30)]
        public void OpenExcelFileDir()
        {
            var filePath = DirInfo.ExcelDir;
            //EditorUtility.RevealInFinder(filePath);
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(filePath));
        }
        
        [Button("打开CSV文件夹",30)]
        public void OpenCSVFileDir()
        {
            var filePath = DirInfo.CSVDir;
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(filePath));
        }
        
        [Button("定位SO文件夹",30)]
        public void OpenSODir()
        {
            var filePath = DirInfo.ScriptableObjectDir;
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(filePath);
            Selection.activeObject = obj;
        }
        
        
        
        [Button("生成CSV文件",30)]
        public void BuildCSVFile()
        {
            string[] dirPath = new string[] { PackConfig.PackageDir};
            //寻找到所有继承ExcelTableOverview的SO,调用其自身的build方法
            //foreach (ExcelTableOverview excelTableOverviewSo in AssetDatabaseUtility.FindAllExcelTableOverviewSO(dirPath))
            foreach (ExcelTableOverview excelTableOverviewSo in AssetDatabaseUtility.FindAsset<ExcelTableOverview>(dirPath))
            {
                excelTableOverviewSo.BuildCSVFile();
            }
        }
        /// <summary>
        /// 根据ExcelData数据生成代码
        /// </summary>
        /// <param name="ExcelData">传入的数据</param>
        /// <param name="className">生成的脚本名</param>
        public void GeneratorCode(ExcelTableData ExcelData,string className,string json)
        {
           
            var jsonPath = Path.Join(DirInfo.JsonDir, $"{className}.json").Replace("\\", "/");
            
            if (json != null)
            {
                using (FileStream fileStream = new FileStream(jsonPath, FileMode.Create, FileAccess.Write))
                {
                    using (TextWriter textWriter = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        textWriter.Write(json);
                    }
                }
            }
                
            var codeJson = DataTableCodeGenerator.BuildTableCodeJson(ExcelData);
            if (codeJson != null)
            {
                Debug.Log($"Build Class:{className}");
                var codePath = Path.Join(DirInfo.ScriptGenerateDir, $"{className}.cs");
                JsonClassGenerator.GeneratorCodeString(codeJson, Namespace, new CSharpCodeWriter(UsingNamespace, ExcelData), className, codePath, jsonPath);

                var codeCustomPath = Path.Join(DirInfo.ScriptCustomDir, $"{className}.Custom.cs");
                if (!File.Exists(codeCustomPath))
                {
                    JsonClassGenerator.GeneratorCodeString("{}", Namespace, new CSharpCodeCustomWriter(UsingNamespace, ExcelData), className, codeCustomPath);
                }

                var overviewPath = Path.Join(DirInfo.ScriptOverviewDir, $"{className}Overview.cs");
                JsonClassGenerator.GeneratorCodeString("{}", Namespace, new CSharpCodeTableOverviewWriter(UsingNamespace, ExcelData,DirInfo.PackageDir, DirInfo.JsonRelativeDir), className, overviewPath);
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
        [FolderPath]
        public string CSVDir;

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