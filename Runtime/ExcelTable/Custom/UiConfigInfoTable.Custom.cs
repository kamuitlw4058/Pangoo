using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using Pangoo;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;

namespace Pangoo
{
    public partial class UiConfigInfoTable
    {
        private Dictionary<string, UiConfigInfoRow> m_UiConfigDict= new Dictionary<string, UiConfigInfoRow>();
        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            Rows.ForEach(rowData =>
            {
                m_UiConfigDict.Add(rowData.Name,rowData);
                Debug.Log(">>>>>>>:"+rowData.Name);
            });
        }

        public override void Merge(ExcelTableBase val)
        {
            var table = val as UiConfigInfoTable;
            Rows.AddRange(table.Rows);
        }
        
        public UiConfigInfoRow GetConfigByType(string typeName)
        {
            UiConfigInfoRow row;
            if (m_UiConfigDict.TryGetValue(typeName,out row))
            {
                return row;
            }
            return null;
        }

        public UiConfigInfoRow GetConfigByType<T>()
        {

            return GetConfigByType(typeof(T).Name);
        }
    }
}