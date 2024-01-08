#if UNITY_EDITOR

using System;
using System.IO;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Xml.Serialization;
using Pangoo.Common;
using MetaTable;
using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class VariablesDetailRowWrapper : MetaTableDetailRowWrapper<VariablesOverview, UnityVariablesRow>
    {
        VariableTypeEnum? m_VariableType;

        [ShowInInspector]
        public int Id
        {
            get
            {
                return UnityRow.Row.Id;
            }
        }

        [ShowInInspector]
        [LabelText("变量值类型")]

        public VariableTypeEnum VariableType
        {
            get
            {
                if (m_VariableType == null)
                {
                    var rowValue = UnityRow.Row?.VariableType.ToEnum<VariableTypeEnum>();
                    if (rowValue == null)
                    {
                        m_VariableType = VariableTypeEnum.DynamicObject;
                        UnityRow.Row.VariableType = m_VariableType.ToString();
                        Save();
                    }
                    else
                    {
                        m_VariableType = rowValue;
                    }

                }

                return m_VariableType.Value;
            }
            set
            {

                m_VariableType = value;
                UnityRow.Row.VariableType = value.ToString();
                Save();
            }
        }

        [ShowInInspector]
        [DelayedProperty]
        [LabelText("变量值Key")]

        public string Key
        {
            get
            {
                return UnityRow.Row?.Key ?? string.Empty;
            }
            set
            {
                Debug.Log("set");

                UnityRow.Row.Key = value;
                Save();
            }
        }

        [ShowInInspector]
        [LabelText("变量值类型")]
        public VariableValueTypeEnum VariableValueType
        {
            get
            {
                if (m_Instance == null)
                {
                    UpdateInstance();
                }

                return UnityRow.Row?.ValueType.ToEnum<VariableValueTypeEnum>() ?? VariableValueTypeEnum.String;
            }
            set
            {
                if (UnityRow.Row != null)
                {
                    UnityRow.Row.ValueType = value.ToString();
                    Save();
                    UpdateInstance();
                }
            }
        }

        Variable m_Instance;

        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        public Variable Instance
        {
            get
            {
                return m_Instance;
            }
            set
            {
                m_Instance = value;
            }
        }

        string GetValueClass(string valType)
        {
            return valType.ToLower() switch
            {
                "string" => typeof(VariableString).FullName,
                "float" => typeof(VariableFloat).FullName,
                "bool" => typeof(VariableBool).FullName,
                "int" => typeof(VariableInt).FullName,
                _ => string.Empty,
            };
        }


        void UpdateInstance()
        {
            if (UnityRow.Row.ValueType.IsNullOrWhiteSpace())
            {
                return;
            }

            var instanceType = GameFramework.Utility.Assembly.GetType(GetValueClass(UnityRow.Row.ValueType));
            if (instanceType == null)
            {
                m_Instance = null;
                return;
            }

            m_Instance = Activator.CreateInstance(instanceType) as Variable;
            m_Instance.Load(UnityRow.Row.DefaultValue);
        }

        [ShowInInspector]
        [ReadOnly]
        [LabelText("默认值字符串")]
        public string DefaultValueString
        {
            get
            {
                return UnityRow.Row?.DefaultValue;
            }
            set
            {
                if (UnityRow.Row != null && Overview != null)
                {
                    UnityRow.Row.DefaultValue = value;
                    Save();
                }
            }
        }


        [Button("保存参数")]
        [TableColumnWidth(80, resizable: false)]
        public void SaveParams()
        {
            DefaultValueString = Instance.Save();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            Instance.Load(UnityRow.Row.DefaultValue);
        }
    }
}
#endif

