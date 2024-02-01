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

    public static class StringListExtension
    {



        public static string Random(this string[] s)
        {
            if (s == null || (s != null && s.Length == 0)) return null;
            return s[UnityEngine.Random.Range(0, s.Length)];
        }




    }
}
