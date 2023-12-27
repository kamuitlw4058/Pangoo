
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

        public static Vector3 Lerp(Vector3 input, Vector3 target, float val)
        {
            return new Vector3()
            {
                x = Mathf.Lerp(input.x, target.x, val),
                y = Mathf.Lerp(input.y, target.y, val),
                z = Mathf.Lerp(input.z, target.z, val),
            };
        }


    }
}