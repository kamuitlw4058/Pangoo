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
            List<int> ret = new List<int>();
            // int[] ret = new int[strs.Length];
            for(int i =0; i < strs.Length;i++){
                if(int.TryParse(strs[i],out int value)){
                    ret.Add(value);
                    // ret[i] = int.Parse(strs[i]);
                }
            }
            return ret.ToArray();
        }

        public static bool IsNullOrWhiteSpace(this string str){
            if(string.IsNullOrWhiteSpace(str)){
                return true;
            }
            return false;
        }

    }
}
