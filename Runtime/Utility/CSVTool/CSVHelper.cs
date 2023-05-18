using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVHelper : MonoBehaviour
{
    #region Made By furukazu , Thanks furukazu
        // flag for processing a CSV
        private enum ParsingMode
        {
            // default(treat as null)
            None,
            // processing a character which is out of quotes 
            // 处理没有引号的字符
            OutQuote,
            // processing a character which is in quotes
            // 处理带引号的字符
            InQuote
        }

        /// <summary>
        /// 解析CSV字符串。
        /// </summary>
        /// <returns>一个二维数组。第一个索引表示行。第二个索引表示列</returns>
        /// <param name="src">raw CSV contents as string</param>
        public static List<List<string>> ParseCSV(string src)
        {
            var rows = new List<List<string>>();
            var cols = new List<string>();
#pragma warning disable XS0001 // Find APIs marked as TODO in Mono
            var buffer = new StringBuilder();
#pragma warning restore XS0001 // Find APIs marked as TODO in Mono

            ParsingMode mode = ParsingMode.OutQuote;
            bool requireTrimLineHead = false;
            var isBlank = new Regex(@"\s");

            int len = src.Length;

            for (int i = 0; i < len; ++i)
            {

                char c = src[i];

                // remove whilespace at beginning of line
                if (requireTrimLineHead)
                {
                    if (isBlank.IsMatch(c.ToString()))
                    {
                        continue;
                    }
                    requireTrimLineHead = false;
                }

                // finalize when c is the last character
                if ((i + 1) == len)
                {
                    // final char
                    switch (mode)
                    {
                        case ParsingMode.InQuote:
                            if (c == '"')
                            {
                                // ignore
                            }
                            else
                            {
                                // if close quote is missing
                                buffer.Append(c);
                            }
                            cols.Add(buffer.ToString());
                            rows.Add(cols);
                            return rows;

                        case ParsingMode.OutQuote:
                            if (c == ',')
                            {
                                // if the final character is comma, add an empty cell
                                // next col
                                cols.Add(buffer.ToString());
                                cols.Add(string.Empty);
                                rows.Add(cols);
                                return rows;
                            }
                            if (cols.Count == 0)
                            {
                                // if the final line is empty, ignore it. 
                                if (string.Empty.Equals(c.ToString().Trim()))
                                {
                                    return rows;
                                }
                            }
                            buffer.Append(c);
                            cols.Add(buffer.ToString());
                            rows.Add(cols);
                            return rows;
                    }
                }

                // the next character
                char n = src[i + 1];

                switch (mode)
                {
                    case ParsingMode.OutQuote:
                        // out quote
                        if (c == '"')
                        {
                            // to in-quote
                            mode = ParsingMode.InQuote;
                            continue;

                        }
                        else if (c == ',')
                        {
                            // next cell
                            cols.Add(buffer.ToString());
                            buffer.Remove(0, buffer.Length);

                        }
                        else if (c == '\r' && n == '\n')
                        {
                            // new line(CR+LF)
                            cols.Add(buffer.ToString());
                            rows.Add(cols);
                            cols = new List<string>();
                            buffer.Remove(0, buffer.Length);
                            ++i; // skip next code
                            requireTrimLineHead = true;

                        }
                        else if (c == '\n' || c == '\r')
                        {
                            // new line
                            cols.Add(buffer.ToString());
                            rows.Add(cols);
                            cols = new List<string>();
                            buffer.Remove(0, buffer.Length);
                            requireTrimLineHead = true;

                        }
                        else
                        {
                            // get one char
                            buffer.Append(c);
                        }
                        break;

                    case ParsingMode.InQuote:
                        // in quote
                        if (c == '"' && n != '"')
                        {
                            // to out-quote
                            mode = ParsingMode.OutQuote;

                        }
                        else if (c == '"' && n == '"')
                        {
                            // get "
                            buffer.Append('"');
                            ++i;

                        }
                        else
                        {
                            // get one char
                            buffer.Append(c);
                        }
                        break;
                }
            }
            return rows;
        }
        #endregion
}
