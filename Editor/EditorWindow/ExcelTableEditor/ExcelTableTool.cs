
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mono.CompilerServices.SymbolWriter;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Pangoo.Editor
{
    public class DataTableTools
    {
        [TableList]
        public List<ExcelConfigWrapper> ExcelTableConfigs = new List<ExcelConfigWrapper>();


        public DataTableTools()
        {
            Refresh();
        }



        [Button("刷新Excel列表")]
        void Refresh()
        {
            ExcelTableConfigs = AssetDatabaseUtility.FindAsset<ExcelTableConfig>().Select(o => new ExcelConfigWrapper(o)).ToList();
            Debug.Log($"configs:{ExcelTableConfigs.Count}");
        }

        [Button("保存配置")]
        void Save()
        {
            foreach (var excelTableConfig in ExcelTableConfigs)
            {
                excelTableConfig.Config.SaveConfig();
            }
        }


        [Button("Excel生成Table代码")]
        void Build()
        {
            foreach (var excelTableConfig in ExcelTableConfigs)
            {
                excelTableConfig.Config.BuildExcelFile();
            }
        }

        [Button("Excel生成SO")]
        void BuildOverviewSo()
        {
            foreach (var excelTableConfigWrapper in ExcelTableConfigs)
            {
                var config = excelTableConfigWrapper.Config;
                config.ExcelBuildOverviewSo();
            }
        }


        [Serializable]
        public class ExcelConfigWrapper
        {
            public ExcelConfigWrapper(ExcelTableConfig val)
            {
                Config = val;
            }

            [ShowInInspector]
            public string Namespace => Config.Namespace;
            
            [ShowInInspector]
            [TableList]
            public List<ExcelEntry> ExcelList
            {
                get
                {
                    return Config.ExcelList;
                }
                set
                {
                    Config.ExcelList = value;
                }

            }
            
            [HideInInspector]
            public ExcelTableConfig Config;
        }
    }
}