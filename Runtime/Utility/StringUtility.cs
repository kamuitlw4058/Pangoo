using System.Text.RegularExpressions;


namespace Pangoo
{
    public static class StringUtility
    {

        public static bool ContainsChinese(string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }

        public static bool IsOnlyDigit(string str){
            var charArr = str.ToCharArray();
            var ret = true;
            for(int i =0;i < charArr.Length;i++){
                if(!char.IsDigit(charArr[i])){
                    ret = false;
                }

            }

            return ret;

        }
    }
}