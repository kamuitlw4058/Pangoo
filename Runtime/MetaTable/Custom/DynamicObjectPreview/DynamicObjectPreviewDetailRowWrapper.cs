#if UNITY_EDITOR

using System;
using System.Collections;
using System.IO;
using System.Linq;
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
    public partial class DynamicObjectPreviewDetailRowWrapper : MetaTableDetailRowWrapper<DynamicObjectPreviewOverview, UnityDynamicObjectPreviewRow>
    {
        [LabelText("预览标题")]
        [ShowInInspector]
        public string Title
        {
            get
            {
                return UnityRow.Row.Title;
            }
            set
            {

                UnityRow.Row.Title = value;
                Save();
            }

        }


        UIPreviewParams m_Params;

        [ShowInInspector]
        public UIPreviewParams Params
        {
            get
            {
                if (m_Params == null)
                {
                    m_Params = new UIPreviewParams();
                    m_Params.Load(UnityRow.Row.Params);
                }
                return m_Params;
            }
            set
            {
                m_Params = value;
                UnityRow.Row.Params = m_Params.Save();
                Save();
            }
        }

        [ShowInInspector]
        public string ParamsString
        {
            get
            {
                return UnityRow.Row.Params;
            }
        }



        KeyCode[] m_InteractKeyCodes;

        [LabelText("交互按键")]
        [ShowInInspector]
        [OnValueChanged("OnInteractKeyCodesChanged", includeChildren: true)]
        public KeyCode[] InteractKeyCodes
        {
            get
            {
                if (m_InteractKeyCodes == null)
                {
                    m_InteractKeyCodes = UnityRow.Row.PreviewInteractKeyCodes.ToSplitArr<KeyCode>();
                }

                return m_InteractKeyCodes;
            }
            set
            {
                m_InteractKeyCodes = value;
                UnityRow.Row.PreviewInteractKeyCodes = value.ToListString();
                Save();
            }
        }

        public void OnInteractKeyCodesChanged()
        {
            Debug.Log($"OnKeyCodesChanged:m_KeyCodes:{m_InteractKeyCodes.Count()}");
            UnityRow.Row.PreviewInteractKeyCodes = m_InteractKeyCodes.ToListString();
            Save();
        }


        KeyCode[] m_KeyCodes;

        [LabelText("退出按键")]
        [ShowInInspector]
        [OnValueChanged("OnKeyCodesChanged", includeChildren: true)]
        public KeyCode[] KeyCodes
        {
            get
            {
                if (m_KeyCodes == null)
                {
                    m_KeyCodes = UnityRow.Row.ExitKeyCodes.ToSplitArr<KeyCode>();
                }

                return m_KeyCodes;
            }
            set
            {
                m_KeyCodes = value;
                UnityRow.Row.ExitKeyCodes = value.ToListString();
                Save();
            }
        }
        // public void OnAdd()
        // {
        //     m_KeyCodes
        // }

        public void OnKeyCodesChanged()
        {
            Debug.Log($"OnKeyCodesChanged:m_KeyCodes:{m_KeyCodes.Count()}");
            UnityRow.Row.ExitKeyCodes = m_KeyCodes.ToListString();
            Save();
        }


        [Button("保存参数")]
        [TableColumnWidth(80, resizable: false)]
        public void SaveParams()
        {
            UnityRow.Row.Params = m_Params.Save();
            Save();
        }

        [Button("加载参数")]
        [TableColumnWidth(80, resizable: false)]
        public void LoadParams()
        {

            if (m_Params == null)
            {
                m_Params = new UIPreviewParams();
                m_Params.Load(UnityRow.Row.Params);
            }

        }
    }
}
#endif

