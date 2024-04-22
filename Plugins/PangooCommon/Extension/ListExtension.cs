using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace Pangoo.Common
{

    public static class ListExtension
    {

        public static bool IsNullOrEmpty<T>(this List<T> l)
        {
            if (l == null || (l != null && l.Count == 0))
            {
                return true;
            }

            return false;
        }


        public static List<T> DiffItems<T>(this List<T> l1, List<T> l2)
        {
            List<T> ret = new List<T>();

            if (l2.IsNullOrEmpty()) return ret;

            if (l1.IsNullOrEmpty()) return l2;

            foreach (var item in l2)
            {
                if (!l1.Contains(item))
                {
                    ret.Add(item);
                }

            }
            return ret;
        }




    }
}
