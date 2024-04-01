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

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class ClueDetailRowWrapper : MetaTableDetailRowWrapper<ClueOverview, UnityClueRow>
    {
        [LabelText("线索标题")]
        [ShowInInspector]
        public string ClueTitle
        {
            get
            {
                return UnityRow.Row.ClueTitle;
            }
            set
            {
                UnityRow.Row.ClueTitle = value;
                Save();
            }
        }



        [LabelText("显示的动态物体")]
        [ValueDropdown("@DynamicObjectOverview.GetUuidDropdown()")]
        [ShowInInspector]
        public string DynamicObjectUuid
        {
            get
            {
                return UnityRow.Row.DynamicObjectUuid;
            }
            set
            {
                UnityRow.Row.DynamicObjectUuid = value;
                UpdatePrefab();
                Save();
            }
        }


        GameObject m_Prefab;


        [ShowInInspector]
        [LabelText("资源预制体")]
        [ReadOnly]
        public GameObject Prefab
        {
            get
            {
                if (m_Prefab == null)
                {
                    m_Prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(UnityRow.Row.DynamicObjectUuid);
                }
                return m_Prefab;
            }
        }

        public void UpdatePrefab()
        {
            m_Prefab = GameSupportEditorUtility.GetPrefabByDynamicObjectUuid(UnityRow.Row.DynamicObjectUuid);
        }

    }
}
#endif

