using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Pangoo.Common
{
    public static class MathUtility
    {
        public static float Remap(float input, Vector2 inputMinMax, Vector2 outputMinMax)
        {
            var inputSign = inputMinMax.x > inputMinMax.y ? -1 : 1;
            var outputSign = outputMinMax.x > outputMinMax.y ? -1 : 1;
            var p = (input - inputMinMax.x) * inputSign / Mathf.Abs(inputMinMax.y - inputMinMax.x);
            var v = outputSign *  p * (Mathf.Abs(outputMinMax.y - outputMinMax.x)) + outputMinMax.x;
            return v;
        }

        public static float ClampRemap(float input, Vector2 inputMinMax, Vector2 outputMinMax)
        {
            var inputA = input;
            if (inputMinMax.x > inputMinMax.y)
            {
                
                inputA = Mathf.Clamp(input, inputMinMax.y, inputMinMax.x);
            }
            else
            {
                inputA = Mathf.Clamp(input, inputMinMax.x, inputMinMax.y);
            }
       
            return Remap(inputA, inputMinMax, outputMinMax);
           
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