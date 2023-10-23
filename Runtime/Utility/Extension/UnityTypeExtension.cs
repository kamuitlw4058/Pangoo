using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using NPinyin;
using GameFramework;


namespace Pangoo
{

    public static class UnityTypeExtension
    {
        public static float ToFloatForce(this string val)
        {
            float outVal;
            if (float.TryParse(val, out outVal))
            {
                return outVal;
            }
            return 0;
        }


        #region  Vector3

        public static string ToSave(this Vector3 val)
        {
            return Utility.Text.Format("{0}|{1}|{2}", val.x, val.y, val.z);

        }

        public static Vector3 ToVector3(this string val)
        {
            if (val.IsNullOrWhiteSpace())
            {
                return Vector3.zero;
            }
            var vals = val.Split("|");
            if (vals.Length != 3)
            {
                return Vector3.zero;
            }

            return new Vector3(vals[0].ToFloatForce(), vals[1].ToFloatForce(), vals[2].ToFloatForce());

        }
        #endregion



    }
}
