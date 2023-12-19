
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Pangoo.Common
{
    public static class MathUtility
    {

        public static float Remap(float input, Vector2 inputMinMax, Vector2 outputMinMax)
        {
            return ((input - inputMinMax.x) / Mathf.Abs(inputMinMax.y - inputMinMax.x) * Mathf.Abs((outputMinMax.y - outputMinMax.x))) + outputMinMax.x;
        }
    }
}