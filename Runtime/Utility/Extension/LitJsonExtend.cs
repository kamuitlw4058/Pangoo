
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using UnityEditor;
using UnityEngine.Tilemaps;
using Pangoo;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

public class LitJsonExtend
{
    /// <summary>
    /// List<Vector3> 序列化Vector3
    /// </summary>

    [InitializeOnLoadMethod]
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
    [InitializeOnLoadMethod]
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
    [InitializeOnLoadMethod]
    static void joinV2Type()
    {
        Action<Vector2, JsonWriter> writeType = (v, w) =>
        {
            w.WriteObjectStart();//开始写入对象

            w.WritePropertyName("x");//写入属性名
            w.Write(v.x.ToString());//写入值

            w.WritePropertyName("y");
            w.Write(v.y.ToString());

            w.WriteObjectEnd();
        };

        JsonMapper.RegisterExporter<Vector2>((v, w) =>
        {
            writeType(v, w);
        });

        Debug.Log("Vector2加入成功");
    }

    /// <summary>
    /// List<Tile> 序列化Tile
    /// </summary>
    [InitializeOnLoadMethod]
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

        Debug.Log("Tile加入成功");
    }

}

