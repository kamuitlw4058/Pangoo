// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using OfficeOpenXml;

namespace Pangoo
{
     [Serializable]
    public partial class AssetPathTable : ExcelTableBase
    {
        [Serializable]
        public partial class AssetPathRow
        {

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源ID")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Id")]
            public int Id ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源名称")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Name")]
            public string Name ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源路径")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("AssetPath")]
            public string AssetPath ;

            /// <summary>
            /// Desc: 
            /// </summary>
            [TableTitleGroup("资源描述")]
            [HideLabel]
            [ShowInInspector]
            [JsonMember("Desc")]
            public string Desc ;
        }


        /// <summary>
        /// Desc: 
        /// </summary>
        [TableTitleGroup("资源路径")]
        [HideLabel]
        [ShowInInspector]
        [JsonMember("AssetPath")]
        public List<AssetPathRow> Rows ;


        /// <summary> 获取表头 </summary>
        public override string[] GetHeadNames()
        {
            return new string[]{"Id","Name","AssetPath","Desc"};
        }


        /// <summary> 获取类型名 </summary>
        public override string[] GetTypeNames()
        {
            return new string[]{"int","string","string","string"};
        }


        /// <summary> 获取描述名 </summary>
        public override string[] GetDescNames()
        {
            return new string[]{"资源ID","资源名称","资源路径","资源描述"};
        }


        /// <summary> 获取表的每行数据 </summary>
        public override List<string[]> GetTableRowDataList()
        {
            List<string[]> tmpRowDataList = new List<string[]>();
            foreach (var item in Rows)
            {
                tmpRowDataListAdd(tmpRowDataList,item);
            }
            return tmpRowDataList;
        }
        /// <summary> 从Excel文件重新构建数据 </summary>
        public virtual void LoadExcelFile(string excelFilePath)
        {
          Rows = LoadExcelFile<AssetPathRow>(excelFilePath);
        }


        /// <summary> 反射获取配置文件路径 </summary>
        public static string DataFilePath()
        {
            return "";
        }

    }
}

