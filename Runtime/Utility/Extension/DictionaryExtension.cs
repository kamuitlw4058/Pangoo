using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
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

    }
}
