using System;
using System.Collections.Generic;
using System.Text;

namespace NPinyin
{
    public static class StringExtension
    {
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
    }
}