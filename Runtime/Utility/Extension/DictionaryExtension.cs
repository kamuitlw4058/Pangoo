using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System;

namespace Pangoo
{

    public static class DictionaryExtension
    {
        public static void Set<K, V>(this Dictionary<K, V> dict, K key, V val)
        {
            if (dict == null)
            {
                return;
            }

            if (dict.ContainsKey(key))
            {
                dict[key] = val;
            }
            else
            {
                dict.Add(key, val);
            }

            return;
        }

        public static List<K> DiffKeys<K, V>(this Dictionary<K, V> dict, Dictionary<K, V> other)
        {
            List<K> ret = new List<K>();
            if (other == null)
            {
                return ret;
            }

            if (dict == null)
            {
                if (other == null)
                {
                    return ret;
                }
                return other.Keys.ToList();
            }

            var otherKeys = other.Keys.ToList();
            for (int i = 0; i < otherKeys.Count; i++)
            {
                var otherKey = otherKeys[i];
                if (dict.ContainsKey(otherKey))
                {
                    continue;
                }

                ret.Add(otherKey);
            }

            return ret;
        }

        public static List<V> ValuesOfKeyList<K, V>(this Dictionary<K, V> dict, List<K> keys)
        {
            List<V> ret = new List<V>();
            foreach (var key in keys)
            {
                if (dict.TryGetValue(key, out V val))
                {
                    if (!ret.Contains(val))
                    {
                        ret.Add(val);
                    }
                }
            }
            return ret;
        }


        public static void SyncKey<K, V>(this Dictionary<K, V> dict, List<K> keys, Func<K, V> addAction = null)
        {
            if (dict == null || keys == null || (keys != null && keys.Count == 0)) return;

            List<K> needList = new List<K>();
            List<K> RemoveList = new List<K>();
            for (int i = 0; i < keys.Count; i++)
            {
                if (dict.ContainsKey(keys[i]) || needList.Contains(keys[i]))
                {
                    continue;
                }

                needList.Add(keys[i]);
            }

            foreach (var kv in dict)
            {
                if (!keys.Contains(kv.Key))
                {
                    RemoveList.Add(kv.Key);
                }
            }


            RemoveList.ForEach(o =>
            {
                dict.Remove(o);
            });


            needList.ForEach(o =>
            {
                if (addAction != null)
                {
                    var val = addAction.Invoke(o);
                    if (val != null)
                    {
                        dict.Add(o, val);
                    }
                }
            });


        }

        public static void SyncKeyValue<K, V>(this Dictionary<K, V> dict, List<K> keys, Func<K, V, bool> CheckValue, Func<K, V> addAction, Action<K, V> removeAction)
        {
            if (dict == null || keys == null || (keys != null && keys.Count == 0) || CheckValue == null || addAction == null) return;

            List<K> RemoveList = new List<K>();


            foreach (var kv in dict)
            {
                if (!keys.Contains(kv.Key))
                {
                    RemoveList.Add(kv.Key);
                }

                if (!CheckValue(kv.Key, kv.Value) && !RemoveList.Contains(kv.Key))
                {
                    RemoveList.Add(kv.Key);
                }
            }


            RemoveList.ForEach(o =>
            {
                var val = dict[o];
                dict.Remove(o);
                removeAction?.Invoke(o, val);
            });

            keys.ForEach(o =>
            {
                if (dict.ContainsKey(o))
                {
                    return;
                }

                if (addAction != null)
                {
                    var val = addAction.Invoke(o);
                    if (val != null)
                    {
                        dict.Add(o, val);
                    }
                }
            });

        }

        public static void AddToDictList<K, V, D>(this Dictionary<K, V> dict, K key, D inputData) where V : List<D>, new() where D : class
        {

            if (dict.TryGetValue(key, out V dataList))
            {
                if (dataList.Count == 0)
                {
                    dataList.Add(inputData);
                    return;
                }

                bool flag = false;

                foreach (var data in dataList)
                {
                    if (data.Equals(inputData))
                    {
                        flag = true;
                    }
                }

                if (!flag)
                {
                    dataList.Add(inputData);
                }
            }
            else
            {
                V newList = new V();
                newList.Add(inputData);
                dict.Add(key, newList);
            }
        }

        public static void RemoveFromDictList<K, V, D>(this Dictionary<K, V> dict, K key, D inputData) where V : List<D>, new() where D : class
        {

            if (dict.TryGetValue(key, out V loaderDataList))
            {
                if (loaderDataList.Count == 0)
                {
                    return;
                }


                V removeList = new V();

                foreach (var data in loaderDataList)
                {
                    if (data.Equals(inputData))
                    {
                        removeList.Add(data);
                    }
                }

                foreach (var data in removeList)
                {
                    loaderDataList.Remove(data);
                }

            }
        }

    }
}
