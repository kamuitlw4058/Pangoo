using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
namespace Pangoo
{

    public static class ListExtension
    {
        public static string ToListString<T>(this List<T> list, string split = "|")
        {
            if (list == null)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.ToString());
                sb.Append(split);
            }
            return sb.ToString().TrimEnd(split.ToCharArray());
        }

        public static string ToListString<T>(this T[] list, string split = "|")
        {
            if (list == null)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.Append(item.ToString());
                sb.Append(split);
            }
            return sb.ToString().TrimEnd(split.ToCharArray());
        }

    }
}
