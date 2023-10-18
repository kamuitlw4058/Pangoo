#if UNITY_EDITOR
using System;
using System.Collections;

using Sirenix.OdinInspector;

using Pangoo.Core.VisualScripting;
using GameFramework;


namespace Pangoo.Editor
{
    public class HotspotDetailWrapper : ExcelTableRowDetailWrapper<HotspotTableOverview, HotspotTable.HotspotRow>
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

                return Row?.HotspotType;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.HotspotType = value;
                    Row.Params = "{}";
                    Save();
                    UpdateInstance();
                }
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
            if (Row.HotspotType.IsNullOrWhiteSpace())
            {
                return;
            }

            var instanceType = Utility.Assembly.GetType(Row.HotspotType);
            if (instanceType == null)
            {
                return;
            }

            m_Instance = Activator.CreateInstance(instanceType) as HotSpot;
            m_Instance.LoadParamsFromJson(Row.Params);
        }

        public IEnumerable GetHotspotType()
        {
            return GameSupportEditorUtility.GetSubTypeWithCategory<HotSpot>();
        }

        [ShowInInspector]
        [ReadOnly]
        public string InstructionParams
        {
            get
            {
                return Row?.Params;
            }
            set
            {
                if (Row != null && Overview != null)
                {
                    Row.Params = value;
                    Save();
                }
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
            Instance.LoadParamsFromJson(Row.Params);
        }

    }
}
#endif