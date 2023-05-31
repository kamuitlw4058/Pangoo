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


    public static string ClearEndEmpty(string s)
    {
        return s.TrimEnd(new char[] { ' ', '\r', '\n' });
    }
}
