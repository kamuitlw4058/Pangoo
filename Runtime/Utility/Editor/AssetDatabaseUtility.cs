using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pangoo
{
    public static class AssetDatabaseUtility
    {
#if UNITY_EDITOR
        public static T LoadAssetAtPath<T>(string path) where T : Object
        {
#if UNITY_5
            return AssetDatabase.LoadAssetAtPath<T>(path);
#else
            return (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
#endif
        }

        public static IEnumerable<T> FindAsset<T>() where T : Object
        {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name}")
            .Select(x => AssetDatabase.GUIDToAssetPath(x))
            .Select(x => LoadAssetAtPath<T>(x));
        }

        public static T FindAssetFirst<T>(string path = null) where T : Object
        {
            var items = FindAsset<T>();
            if (items.Count() > 0)
            {
                return items.First();
            }
            return null;
        }
        
        ///从资源文件夹中找到所有特定类型
        public static List<T> FindAllObjectFromResources<T>()
        {
            List<T> tmp = new List<T>();
            string ResourcePath = Application.dataPath + "/Resources";
            //返回与在指定目录中的指定搜索模式匹配的子目录的名称（包括其路径），还可以选择地搜索子目录
            //searchPattern:*（星号）通配符说明符	该位置的零个或多个字符
            string[] directories = Directory.GetDirectories(ResourcePath, "*", SearchOption.AllDirectories);

            foreach (string directorie in directories)
            {
                string directoriePath = directorie.Substring(ResourcePath.Length + 1);
                T[] result = Resources.LoadAll(directoriePath, typeof(T)).Cast<T>().ToArray();

                foreach (var item in result)
                {
                    if (!tmp.Contains(item))
                    {
                        tmp.Add(item);
                    }
                }
            }
            return tmp;
        }
        
        /// <summary>
        /// 在Assets中找到所有继承ExcelTableOverview的SO
        /// </summary>
        /// <returns>ExcelTableOverview列表</returns>
        public static List<ExcelTableOverview> FindAllExcelTableOverviewSO(string[] dirPath=null)
        {
            //在Assets中找到所有的ExcelTableOverviewSO并获得它的guid。
            string[] guids = AssetDatabase.FindAssets("t:ExcelTableOverview",dirPath);
            //创建一个数组，存储找到ExcelTableOverviewSO。
            ExcelTableOverview[] items = new ExcelTableOverview[guids.Length];

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);                      //使用guid来查找资产路径。
                Debug.Log(path);
                items[i] = AssetDatabase.LoadAssetAtPath<ExcelTableOverview>(path);        //使用path查找和加载ExcelTableOverviewSO。
            }
            return items.ToList();
        }
        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name="DirPath">字符串数组</param>
        /// <param name="separator">分隔符号</param>
        /// <returns></returns>
        public static string CombiningStrings(string[]DirPath,string separator)
        {
            string tmpStr = "";
            foreach (var str in DirPath)
            {
                if (tmpStr!="")
                {
                    tmpStr += separator;
                }

                if (str.Contains(","))
                {
                    tmpStr += $"\"{str}\"";
                }
                else
                {
                    tmpStr += str;
                }
            }
            return tmpStr;
        }
        
#endif
    }
}