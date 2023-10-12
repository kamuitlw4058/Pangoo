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
            "System.Xml.Serialization"
        };


#if UNITY_EDITOR
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

        void InitDir(PackageConfig config, ref ExcelDirInfo entry)
        {
            var scriptDir = Path.Join(config.PackageDir, config.ScriptsMainDir, ModuleName).Replace("\\", "/");
            var scriptGenerateDir = Path.Join(scriptDir, "Generate").Replace("\\", "/");
            var scriptCustomDir = Path.Join(scriptDir, "Custom").Replace("\\", "/");
            var scriptOverviewDir = Path.Join(scriptDir, "Overview").Replace("\\", "/");
            var streamResDir = Path.Join(config.PackageDir, config.StreamResDir, ModuleName).Replace("\\", "/");
            var moduleRelativeDir = Path.Join(config.StreamResDir, ModuleName).Replace("\\", "/");
            var jsonRelativeDir = Path.Join(moduleRelativeDir, "Json").Replace("\\", "/");
            var jsonDir = Path.Join(streamResDir, "Json").Replace("\\", "/");
            var excelDir = Path.Join(streamResDir, "Excel").Replace("\\", "/");
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
            var pangooList = new List<string>();
            foreach (var filePath in files)
            {
                var regularFilePath = filePath.Replace("\\", "/");
                var fileName = Path.GetFileNameWithoutExtension(regularFilePath);
                if (!fileName.StartsWith("~"))
                {
                    if (ExcelList.Find(o => o.ExcelName == fileName) == null)
                    {
                        var namesapce = string.Empty;
                        var IsPangooTable = false;
                        if (GameSupportEditorUtility.GetExcelTableNameInPangoo(fileName) && PackConfig.MainNamespace != "Pangoo")
                        {
                            namesapce = "Pangoo";
                            IsPangooTable = true;
                            pangooList.Add(GameSupportEditorUtility.GetExcelTablePangooTableName(fileName));
                        }

                        if (PackConfig.MainNamespace == "Pangoo")
                        {
                            IsPangooTable = true;
                        }

                        ExcelList.Add(new ExcelEntry()
                        {
                            ExcelName = fileName,
                            BaseNamespace = namesapce,
                            IsPangooTable = IsPangooTable,
                            Named = true,
                        });
                    }

                }
            }

            if (PackConfig.MainNamespace != "Pangoo")
            {
                GameSupportEditorUtility.GetExcelTablePangooTableNames().ForEach(o =>
                {
                    if (!pangooList.Contains(o))
                    {
                        ExcelList.Add(new ExcelEntry()
                        {
                            ExcelName = o.Substring(7, o.Length - (5 + 7)),
                            BaseNamespace = "Pangoo",
                            IsPangooTable = true,
                            Named = true,
                        });
                    }
                });
            }




        }

        [FoldoutGroup("生成文件或SO")]
        [Button("Excel生成Table代码", 30)]
        public void ExcelBuildTableCode()
        {
            Debug.Log($"Start ExcelBuildTableCode");
            InitDirInfo();
            Debug.Log($"Start Iter ExcelList!");
            foreach (var entry in ExcelList)
            {
                if (!string.IsNullOrEmpty(entry.BaseNamespace) && entry.BaseNamespace != Namespace && Namespace != "Pangoo")
                {
                    Debug.Log($"entry :{entry.ExcelName} skip");
                    continue;
                }

                var excelFilePath = Path.Join(DirInfo.ExcelDir, $"{entry.ExcelName}.xlsx").Replace("\\", "/");
                Debug.Log($"Start Build:{excelFilePath}");

                var classBaseName = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}");
                var className = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}Table");
                ExcelTableData ExcelData = ExcelTableData.ParserEPPlus(excelFilePath, classBaseName);

                GeneratorCode(ExcelData, className, entry.Named);
            }
            AssetDatabase.Refresh();
        }

        [FoldoutGroup("生成文件或SO")]
        [Button("Excel生成SO", 30)]
        public void ExcelBuildOverviewSo()
        {
            InitDirInfo();
            foreach (ExcelEntry excelEntry in ExcelList)
            {
                var className = ($"{excelEntry.ExcelName}Table");
                // Debug.Log($"尝试创建:{className}");
                var classNamesapce = string.IsNullOrEmpty(excelEntry.BaseNamespace) ? Namespace : excelEntry.BaseNamespace;
                ExcelTableOverview so;
                var path = PathUtility.Join(DirInfo.ScriptableObjectDir, $"{excelEntry.ExcelName}.asset");
                if (File.Exists(path))
                {
                    so = AssetDatabaseUtility.LoadAssetAtPath<ExcelTableOverview>(path);
                }
                else
                {
                    so = ScriptableObject.CreateInstance($"{classNamesapce}.{className}Overview") as ExcelTableOverview;
                    AssetDatabase.CreateAsset(so, path);
                }
                so.Config = PackConfig;
                so.LoadExcelFile(false);
                Debug.Log($"创建SO成功：{path}");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [FoldoutGroup("生成文件或SO")]
        [Button("生成Excel文件", 30)]
        public void BuildExcelFile()
        {
            foreach (ExcelTableOverview excelTableOverviewSo in AssetDatabaseUtility.FindAsset<ExcelTableOverview>(PackConfig.PackageDir))
            {
                excelTableOverviewSo.BuildExcelFile();
            }
        }

        [FoldoutGroup("打开或定位文件夹")]
        [Button("打开Excel文件夹", 30)]
        public void OpenExcelFileDir()
        {
            var filePath = DirInfo.ExcelDir;
            //EditorUtility.RevealInFinder(filePath);
            System.Diagnostics.Process.Start("explorer.exe", Path.GetFullPath(filePath));
        }
        [FoldoutGroup("打开或定位文件夹")]
        [Button("定位SO文件夹", 30)]
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
        public void GeneratorCode(ExcelTableData ExcelData, string className, bool named)
        {
            var codeJson = DataTableCodeGenerator.BuildTableCodeJson(ExcelData);
            if (codeJson != null)
            {
                var codePath = Path.Join(DirInfo.ScriptGenerateDir, $"{className}.cs");
                JsonClassGenerator.GeneratorCodeString(codeJson, Namespace, new CSharpCodeWriter(UsingNamespace, ExcelData, named), className, codePath);
                // AssetDatabase.ImportAsset(codePath);

                var codeCustomPath = Path.Join(DirInfo.ScriptCustomDir, $"{className}.Custom.cs");
                if (!File.Exists(codeCustomPath))
                {
                    JsonClassGenerator.GeneratorCodeString("{}", Namespace, new CSharpCodeCustomWriter(UsingNamespace, ExcelData), className, codeCustomPath);
                    // AssetDatabase.ImportAsset(codeCustomPath);
                }

                var overviewPath = Path.Join(DirInfo.ScriptOverviewDir, $"{className}Overview.cs");
                JsonClassGenerator.GeneratorCodeString("{}", Namespace, new CSharpCodeTableOverviewWriter(UsingNamespace, ExcelData), className, overviewPath);
                // AssetDatabase.ImportAsset(overviewPath);

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
        [TableTitleGroup("操作")]
        [HideLabel]
        [TableColumnWidth(60, resizable: false)]
        public bool Build = true;

        [HideInTables]
        public bool IsPangooTable;

        [ReadOnly]
        public string ExcelName;

        [ValueDropdown("GetNamespaces")]
        [EnableIf("@!this.IsPangooTable")]
        public string BaseNamespace;

        [TableColumnWidth(60, resizable: false)]
        public bool Named = false;

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