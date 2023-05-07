using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Pangoo
{
    public static class TypeUtility
    {
        private static readonly string[] RuntimeAssemblyNames =
        {
#if UNITY_2017_3_OR_NEWER
            "UnityGameFramework.Runtime",
#endif
            "Pangoo",
        };

        private static readonly string[] RuntimeOrEditorAssemblyNames =
        {
#if UNITY_2017_3_OR_NEWER
            "UnityGameFramework.Runtime",
#endif
            "Assembly-CSharp",
#if UNITY_2017_3_OR_NEWER
            "UnityGameFramework.Editor",
#endif
            "Pangoo",
        };

        
        public static string[] GetAssemblyNames(){
            List<string> list = new List<string>();
            list.AddRange(RuntimeAssemblyNames);
            var path = "Assets/GameMain/StreamRes/Configs/ugf.txt";
            if (File.Exists(path))
            {
                var lines = File.ReadLines(path);
                list.AddRange(lines);
            }
            return list.ToArray();
        }


        /// <summary>
        /// 在运行时程序集中获取指定基类的所有子类的名称。
        /// </summary>
        /// <param name="typeBase">基类类型。</param>
        /// <returns>指定基类的所有子类的名称。</returns>
        internal static string[] GetRuntimeTypeNames(System.Type typeBase)
        {
            return GetTypeNames(typeBase, GetAssemblyNames());
        }

        
        /// <summary>
        /// 在运行时程序集中获取指定基类的所有子类的名称。
        /// </summary>
        /// <param name="typeBase">基类类型。</param>
        /// <returns>指定基类的所有子类的名称。</returns>
        internal static System.Type[] GetRuntimeTypes(System.Type typeBase)
        {
            return GetTypes(typeBase, GetAssemblyNames());
        }

        public static System.Type GetRuntimeType(string typeName){
            return GetType(typeName,GetAssemblyNames());
        }

        private static string[] GetTypeNames(System.Type typeBase, string[] assemblyNames)
        {
            List<string> typeNames = new List<string>();
            foreach (string assemblyName in assemblyNames)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.Load(assemblyName);
                }
                catch
                {
                    continue;
                }

                if (assembly == null)
                {
                    continue;
                }

                System.Type[] types = assembly.GetTypes();
                foreach (System.Type type in types)
                {
                    if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                    {
                        typeNames.Add(type.FullName);
                    }
                }
            }

            typeNames.Sort();
            return typeNames.ToArray();
        }


        private static System.Type[] GetTypes(System.Type typeBase, string[] assemblyNames)
        {
            List<System.Type> typeList = new List<System.Type>();
            foreach (string assemblyName in assemblyNames)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.Load(assemblyName);
                }
                catch
                {
                    continue;
                }

                if (assembly == null)
                {
                    continue;
                }

                System.Type[] types = assembly.GetTypes();
                foreach (System.Type type in types)
                {
                    if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                    {
                        typeList.Add(type);
                    }
                }
            }

            typeList.Sort();
            return typeList.ToArray();
        }


        private static System.Type GetType(string typeName, string[] assemblyNames)
        {
            foreach (string assemblyName in assemblyNames)
            {
                Assembly assembly = null;
                try
                {
                    assembly = Assembly.Load(assemblyName);
                }
                catch
                {
                    continue;
                }

                if (assembly == null)
                {
                    continue;
                }
                var type = assembly.GetType(typeName);
                if(type != null){
                    return type;
                }


            }
            return null;

     
        }
    }
}
