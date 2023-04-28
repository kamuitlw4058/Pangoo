using System.Collections.Generic;
using UnityEngine;

namespace Pangoo
{
    public static class TransformExtension
    {
        public static IEnumerable<Transform> Children(this Transform t, bool includesDescendants = false)
        {
            var l = t.childCount;
            for (var i = 0; i < l; ++i)
            {
                var child = t.GetChild(i);
                yield return child;
                if (includesDescendants)
                {
                    foreach (var descendant in child.Children(true))
                    {
                        yield return descendant;
                    }
                }
            }
        }


        public static T GetOrAddComponent<T>(this Transform origin) where T : Component
        {
            T component = origin.GetComponent<T>();
            if (component == null)
            {
                component = origin.gameObject.AddComponent<T>();
            }
            return component;
        }


        public static string GetPath(this Transform transform, string splitter = "/", bool root = true)
        {
            var result = transform.name;
            var parent = transform.parent;
            while (parent != null)
            {
                result = $"{parent.name}{splitter}{result}";
                parent = parent.parent;
            }
            if (root)
            {
                return $"/{result}";
            }
            else
            {
                return result;
            }

        }


    }
}
