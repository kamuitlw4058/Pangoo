using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Globalization;

namespace Pangoo
{

    public static class StringExtension
    {
        private static readonly CultureInfo CULTURE = CultureInfo.InvariantCulture;
        private const NumberStyles HEX = NumberStyles.HexNumber;

        private static readonly TextInfo TXT_INFO = CultureInfo.InvariantCulture.TextInfo;
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


        public static string Humanize(this string source)
        {
            if(source == null){
                return null;
            }

            #if UNITY_EDITOR
            source = UnityEditor.ObjectNames.NicifyVariableName(source);
            #endif

            source =source.Replace('-',' ').Replace('_',' ');
            source = TXT_INFO.ToTitleCase(source);
            return source;
        }


        public static Color ToColor(this string input)
        {
            if (input.Length > 9 || input.IsNullOrWhiteSpace()) return default;


            if (input[0] == '#') input = input[1..];
            int inputLength = input.Length;
            
            if (inputLength != 6 && inputLength != 8) return default;
            int r = int.Parse($"{input[0]}{input[1]}", HEX, CULTURE);
            int g = int.Parse($"{input[2]}{input[3]}", HEX, CULTURE);
            int b = int.Parse($"{input[4]}{input[5]}", HEX, CULTURE);
            
            int a = inputLength == 8
                ? int.Parse($"{input[6]}{input[7]}", HEX, CULTURE)
                : 255;
            
            return new Color(
                r / 255f,
                g / 255f,
                b / 255f,
                a / 255f
            );
        }

    }
}
