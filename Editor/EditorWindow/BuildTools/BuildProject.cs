using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Pangoo.Editor.ResourceTools;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;
using UnityGameFramework.Editor.ResourceTools;
using Debug = UnityEngine.Debug;

namespace Pangoo.Editor
{
    public class BuildProject
    {
        private static string m_CommitID;

        private static string outputDirPath = @"C:\Users\ASUS\Desktop";
        private static string dirPath;
        private static string tagName = "A0";
        private static string monthDay = "0918";
        private static string buildNumber = "123";

        private UnityAction buildResoureceEvent;
        
        /// <summary>
        /// 是否为在编辑器内部打包
        /// </summary>
        private static bool isJenkinsBuild = true;

        private static bool isTest;
        private static string devPackageOptions;

        public static bool isBuildFail;
        [MenuItem("Pangoo/BuildTools/BuildPC")]
        public static async void BuildPC()
        {
            Debug.Log("项目根目录1：" + Directory.GetParent(Application.dataPath));

            isJenkinsBuild = GetCommandLineArgValue("-outputDirPath") != null;
            
            #region 获取jenkins参数值

            if (isJenkinsBuild)
            {
                outputDirPath = GetCommandLineArgValue("-outputDirPath");
                tagName = GetCommandLineArgValue("-tagName");
                buildNumber = GetCommandLineArgValue("-buildNumber");
                monthDay = GetCommandLineArgValue("-monthDay");
                isTest = Boolean.Parse(GetCommandLineArgValue("-isTest"));
                devPackageOptions=GetCommandLineArgValue("-devPackageOptions");
            }
            else
            {
                outputDirPath = @"C:\Users\ugmax\Desktop\FangLing_HDRP_Package";
                tagName = "测试";
                buildNumber = "99";
                monthDay = "1102";
                isTest = true;
                devPackageOptions = "只打Build";
            }

            #endregion
            
            // await BuildResoure();
            // await MoveABPackgeResource();

            if (isTest)
            {
                BuildSettingAndRun();
            }
            else
            {
                if (devPackageOptions!="只打Build")
                {
                    BuildResoure();
                    MoveABPackgeResource();
                }

                if (isBuildFail)
                {
                    Debug.LogError("资源包打包错误");
                    return;
                }
                
                if (devPackageOptions != "只打AB包")
                {
                    BuildSettingAndRun();
                }
            }
        }

        private static void BuildSettingAndRun()
        {
            Debug.Log("开始设置构建参数并执行构建");
            #region 构建设置

            BuildPlayerOptions options = new BuildPlayerOptions();
            //获取所有场景名字
            string[] scenePaths = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                scenePaths[i] = EditorBuildSettings.scenes[i].path;
            }

            options.scenes = scenePaths;
            options.target = BuildTarget.StandaloneWindows64;
            options.options = BuildOptions.None;

            dirPath = $@"{outputDirPath}";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            options.locationPathName = $@"{dirPath}\{PlayerSettings.productName}.exe";

            #endregion

            #region 创建一个记录Git提交ID的txt文件

            GetCommitID();
            if (!File.Exists($@"{dirPath}\提交ID.txt"))
            {
                FileStream fileStream = new FileStream($@"{dirPath}\sourceTree_CommitID.txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8);

                sw.WriteLine(m_CommitID);
                sw.Close();
                sw.Dispose();
                fileStream.Close();
                fileStream.Dispose();
            }
            else
            {
                File.WriteAllText($@"{dirPath}\提交ID.txt", m_CommitID);
            }

            #endregion

            Debug.Log("项目本地路径名1：" + options.locationPathName);

            if (isTest)
            {
                EditorUserBuildSettings.development = true;
            }

            BuildPipeline.BuildPlayer(options);
        }

        private static string EnvironmentVariable
        {
            get
            {
                string sPath = System.Environment.GetEnvironmentVariable("Path");
                var result = sPath.Split(';');
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i].Contains(@"Git\cmd"))
                    {
                        sPath = result[i];
                        return sPath;
                    }
                }

                return sPath;
            }
        }

        private static void GetCommitID()
        {
            string gitPath = System.IO.Path.Combine(EnvironmentVariable, "git.exe");
            Debug.LogFormat("环境路径：{0}", gitPath);
            Process p = new Process();
            p.StartInfo.FileName = gitPath; //获取或设置要启动的应用程序或文档。
            p.StartInfo.Arguments = "rev-parse HEAD"; //命令行命令
            p.StartInfo.CreateNoWindow = true; //获取或设置指示是否在新窗口中启动该进程的值。
            p.StartInfo.UseShellExecute = false; //获取或设置指示是否使用操作系统 shell 启动进程的值。
            p.StartInfo.RedirectStandardOutput = true; //获取或设置指示是否将应用程序的文本输出写入 StandardOutput 流中的值。
            p.OutputDataReceived += OnOutputDataReceived;
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
        }
        
        private static void AddTagAndPush()
        {
            //tagName = GetCommandLineArgValue("-tagName");
            tagName = "ABCDE";
            Debug.Log("标签名字:" + tagName);

            string gitPath = System.IO.Path.Combine(EnvironmentVariable, "git.exe");
            Process p = new Process();
            p.StartInfo.FileName = gitPath;

            p.StartInfo.Arguments = $"tag -a {tagName} -m \"{tagName}\"";
            Debug.Log("命令:" + p.StartInfo.Arguments);
            p.Start();
            p.StartInfo.Arguments = $"push -f origin {tagName}";
            p.WaitForExit();
            Debug.Log("命令:" + p.StartInfo.Arguments);
            p.Start();
            Debug.Log("推送标签");
            p.WaitForExit();
        }

        private static void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e != null && !string.IsNullOrEmpty(e.Data))
            {
                m_CommitID = e.Data;
                Debug.Log("提交ID1：" + m_CommitID);
            }
        }

        /// <summary>
        /// 获得命令行参数的值
        /// </summary>
        /// <param name="argName"></param>
        /// <returns></returns>
        private static string GetCommandLineArgValue(string argName)
        {
            string[] commandLineArgs = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < commandLineArgs.Length - 1; i++)
            {
                if (commandLineArgs[i].ToLower() == argName.ToLower())
                {
                    Debug.Log($"命令行内容:{commandLineArgs[i]}");
                    return commandLineArgs[i + 1];
                }
            }

            return null;
        }

        static ResourceBuilderController m_Controller = new ResourceBuilderController();
        static ResourceBuilder m_Builder = new ResourceBuilder();
        static string abPackgePath = $"{Directory.GetParent(Application.dataPath)?.ToString()}/ABs";
        public static string copyPath;

        [MenuItem("Pangoo/BuildTools/BuildResoure")]
        private static void BuildResoure()
        {
            Debug.Log("检查是否需要构建资源");

            try
            {
                isTest=Boolean.Parse(GetCommandLineArgValue("-isTest"));
            }
            catch (Exception e)
            {
                isTest = true;
            }
            
            if (isTest)
            {
                string dirPath = Application.streamingAssetsPath + "/" + "GameMain";
                string filePath=Application.streamingAssetsPath + "/" + "GameFrameworkVersion.dat";
                if (Directory.Exists(dirPath)&&File.Exists(filePath))
                {
                    Debug.Log("已包含资源文件，跳过重复构建");
                    return;
                }
            }
            
            Debug.Log("开始打包资源");
            //创建存放打包资源的文件夹
            if (!Directory.Exists(abPackgePath))
            {
                Debug.Log("创建ABs文件夹");
                Directory.CreateDirectory(abPackgePath);
            }
            
            //刷新打包资源的信息
            ResourceRuleEditor m_resourceRule = new ResourceRuleEditor();
            m_resourceRule.RefreshResourceCollection();
            m_resourceRule.Save();

            //m_Builder.m_OrderBuildResources = false;

            if (m_Controller.Load())
            {
                Debug.Log("Load configuration success.");

                m_Builder.m_CompressionHelperTypeNameIndex = 0;
                string[] compressionHelperTypeNames = m_Controller.GetCompressionHelperTypeNames();
                for (int i = 0; i < compressionHelperTypeNames.Length; i++)
                {
                    if (m_Controller.CompressionHelperTypeName == compressionHelperTypeNames[i])
                    {
                        m_Builder.m_CompressionHelperTypeNameIndex = i;
                        break;
                    }
                }

                m_Controller.InternalResourceVersion = int.Parse(buildNumber);
                Debug.Log("资源包版本:"+m_Controller.InternalResourceVersion);
                m_Controller.RefreshCompressionHelper();

                m_Builder.m_BuildEventHandlerTypeNameIndex = 0;
                string[] buildEventHandlerTypeNames = m_Controller.GetBuildEventHandlerTypeNames();
                for (int i = 0; i < buildEventHandlerTypeNames.Length; i++)
                {
                    if (m_Controller.BuildEventHandlerTypeName == buildEventHandlerTypeNames[i])
                    {
                        m_Builder.m_BuildEventHandlerTypeNameIndex = i;
                        break;
                    }
                }

                m_Controller.RefreshBuildEventHandler();
            }
            else
            {
                Debug.LogWarning("加载配置失败.");
            }

            Debug.Log("配置中输出目录:" + m_Controller.OutputDirectory);
            m_Controller.OutputDirectory = abPackgePath;
            
            copyPath = m_Controller.OutputPackagePath;
            m_Controller.BuildEventHandlerTypeName = "TryCatchBuildEventHandler";
            m_Builder.BuildResources(m_Controller);
            
            MoveABPackgeResource();
            //return Task.CompletedTask;
        }

        private static void MoveABPackgeResource()
        {
            Debug.Log("开始移动资源包");
            m_Controller.Load();

            string sourceDirectoryPath = $"{copyPath}/Windows";
            Debug.Log("复制路径:" + sourceDirectoryPath);
            string targetDirectoryPath = $"{Application.streamingAssetsPath}";
            Debug.Log("目标路径:" + targetDirectoryPath);

            Debug.Log("开始拷贝文件夹");
            CopyPastFilesAndDirs(sourceDirectoryPath, targetDirectoryPath);

            Debug.Log("资源移动完成");
        }

        private static void CopyPastFilesAndDirs(string srcDir, string destDir)
        {
            if (!Directory.Exists(destDir)) //若目标文件夹不存在
            {
                Directory.CreateDirectory(destDir); //创建目标文件夹        
            }

            string newPath;
            FileInfo fileInfo;
            Directory.CreateDirectory(destDir); //创建目标文件夹                                                  
            string[] files = Directory.GetFiles(srcDir); //获取源文件夹中的所有文件完整路径
            foreach (string path in files) //遍历文件     
            {
                fileInfo = new FileInfo(path);
                newPath = Path.Combine(destDir, fileInfo.Name);
                Debug.Log("新文件路径:" + newPath);
                Debug.Log("<>fileName=" + fileInfo.Name);
                File.Copy(path, newPath, true);
            }

            string[] dirs = Directory.GetDirectories(srcDir);
            foreach (string path in dirs) //遍历文件夹
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                string newDir = Path.Combine(destDir, directory.Name);
                Debug.Log("新目录路径:" + newDir);
                Debug.Log("<>DirName=" + directory.Name);
                CopyPastFilesAndDirs(path, newDir);
            }
        }
    }
}