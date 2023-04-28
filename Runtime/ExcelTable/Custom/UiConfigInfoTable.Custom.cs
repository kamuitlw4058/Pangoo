// 本文件使用工具自动生成，请勿进行手动修改！

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LitJson;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pangoo
{
    public partial class UiConfigInfoTable
    {

        Dictionary<string, UiConfigInfoRow> m_Dictionary;

        /// <summary> 用户处理 </summary>
        public override void CustomInit()
        {
            if (m_Dictionary == null)
            {
                m_Dictionary = Rows.ToDictionary(row => row.Name);
            }
        }


        public UiConfigInfoRow GetConfigByType<T>()
        {
            var name = typeof(T).Name;
            return GetConfigByType(name);
        }

        public UiConfigInfoRow GetConfigByType(string name)
        {
            if (m_Dictionary.TryGetValue(name, out UiConfigInfoRow row))
            {
                return row;
            }

            return null;

        }

    }
}

