using Sirenix.OdinInspector;
using UnityEngine;
using LitJson;
using System;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace Pangoo
{
    public abstract class ExcelTableOverview : GameConfigBase
    {
        public string Namespace { get; set; }

        public virtual Type GetDataType()
        {
            return GetType();
        }

        public virtual ExcelTableBase GetExcelTableBase()
        {
            return null;
        }

        public virtual int GetRowCount()
        {
            return 0;
        }

        public virtual string GetName()
        {
            return this.GetType().Name;
        }

        public virtual string GetJsonPath()
        {
            return "";
        }


#if UNITY_EDITOR


        public virtual void LoadFromJson()
        {

        }

        public virtual void SaveJson()
        {
        }

#endif
    }
}

