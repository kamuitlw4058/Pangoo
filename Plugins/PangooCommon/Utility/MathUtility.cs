
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
            if (outputMinMax.y>outputMinMax.x)
            {
                return ((input - inputMinMax.x) / Mathf.Abs(inputMinMax.y - inputMinMax.x) * Mathf.Abs((outputMinMax.y - outputMinMax.x))) + outputMinMax.x;
            }
            else
            {
                return Mathf.Abs((input - inputMinMax.x) / Mathf.Abs(inputMinMax.y - inputMinMax.x) * Mathf.Abs((outputMinMax.y - outputMinMax.x)) - outputMinMax.x);
            }
            
        }
        
        /// <summary>
        /// 限制重映射
        /// </summary>
        /// <param name="input"></param>
        /// <param name="clampMinMax"></param>
        /// <param name="inputMinMax"></param>
        /// <param name="outputMinMax"></param>
        /// <param name="direction">方向为0是缩小，方向为1是变大</param>
        /// <returns></returns>
        public static float ClampRemap(float input,Vector2 clampMinMax,Vector2 inputMinMax, Vector2 outputMinMax,float direction=0)
        {
            if (input<inputMinMax.y)
            {
                float val = 0f;
                if (direction==0)
                {
                    if (outputMinMax.y>outputMinMax.x)
                    {
                        val=clampMinMax.y*Remap(input,inputMinMax,outputMinMax);
                    }
                    else
                    {
                        Debug.Log("当前系数:"+Remap(input,inputMinMax,outputMinMax));
                        val=clampMinMax.y*(1-Remap(input,inputMinMax,outputMinMax));
                    }
                }

                if (direction==1)
                {
                    if (outputMinMax.y > outputMinMax.x)
                    {
                        val = clampMinMax.x*(1+Math.Abs(Remap(input,inputMinMax,outputMinMax)-1));
                    }
                    else
                    {
                        val = clampMinMax.x*(1+Math.Abs(Remap(input,inputMinMax,outputMinMax)));
                    }
                }
                
                return Math.Clamp(val, clampMinMax.x, clampMinMax.y);
            }
            else
            {
                if (direction==0)
                {
                    return clampMinMax.y;
                }

                if (direction==1)
                {
                    return clampMinMax.x;
                }
            }
            return Remap(input,inputMinMax,outputMinMax);
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