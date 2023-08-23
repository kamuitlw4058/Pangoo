using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = System.Object;

public class StringConvert
{
    public static Object ToValue(Type type, string str, bool multi=false)
    {
        object obj = null;
        str = ClearEndEmpty(str);
        if (type.IsEnum)
        {
            if (multi)
            {
                int value = 0;
                if (str == "无"||str=="None")
                {
                    value = 0;
                }
                else
                {
                    string[] currentEnumNames = str.Split('&');
                    for (int i = 0; i < currentEnumNames.Length; i++)
                    {
                        value += (int)Enum.Parse(type, currentEnumNames[i]);
                    }
                }
                obj = value;
            }
            else
            {
                obj = Enum.Parse(type, str);
            }
        }
        else if (typeof(string).Equals(type))
        {
            obj = str;
        }
        else if (typeof(bool).Equals(type))
        {
            obj = str.Equals("True");
        }
        else if (typeof(int).Equals(type))
        {
            int value = 0;
            int.TryParse(str, out value);
            obj = value;
        }
        else if (typeof(float).Equals(type))
        {
            float value = 0f;
            float.TryParse(str, out value);
            obj = value;
        }
        else if (typeof(Vector3).Equals(type))
        {
            string[] posValue = str.Split(",");
            float x = 0f;
            float y = 0f;
            float z = 0f;

            try
            {
                float.TryParse(posValue[0],out x);
                float.TryParse(posValue[1],out y);
                float.TryParse(posValue[2],out z);
            }
            catch (Exception e)
            {
                Console.WriteLine("表中值不满足3位，请检查数据是否正确");
                throw;
            }
            Vector3 value = new Vector3(x,y,z);
            obj = value;
        }
#if UNITY_EDITOR
        else if (typeof(Sprite).Equals(type))
        {
            string fileNameSprite = str.Replace("(UnityEngine.Sprite)",string.Empty).Trim();
            string[] guids = AssetDatabase.FindAssets(fileNameSprite);
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            List<Sprite> sprites=new List<Sprite>();
            for (int i = 0; i < AssetDatabase.LoadAllAssetsAtPath(path).Length; i++)
            {
                sprites.Add(AssetDatabase.LoadAllAssetsAtPath(path)[i] as Sprite);
            }
            foreach (var sprite in sprites)
            {
                if (sprite!=null)
                {
                    if (sprite.name == fileNameSprite)
                    {
                        Debug.Log("正确");
                        obj = sprite;
                        return obj;
                    }
                }
            }
            //Sprite sprite =AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;
            //o = sprite;
        }else if (typeof(AudioClip).Equals(type))
        {
            string fileNameAudioClip = str.Replace("(UnityEngine.AudioClip)", string.Empty).Trim();
            string[] guids = AssetDatabase.FindAssets(fileNameAudioClip);
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            AudioClip audioClip= AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip)) as AudioClip;
            obj = audioClip;
        }
#endif
        return obj;
    }

    public static Object ToValue(string typeStr,string valueStr)
    {
        var obj = ToValue(ToType(typeStr),valueStr);
        return obj;
    }

    public static Type ToType(string str)
    {
        if (str=="string"||str=="String")
        {
            return typeof(string);
        }
        else if (str=="bool"||str=="Bool")
        {
            return typeof(bool);
        }
        else if (str=="int"||str=="Int")
        {
            return typeof(int);
        }
        else if (str=="float"||str=="Float")
        {
            return typeof(float);
        }
        else
        {
            Debug.LogError("这个字符串没有支持的类型可转换");
        }
        return null;
    }

    public static string ClearEndEmpty(string s)
    {
        return s.TrimEnd(new char[] { ' ', '\r', '\n' });
    }
}
