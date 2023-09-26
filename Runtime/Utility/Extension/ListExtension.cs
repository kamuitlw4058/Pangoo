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
