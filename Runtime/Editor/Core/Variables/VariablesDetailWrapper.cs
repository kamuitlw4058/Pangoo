#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using Pangoo.Core.VisualScripting;
using UnityEngine;

namespace Pangoo
{
    public class VariablesDetailWrapper : ExcelTableRowDetailWrapper<VariablesTableOverview, VariablesTable.VariablesRow>
    {
        VariableTypeEnum? m_VariableType;

        [ShowInInspector]
        public VariableTypeEnum VariableType
        {
            get
            {
                if (m_VariableType == null)
                {
                    var rowValue = Row?.VariableType.ToEnum<VariableTypeEnum>();
                    if (rowValue == null)
                    {
                        m_VariableType = VariableTypeEnum.DynamicObject;
                        Row.VariableType = m_VariableType.ToString();
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
                if (Row != null)
                {
                    m_VariableType = value;
                    Row.VariableType = value.ToString();
                    Save();
                }
            }
        }

        [ShowInInspector]
        [DelayedProperty]
        public string Key
        {
            get
            {
                return Row?.Key ?? string.Empty;
            }
            set
            {
                Debug.Log("set");
                if (Row != null)
                {
                    Row.Key = value;
                    Save();
                }
            }
        }

        [ShowInInspector]
        public VariableValueTypeEnum VariableValueType
        {
            get
            {
                if (m_Instance == null)
                {
                    UpdateInstance();
                }

                return Row?.ValueType.ToEnum<VariableValueTypeEnum>() ?? VariableValueTypeEnum.String;
            }
            set
            {
                if (Row != null)
                {
                    Row.ValueType = value.ToString();
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
            if (Row.ValueType.IsNullOrWhiteSpace())
            {
                return;
            }

            var instanceType = GameFramework.Utility.Assembly.GetType(GetValueClass(Row.ValueType));
            if (instanceType == null)
            {
                m_Instance = null;
                return;
            }

            m_Instance = Activator.CreateInstance(instanceType) as Variable;
            m_Instance.Load(Row.DefaultValue);
        }

        [ShowInInspector]
        [ReadOnly]
        [LabelText("默认值字符串")]
        public string DefaultValueString
        {
            get
            {
                return Row?.DefaultValue;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.DefaultValue = value;
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
            Instance.Load(Row.DefaultValue);
        }


    }


}
#endif