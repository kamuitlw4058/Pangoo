using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BuildProject
{
    private static string m_CommitID;

    private static string outputDirPath = @"C:\Users\ASUS\Desktop";
    private static string dirPath;
    private static string tagName = "A0";
    private static string monthDay = "0918";
    private static string buildNumber = "123";


    [MenuItem("BuildManager/BuildPC")]
    public static void BuildPC()
    {
        Debug.Log("项目根目录1：" + Directory.GetParent(Application.dataPath).ToString());

        #region 获取jenkins参数值

        outputDirPath = GetCommandLineArgValue("-outputDirPath");
        tagName = GetCommandLineArgValue("-tagName");
        buildNumber = GetCommandLineArgValue("-buildNumber");
        monthDay = GetCommandLineArgValue("-monthDay");

        #endregion

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
        if (!File.Exists($@"{ dirPath}\提交ID.txt"))
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
            File.WriteAllText($@"{ dirPath}\提交ID.txt", m_CommitID);
        }


        #endregion

        Debug.Log("项目本地路径名1：" + options.locationPathName);
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

    [MenuItem("BuildManager/AddTagAndPush")]
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
            Debug.Log($"命令行内容:{commandLineArgs[i]}");
            if (commandLineArgs[i].ToLower() == argName.ToLower())
            {
                return commandLineArgs[i + 1];
            }
        }

        return null;
    }
}