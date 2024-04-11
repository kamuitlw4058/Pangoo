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

    }
}
