using System.Collections.Generic;
using UnityEngine;

namespace Pangoo
{
    public static class TransformExtension
    {
        public static void Rotate2TransformPlane(this Transform t, Transform target)
        {
            if (t == null || target == null) return;

            Vector3 targetForward = target.TransformDirection(Vector3.forward);

            Vector3 targetUp = target.TransformDirection(Vector3.up);

            // // 使用LookRotation计算旋转
            Quaternion rotationQuaternion = Quaternion.LookRotation(targetForward, targetUp);

            t.rotation = rotationQuaternion;
        }


        public static List<Transform> Children(this Transform t, bool includesDescendants = false)
        {
            List<Transform> ret = new List<Transform>();
            for (int i = 0; i < t.childCount; i++)
            {
                ret.Add(t.GetChild(i));
            }
            return ret;
        }

        public static void DestroyChildern(this Transform t)
        {
            var l = t.childCount;
            List<Transform> trans = new List<Transform>();
            for (var i = 0; i < l; ++i)
            {
                var child = t.GetChild(i);
                trans.Add(child);
            }
            foreach (var tran in trans)
            {
                Object.Destroy(tran.gameObject);
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
