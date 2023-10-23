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
using Pangoo.Core.Common;

namespace Pangoo
{

    public static class PangooTypeExtension
    {


        #region  Vector3

        public static string ToSave(this TransformValue val)
        {
            // var Postion = Utility.Text.Format("{0}|{1}|{2}", val., val.y, val.z);
            // return Utility.Text.Format("{0}|{1}|{2}", .x, val.y, val.z);
            return Utility.Text.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                             val.Postion.x, val.Postion.y, val.Postion.z,
                             val.Rotation.x, val.Rotation.y, val.Rotation.z,
                             val.Scale.x, val.Scale.y, val.Scale.z
                             );

        }

        public static TransformValue ToTransformValue(this string val)
        {
            if (val.IsNullOrWhiteSpace())
            {
                return TransformValue.Empty;
            }
            var vals = val.Split("|");
            if (vals.Length != 9)
            {
                return TransformValue.Empty;
            }

            var position = new Vector3(vals[0].ToFloatForce(), vals[1].ToFloatForce(), vals[2].ToFloatForce());
            var rotation = new Vector3(vals[3].ToFloatForce(), vals[4].ToFloatForce(), vals[5].ToFloatForce());
            var scale = new Vector3(vals[6].ToFloatForce(), vals[7].ToFloatForce(), vals[8].ToFloatForce());

            return new TransformValue() { Postion = position, Rotation = rotation, Scale = scale };

        }
        #endregion



    }
}
