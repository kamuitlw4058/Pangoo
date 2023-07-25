using System.IO;
using Pangoo;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;

namespace Pangoo.Editor
{
    public class ModelImportLocal
    {
        const string BasePath = "Assets/ArtSync";

        [Sirenix.OdinInspector.FolderPath(AbsolutePath =true,UseBackslashes =true,RequireExistingPath = true)]
        [OnValueChanged("OnModelPathChange")]
        public string ModelPath;

        public bool IsModelPathInProject;
        bool IsNameError;

        string CurrentPath =  Directory.GetCurrentDirectory();

        [InfoBox("目录名格式错误 请参考如下命名:DaMen_HoneSe_01", InfoMessageType.Error, "IsNameError")]
        public string Name;

        public bool CheckModelDirectory(string val){
            if(string.IsNullOrEmpty(val)){
                return false;
            }

            var vals = val.Split("_");
            if(vals.Length != 3){
                return true;
            }

            return false;
        }

        void OnModelPathChange(){
            if(!Directory.Exists(BasePath)){
                Directory.CreateDirectory(BasePath);
            }

            IsModelPathInProject = IsPathInProject(ModelPath)? true :false;

            Name = Path.GetFileName(ModelPath);
            IsNameError = CheckModelDirectory(Name);

            if(IsNameError){
                return;
            }


            // var ModelBasePath = Path.Join(BasePath,Name);
            // if(!Directory.Exists(ModelBasePath)){
            //     Directory.CreateDirectory(ModelBasePath);
            // }

            // var timestamp = string.Format("{0:yyyyMMddHHmmss}",DateTime.Now); //17 17 2017 2017

            // var ModelVersionPath = Path.Join(ModelBasePath,$"{Name}_{timestamp}");

            // DirectoryUtility.CopyDirectory(ModelPath,ModelVersionPath,true);
            // AssetDatabase.ImportAsset(ModelVersionPath);


            // string ModelAssetPath;
            // if(!IsModelPathInProject){
            //     var outputPath = Path.Join(BasePath,Path.GetFileName(ModelPath));
            //     outputPath = outputPath.Replace('\\', '/');
            //     Debug.Log($"TempPath:{outputPath}");
            //     File.Copy(ModelPath,outputPath,true);
            //     ModelAssetPath= outputPath;

            //     AssetDatabase.ImportAsset(outputPath);

            // }else{
            //     ModelAssetPath = ToAssetsPath(ModelPath);
            // }

            // ModelObject = AssetDatabaseUtility.LoadAssetAtPath<Object>(ToAssetsPath(ModelAssetPath));
        }


        [DisableIf("IsNameError")]
        [Button("导入本地", ButtonSizes.Large)]
        private void ImportFromLocal()
        {

        }

        bool IsPathInProject(string path){
            if(path.Length < CurrentPath.Length){
                return false;   
            }

            if(path.Substring(0,CurrentPath.Length) != CurrentPath){
                return false;
            }

            return true;
        }

        string ToAssetsPath(string path){
            path = path.Substring(path.IndexOf("Assets"));
            path = path.Replace('\\', '/');
            return path;
        }



    }

}
