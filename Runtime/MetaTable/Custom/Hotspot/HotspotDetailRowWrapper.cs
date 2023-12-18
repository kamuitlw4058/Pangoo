#if UNITY_EDITOR

using System;
using System.Collections;
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
    public partial class HotspotDetailRowWrapper : MetaTableDetailRowWrapper<HotspotOverview, UnityHotspotRow>
    {
        [ShowInInspector]
        [ValueDropdown("GetHotspotType")]
        public string HotspotType
        {
            get
            {
                if (m_Instance == null)
                {
                    UpdateInstance();
                }

                return UnityRow.Row?.HotspotType;
            }
            set
            {

                UnityRow.Row.HotspotType = value;
                UnityRow.Row.Params = "{}";
                Save();
                UpdateInstance();
            }
        }

        HotSpot m_Instance;

        [ShowInInspector]
        [HideLabel]
        [HideReferenceObjectPicker]
        public HotSpot Instance
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

        void UpdateInstance()
        {
            if (UnityRow.Row.HotspotType.IsNullOrWhiteSpace())
            {
                return;
            }

            var instanceType = AssemblyUtility.GetType(UnityRow.Row.HotspotType);
            if (instanceType == null)
            {
                return;
            }

            m_Instance = Activator.CreateInstance(instanceType) as HotSpot;
            m_Instance.LoadParamsFromJson(UnityRow.Row.Params);
        }

        public IEnumerable GetHotspotType()
        {
            return GameSupportEditorUtility.GetHotspotType();
        }

        [ShowInInspector]
        [ReadOnly]
        public string InstructionParams
        {
            get
            {
                return UnityRow.Row?.Params;
            }
            set
            {

                UnityRow.Row.Params = value;
                Save();
            }
        }

        [Button("保存参数")]
        [TableColumnWidth(80, resizable: false)]
        public void SaveParams()
        {
            InstructionParams = Instance.ParamsToJson();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {
            Instance.LoadParamsFromJson(UnityRow.Row.Params);
        }
    }
}
#endif

