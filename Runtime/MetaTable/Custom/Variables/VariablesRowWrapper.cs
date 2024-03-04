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
    public partial class VariablesRowWrapper : MetaTableRowWrapper<VariablesOverview, VariablesNewRowWrapper, UnityVariablesRow>
    {
        public override bool CanNameChange => EnabledEdit;
        public override bool CheckName => false;
        public bool EnabledEdit { get; set; }

        [ShowInInspector]
        [EnableIf("EnabledEdit")]
        public string Key
        {
            get
            {
                return UnityRow.Row.Key;
            }
            set
            {
                UnityRow.Row.Key = value;
                Save();
            }
        }


        [ShowInInspector]
        [EnableIf("EnabledEdit")]
        [TableColumnWidth(120, resizable: false)]
        public VariableTypeEnum VariableType
        {
            get
            {
                return UnityRow.Row.VariableType.ToEnum<VariableTypeEnum>();
            }
            set
            {
                UnityRow.Row.VariableType = value.ToString();
                Save();
            }

        }

        [ShowInInspector]
        [EnableIf("EnabledEdit")]
        [TableColumnWidth(100, resizable: false)]
        public VariableValueTypeEnum ValueType
        {
            get
            {
                if (m_Instance == null)
                {
                    UpdateInstance();
                }
                return UnityRow.Row.ValueType.ToEnum<VariableValueTypeEnum>();
            }
            set
            {
                UnityRow.Row.ValueType = value.ToString();
                Save();
                UpdateInstance();
            }
        }


        Variable m_Instance;


        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        [EnableIf("EnabledEdit")]
        [OnValueChanged("OnInstanceChanged")]
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

        public void OnInstanceChanged()
        {
            UnityRow.Row.DefaultValue = m_Instance.Save();
            Save();
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

            if (m_Instance == null || m_Instance.GetType() != instanceType)
            {
                m_Instance = Activator.CreateInstance(instanceType) as Variable;
                m_Instance.Load(UnityRow.Row.DefaultValue);
            }



        }

        [ShowInInspector]
        [ReadOnly]
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


        [ShowInInspector]
        public bool NotSave
        {
            get
            {
                return UnityRow.Row.NotSave;
            }
            set
            {
                UnityRow.Row.NotSave = value;
                Save();
            }
        }


        public override void Edit()
        {
            EnabledEdit = !EnabledEdit;
            EditButtonText = EnabledEdit ? "关闭编辑" : "编辑";
        }
    }
}
#endif

