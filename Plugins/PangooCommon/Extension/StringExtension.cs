using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using NPinyin;


namespace Pangoo.Common
{

    public static class StringExtension
    {
        private static readonly CultureInfo CULTURE = CultureInfo.InvariantCulture;
        private const NumberStyles HEX = NumberStyles.HexNumber;

        private static readonly TextInfo TXT_INFO = CultureInfo.InvariantCulture.TextInfo;


        public static IEnumerable<T> ToSplitIEnumerable<T>(this string s, string split = "|")
        {

            if (string.IsNullOrWhiteSpace(s))
            {
                return new List<T>();
            }
            return s.Split(split).Select(o => o.ToValue<T>());
        }

        public static T[] ToSplitArr<T>(this string s, string split = "|")
        {
            var arr = ToSplitIEnumerable<T>(s, split);
            if (arr.Count() == 0)
            {
                return new T[0];
            }
            return arr.ToArray();
        }
        public static List<T> ToSplitList<T>(this string s, string split = "|")
        {
            return ToSplitIEnumerable<T>(s, split)?.ToList();
        }

        public static List<int> ToListInt(this string s, string split = "|")
        {
            if (string.IsNullOrEmpty(s))
            {
                return new List<int>();
            }
            return s.Split(split).Select(o => int.Parse(o)).ToList();
        }

        public static int[] ToArrInt(this string str, string split = "|")
        {
            List<int> ret = new List<int>();
            if (str.IsNullOrWhiteSpace())
            {
                return new int[0];
            }

            var strs = str.Split(split);

            for (int i = 0; i < strs.Length; i++)
            {
                if (int.TryParse(strs[i], out int value))
                {
                    ret.Add(value);
                }
            }
            return ret.ToArray();
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            return false;
        }


        public static string Humanize(this string source)
        {
            if (source == null)
            {
                return null;
            }

#if UNITY_EDITOR
            source = UnityEditor.ObjectNames.NicifyVariableName(source);
#endif

            source = source.Replace('-', ' ').Replace('_', ' ');
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

        public static T ToEnum<T>(this string str, T defaultVal = default(T))
        {
            if (str == null)
            {
                return defaultVal;
            }
            try
            {
                return (T)Enum.Parse(typeof(T), str);
            }
            catch
            {
                return defaultVal;
            }
        }


        public static string ToPascal(this string str)
        {
            char[] chars = str.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            for (int i = 1; i < chars.Length; i++)
            {
                chars[i] = char.ToLower(chars[i]);
            }
            return new string(chars);
        }

        public static bool ContainsChinese(this string str)
        {
            if (str.IsNullOrWhiteSpace())
            {
                return false;
            }
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        public static bool ContainsChinese(this char c)
        {
            return Regex.IsMatch(c.ToString(), @"[\u4e00-\u9fa5]");
        }

        public static string ToPinyin(this string str)
        {
            Debug.Log($"在toPin中：{str}");
            if (str.IsNullOrWhiteSpace())
            {
                return str;
            }


            if (!str.ContainsChinese())
            {
                return str.Replace("-", "_");
            }

            var chars = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            foreach (var c in chars)
            {
                if (c.ContainsChinese())
                {
                    sb.Append(Pinyin.GetPinyin(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Replace("-", "_");
        }

        public static T ToValue<T>(this string str)
        {
            var type = typeof(T);
            return (T)str.ToValue(type);
        }


        public static System.Object ToValue(this string str, Type type)
        {

            if (type.IsEnum)
            {
                int value = 0;
                if (str.IsNullOrWhiteSpace())
                {
                    return 0;
                }
                string[] options = str.Split('&');
                for (int i = 0; i < options.Length; i++)
                {
                    value += (int)Enum.Parse(type, options[i]);
                }
                return value;
            }
            else if (typeof(string).Equals(type))
            {
                return str;
            }
            else if (typeof(bool).Equals(type))
            {
                if (str.IsNullOrWhiteSpace())
                {
                    return false;
                }
                return str.ToLower().Equals("true");
            }
            else if (typeof(int).Equals(type))
            {
                int value = 0;
                int.TryParse(str, out value);
                return value;
            }
            else if (typeof(float).Equals(type))
            {
                float value = 0f;
                float.TryParse(str, out value);
                return value;
            }
            else if (typeof(Vector3).Equals(type))
            {
                string[] posValue = str.Trim(new char[] { '(', ')' }).Split(",");
                float x = 0f;
                float y = 0f;
                float z = 0f;

                try
                {
                    float.TryParse(posValue[0], out x);
                    float.TryParse(posValue[1], out y);
                    float.TryParse(posValue[2], out z);
                }
                catch (Exception e)
                {
                    Console.WriteLine("表中值不满足3位，请检查数据是否正确");
                }
                Vector3 value = new Vector3(x, y, z);
                return value;
            }

            return null;
        }


        public static string ToShortUuid(this string str)
        {
            if (str.IsNullOrWhiteSpace())
            {
                return str;
            }

            return str.Substring(0, Math.Min(8, str.Length));
        }


    }
}
