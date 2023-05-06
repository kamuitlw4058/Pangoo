using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Resource;
using LitJson;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Pangoo
{
    public class ExcelTableComponent : GameFrameworkComponent
    {
// #if UNITY_EDITOR
//         private IEnumerable GetAllAssemblyNames()
//         {
//             return GameSupportEditorUtility.GetAssembly();
//         }

//         [ValueDropdown("GetAllAssemblyNames")]
// #endif


        public delegate void ExcelTableLoadCompleteCallback();
        // public List<string> AssemblyNames;

        public List<ExcelTableOverview> TableOverviews;

        [ShowInInspector]
        Dictionary<Type, ExcelTableBase> m_ExcelTableDic = new Dictionary<Type, ExcelTableBase>();

        [ShowInInspector]
        Dictionary<Type, bool> m_ExcelTableLoadStatusDic = new Dictionary<Type, bool>();

        public bool IsLoaded { get; private set; }



        public T GetExcelTable<T>() where T : ExcelTableBase
        {
            if (m_ExcelTableDic.TryGetValue(typeof(T), out var tableBase))
            {
                return (T)tableBase;
            }
            Log.Warning($"获取ExcelTable:{typeof(T).Name} 配置表失败！");
            return null;
        }


        public void LoadExcelTable(ExcelTableLoadCompleteCallback callback = null, bool useOverview = true)
        {
            if (IsLoaded)
            {
                return;
            }
            List<Type> typeList = new List<Type>();

            if (useOverview)
            {
                foreach (var overview in TableOverviews)
                {
                    var type = overview.GetDataType();
                    var table = overview.GetExcelTableBase();
                    if (m_ExcelTableDic.ContainsKey(type))
                    {
                        var origTable = m_ExcelTableDic[type];
                        origTable.Merge(table);
                        table = origTable;
                    }
                    else
                    {
                        m_ExcelTableDic.Add(type, table);
                    }
                    table.CustomInit();
                }
            }

            // foreach (var assemblyName in AssemblyNames)
            // {
            //     var types = Assembly.Load(assemblyName).GetTypes();
            //     foreach (var t in types)
            //     {
            //         if (!t.IsClass || t.IsAbstract)
            //         {
            //             continue;
            //         }

            //         if (t.IsSubclassOf(typeof(ExcelTableBase)))
            //         {
            //             typeList.Add(t);
            //             m_ExcelTableLoadStatusDic.Add(t, false);

            //         }
            //     }
            // }
            // for (int i = 0; i < typeList.Count; i++)
            // {
            //     Load(typeList[i], callback);
            // }

            IsLoaded = true;
            if (callback != null)
            {
                callback();
            }

        }
        ResourceComponent ResouceCom;


        protected void Start()
        {
            ResouceCom = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceComponent>();
            foreach (var overview in TableOverviews)
            {
            }
        }

        public void Load(Type tableType, ExcelTableLoadCompleteCallback callback = null)
        {
            var method = tableType.GetMethod("DataFilePath", BindingFlags.Public | BindingFlags.Static);
            if (method == null)
            {
                Log.Error($"加载数据配置表 '{tableType.Name}' 缺少文件路径定义");
                return;
            }
            var fileName = (string)method.Invoke(null, null);
            Log.Info($"开始加载数据配置表 '{tableType.Name}' path:{fileName}");
            ResouceCom.LoadAsset(fileName, 0, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    if (asset is TextAsset textAsset)
                    {

                        try
                        {
                            var dataTableClass = (ExcelTableBase)JsonMapper.ToObject(textAsset.text, tableType);
                            m_ExcelTableDic.Add(tableType, dataTableClass);
                            dataTableClass.CustomInit();
                            ResouceCom.UnloadAsset(asset);
                            Log.Info($"加载数据配置表 '{tableType.Name}' 完成!");
                        }
                        catch (Exception e)
                        {
                            Log.Error($"加载数据配置表 '{tableType.Name}' 失败. Message: {e}");
                        }
                    }
                    else
                    {
                        Log.Error($"加载数据配置表 '{tableType.Name}' 失败. Message: 普通文本资源");
                    }
                    m_ExcelTableLoadStatusDic[tableType] = true;

                    bool flag = true;
                    foreach (var value in m_ExcelTableLoadStatusDic.Values)
                    {
                        if (!value)
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        IsLoaded = true;
                        if (callback != null)
                        {
                            callback();
                        }
                    }



                },
                 (assetName, asset, errorMessage, userData) =>
                 {
                     Log.Error($"加载数据配置表 '{tableType.Name}' 失败. Message:{errorMessage}");
                 }
            ));
        }

    }
}