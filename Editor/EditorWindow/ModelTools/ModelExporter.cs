using System.IO;
using Pangoo;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Pangoo.Editor
{
    public class ModelExporter
    {
        const string BasePath = "Assets/ArtSync";

        [Sirenix.OdinInspector.FilePath(AbsolutePath =true,Extensions = "fbx",UseBackslashes =true)]
        [OnValueChanged("OnModelPathChange")]
        public string ModelPath;

        public string CurrentPath =  Directory.GetCurrentDirectory();



        [PreviewField]
        public Object ModelObject;

        void OnModelPathChange(){
            if(!Directory.Exists(BasePath)){
                Directory.CreateDirectory(BasePath);
            }
        //    if(ModelPath.Length < )

            if(!string.IsNullOrEmpty(ModelPath)){
                
                ModelObject = AssetDatabaseUtility.LoadAssetAtPath<Object>(ModelPath);
                
            }   
        }

        bool IsPathInProject(string path){
            if(path.Length < CurrentPath.Length){
                
            }

            return true;
        }


    }

}
