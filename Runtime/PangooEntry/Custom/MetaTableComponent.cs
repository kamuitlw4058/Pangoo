using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameFramework.Resource;
using LitJson;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityGameFramework.Runtime;
using MetaTable;

namespace Pangoo
{
    public class MetaTableComponent : GameFrameworkComponent
    {
        // #if UNITY_EDITOR
        //         private IEnumerable GetAllAssemblyNames()
        //         {
        //             return GameSupportEditorUtility.GetAssembly();
        //         }

        //         [ValueDropdown("GetAllAssemblyNames")]
        // #endif


        public delegate void MetaTableLoadCompleteCallback();
        // public List<string> AssemblyNames;

        public List<MetaTableOverview> TableOverviews;
#if UNITY_EDITOR

        [Button("引入所有Overview")]
        public void UpdateAllOverviews()
        {
            TableOverviews = AssetDatabaseUtility.FindAsset<MetaTableOverview>().ToList();
        }
#endif

        [ShowInInspector]
        Dictionary<Type, MetaTableBase> m_TableDict = new Dictionary<Type, MetaTableBase>();

        [ShowInInspector]
        // [HideInPlayMode]
        Dictionary<Type, bool> m_ExcelTableLoadStatusDic = new Dictionary<Type, bool>();

        [ShowInInspector]
        [HideInPlayMode]
        public bool IsLoaded { get; private set; }


        public T GetMetaTable<T>() where T : MetaTableBase
        {
            if (m_TableDict.TryGetValue(typeof(T), out var tableBase))
            {
                return (T)tableBase;
            }
            Log.Warning($"获取MetaTable:{typeof(T).Name} 配置表失败！");
            return null;
        }

        // [Button("Load")]
        public void LoadMetaTable(MetaTableLoadCompleteCallback callback = null, bool useOverview = true)
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
                    var table = overview.ToTable();
                    var type = table.GetType();
                    if (m_TableDict.ContainsKey(type))
                    {
                        var origTable = m_TableDict[type];
                        origTable.MergeRows(table.BaseRows);
                        table = origTable;
                    }
                    else
                    {
                        m_TableDict.Add(type, table);
                    }

                    Debug.Log($"当前使用的数据SO名字：{overview.name}");

                }
            }
            else
            {
                Debug.Log("Load ExcelTable Without Overview");
            }



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

        }

#if UNITY_EDITOR

#endif


    }
}