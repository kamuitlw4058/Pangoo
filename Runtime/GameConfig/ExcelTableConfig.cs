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
        //CSV表头
        private List<string> csvHeader;
        [ShowInInspector]
        private List<string> csvHeaderType;
        private List<string> csvHeaderDesc;
        private string csvSeparator = ",";

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
                    continue;
                }

                var excelPath = Path.Join(DirInfo.ExcelDir, $"{entry.ExcelName}.xlsx").Replace("\\", "/");
                Debug.Log($"Start Build:{excelPath}");
                var classBaseName = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}");
                var className = JsonClassGenerator.ToTitleCase($"{entry.ExcelName}Table");
                var ExcelData = ExcelTableData.Parser(excelPath, classBaseName);
                var json = DataTableDataGenerator.BuildTableDataJson(ExcelData);
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
            AssetDatabase.Refresh();
        }


        [Button("生成SO")]
        public void BuildOverviewSo()
        {
            InitDirInfo();
            foreach (var excelEntry in ExcelList)
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
            object subObj=null;
            FieldInfo[] fieldInfos=new FieldInfo[]{};
            int listCount=0;
            string[] dirPath = new string[] { PackConfig.PackageDir};
            //寻找到所有继承ExcelTableOverview的SO
            foreach (ExcelTableOverview excelTableOverviewSo in AssetDatabaseUtility.FindAllExcelTableOverviewSO(dirPath))
            {
                #region 旧方法
                // GetGenericTypeDataFields(excelTableOverviewSo,ref subObj,ref fieldInfos,ref listCount);
                // CreateFile(fieldInfos,excelTableOverviewSo.GetName());
                //
                // //写入数据
                // for (int i = 0; i < listCount; i++)
                // {
                //     Debug.Log("长度:"+listCount);
                //     object item = subObj.GetType().GetProperty("Item").GetValue(subObj, new object[] { i });
                //     fieldInfos = item.GetType().GetFields();
                //     List<string> texts = new List<string>();
                //     foreach (FieldInfo fieldInfo in fieldInfos)
                //     {
                //         texts.Add(fieldInfo.GetValue(item).ToString());
                //     }
                //     AppendToFile(texts,excelTableOverviewSo.GetName());
                // }
                

                #endregion
                
                excelTableOverviewSo.BuildCSVFile();
            }
        }
        /// <summary>
        /// 传入需要查询的SO对象，通过反射打印其List
        /// </summary>
        /// <param name="excelTableOverviewSO"></param>
        public void GetGenericTypeDataFields(ExcelTableOverview excelTableOverviewSO,ref object subObj,ref FieldInfo[] fieldInfos,ref int listCount)
        {
            //获得SO中的Data类型
            Type type = excelTableOverviewSO.GetDataType();

            // 通过类实例的Type获取所有公共字段
            FieldInfo[] infos = type.GetFields();   //各SO的Rows
            
            foreach (FieldInfo info in infos)
            {
                if (info != null)
                {
                    // 判断字段类型是否为泛型类型，如：
                    // typeof(int).IsGenericType --> False
                    // typeof(List<int>).IsGenericType --> True
                    // typeof(Dictionary<int>).IsGenericType --> True
                    if (info.FieldType.IsGenericType)
                    {
                        // 获取该泛型属性值，返回一个列表，如List<Man>；
                        // 因为是反射返回的数据，无法直接转换为List使用，针对这种数据，反射机制对这种属性值提供了
                        // “Count”列表长度、“Item”子元素等属性；
                        var dataField = excelTableOverviewSO.GetType().GetField("Data").GetValue(excelTableOverviewSO);
                        subObj = info.GetValue(dataField);
                        
                        if (subObj!=null)
                        {
                            // 获取列表List<Man>长度
                            listCount = Convert.ToInt32(subObj.GetType().GetProperty("Count").GetValue(subObj, null));
                            
                            for (int i = 0; i < listCount; i++)
                            {
                                // 获取列表子元素，然后子元素其实也是一个类，然后递归调用当前方法获取类的所有公共属性
                                object item = subObj.GetType().GetProperty("Item").GetValue(subObj, new object[] { i });
                                fieldInfos = item.GetType().GetFields();
                            }
                            
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fieldInfos">传入的字段数组</param>
        /// <param name="fileName">传入的文件名</param>
        private void CreateFile(FieldInfo[] fieldInfos,string fileName)
        {
            VerifyCSVDirectory();
            GetHeaderString(fieldInfos);
            using (StreamWriter sw = File.CreateText(DirInfo.CSVDir+"/"+PackConfig.Lang+"/"+fileName+".csv"))
            {
                string finalString = "";
                foreach (string header in csvHeader)
                {
                    if (finalString != "")
                    {
                        finalString += csvSeparator;
                    }
                    finalString += header;
                }
                sw.WriteLine(finalString);
                
                finalString = "";
                foreach (string headerType in csvHeaderType)
                {
                    if (finalString != "")
                    {
                        finalString += csvSeparator;
                    }
                    finalString += headerType;
                }
                sw.WriteLine(finalString);
            }
        }
        private void AppendToFile(List<string> strings,string fileName)
        {
            using (StreamWriter sw = File.AppendText(DirInfo.CSVDir+"/"+PackConfig.Lang+"/"+fileName+".csv"))
            {
                string finalString = "";
                foreach (string text in strings)
                {
                    if (finalString != "")
                    {
                        finalString += csvSeparator;
                    }
                    finalString += text;
                }
                sw.WriteLine(finalString);
            }
        }

        /// <summary>
        /// 验证文件夹
        /// </summary>
        private void VerifyCSVDirectory()
        {
            string directory = DirInfo.CSVDir+"/"+PackConfig.Lang;
            Debug.Log(directory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void GetHeaderString(FieldInfo[] fieldInfos)
        {
            List<string> headerText = new List<string>();
            List<string> headerTypeText = new List<string>();
            List<string> headerDescText = new List<string>();
            foreach (var field in fieldInfos)
            {
                headerText.Add(field.Name);
                headerTypeText.Add(field.FieldType.Name);
                headerDescText.Add("");
            }
            csvHeader = headerText;
            csvHeaderType = headerTypeText;
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
}