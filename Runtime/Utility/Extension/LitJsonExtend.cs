using UnityEngine;
using LitJson;
using System;
using Cinemachine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Tilemaps;
using Pangoo;

public class LitJsonExtend
{
#if UNITY_EDITOR
    [InitializeOnLoadMethod]
#endif
    public static void LitJsonInit()
    {
        joinV3Type();
        joinV3IntType();
        joinV2Type();
        joinTileType();
        JoinNoiseSettingsType();
    }

    /// <summary>
    /// List<Vector3> 序列化Vector3
    /// </summary>

    static void joinV3Type()
    {
        Action<Vector3, JsonWriter> writeType = (v, w) =>
        {
            w.Write($"{v.x}|{v.y}|{v.z}");
        };

        JsonMapper.RegisterExporter<Vector3>((v, w) =>
        {
            writeType(v, w);
        });


        JsonMapper.RegisterImporter<string, Vector3>((str) =>
        {
            if (str.IsNullOrWhiteSpace())
            {
                return Vector3.zero;
            }

            var strs = str.Split("|");
            if (strs.Length != 3)
            {
                return Vector3.zero;
            }

            try
            {
                return new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
            }
            catch
            {

            }
            return Vector3.zero;

        });

    }


    /// <summary>
    /// List<Vector3Int> 序列化Vector3Int
    /// </summary>
    static void joinV3IntType()
    {
        Action<Vector3Int, JsonWriter> writeType = (v, w) =>
        {
            w.Write($"{v.x}|{v.y}|{v.z}");
        };

        JsonMapper.RegisterExporter<Vector3Int>((v, w) =>
        {
            writeType(v, w);
        });

        JsonMapper.RegisterImporter<string, Vector3Int>((str) =>
        {
            if (str.IsNullOrWhiteSpace())
            {
                return Vector3Int.zero;
            }

            var strs = str.Split("|");
            if (strs.Length != 3)
            {
                return Vector3Int.zero;
            }

            try
            {
                return new Vector3Int(int.Parse(strs[0]), int.Parse(strs[1]), int.Parse(strs[2]));
            }
            catch
            {

            }
            return Vector3Int.zero;

        });
        

    }

    /// <summary>
    /// List<Vector2> 序列化Vector2
    /// </summary>
    static void joinV2Type()
    {
        Action<Vector2, JsonWriter> writeType = (v, w) =>
        {
            w.Write($"{v.x}|{v.y}");
        };

        JsonMapper.RegisterExporter<Vector2>((v, w) =>
        {
            writeType(v, w);
        });
        JsonMapper.RegisterImporter<string, Vector2>((str) =>
        {
            if (str.IsNullOrWhiteSpace())
            {
                return Vector2.zero;
            }

            var strs = str.Split("|");
            if (strs.Length != 2)
            {
                return Vector2.zero;
            }

            try
            {
                return new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
            }
            catch
            {

            }
            return Vector2.zero;

        });
        // Debug.Log("Vector2加入成功");
    }

    /// <summary>
    /// List<Tile> 序列化Tile
    /// </summary>
    static void joinTileType()
    {
        Action<Tile, JsonWriter> writeType = (v, w) =>
        {
            w.WriteObjectStart();//开始写入对象

            // w.WritePropertyName("data");//写入属性名
            // w.Write("");//写入值 
            w.WriteObjectEnd();
        };

        JsonMapper.RegisterExporter<Tile>((v, w) =>
        {
            writeType(v, w);
        });

        // Debug.Log("Tile加入成功");
    }

    static void JoinNoiseSettingsType()
    {
        Action<NoiseSettings, JsonWriter> writeType = (v, w) =>
        {
            w.Write($"{v.name}");
        };

        JsonMapper.RegisterExporter<NoiseSettings>((v, w) =>
        {
            writeType(v, w);
        });
        
        JsonMapper.RegisterImporter<string, NoiseSettings>((str) =>
        {
            NoiseSettings noiseSettings;
            try
            {
                noiseSettings=Resources.Load<NoiseSettings>($"NoiseSettings/{str}");
                return noiseSettings;
            }
            catch (Exception e)
            {
                
            }

            return null;
        });
    }

}

