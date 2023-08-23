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

        public static int[] ToArrInt(this string str,string split="|"){
            var strs= str.Split(split);
            int[] ret = new int[strs.Length];
            for(int i =0; i < strs.Length;i++){
                ret[i] = int.Parse(strs[i]);
            }
            return ret;
        }

    }
}
