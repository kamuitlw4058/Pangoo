using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class DirectoryUtility
{
    public static void ExistsOrCreate(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public static bool CopyDirectory(string sourceFolder, string destFolder,bool IsImporterAsset=false)
    {

        try
        {
            //如果目标路径不存在,则创建目标路径
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            //得到原文件根目录下的所有文件
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);//复制文件

                #if UNITY_EDITOR
                if(IsImporterAsset){
                    AssetDatabase.ImportAsset(dest);
                }   
            
                #endif
            }

            //得到原文件根目录下的所有文件夹
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyDirectory(folder, dest);//构建目标路径,递归复制文件
            }                
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }
}
