using System;
using System.IO;

namespace Pangoo{

    public static partial class PathUtility
    {
        public static string GetDirectoryName(string path){
            var ret = Path.GetDirectoryName(path);
            return ret.Replace("\\","/");
        }

        public static string GetFileName(string path){
            return Path.GetFileName(path);
        }

        public static string Join(string path,string subpath){
            return Path.Join(path,subpath).Replace("\\","/");
        }
    }
}