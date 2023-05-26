using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pangoo
{

    public static class StringExtension
    {
        public static List<int> ToListInt(this string s,string split = "|"){
            if(string.IsNullOrEmpty(s)){
                return new List<int>();
            }
            return s.Split(split).Select(o=> int.Parse(o)).ToList();
        }

    }
}
