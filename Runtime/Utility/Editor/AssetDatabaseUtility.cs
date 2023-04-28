using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pangoo
{
    public class AssetDatabaseUtility
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
#endif
    }
}