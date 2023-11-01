#if UNITY_EDITOR
using System.Collections.Generic;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using System;

using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;



namespace Pangoo
{
    public partial class DynamicObjectDetailWrapper
    {


        [ShowInInspector]
        [PropertyOrder(6)]
        [LabelText("是否使用热点区域")]
        [TabGroup("热点系统", Order = 12)]
        public bool UseHotspot
        {
            get
            {
                return Row?.UseHotspot ?? false;
            }
            set
            {
                Row.UseHotspot = value;
                Save();
            }
        }


        [ShowInInspector]
        [PropertyOrder(7)]
        [LabelText("热点区域范围")]
        [ShowIf("@this.UseHotspot")]
        [TabGroup("热点系统")]
        public float HotspotRadius
        {
            get
            {
                return Row?.HotspotRadius ?? 0f;
            }
            set
            {
                Row.HotspotRadius = value;
                Save();
            }
        }


        [ShowInInspector]
        [PropertyOrder(8)]
        [LabelText("热点区域偏移")]
        [ShowIf("@this.UseHotspot")]
        [TabGroup("热点系统")]
        public Vector3 HotspotOffset
        {
            get
            {
                return Row?.HotspotOffset ?? Vector3.zero;
            }
            set
            {
                Row.HotspotOffset = value;
                Save();
            }
        }

        [ShowInInspector]
        [PropertyOrder(9)]
        [LabelText("热点区域Ids")]
        [ShowIf("@this.UseHotspot")]
        [ValueDropdown("GetHotspotIds", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [TabGroup("热点系统")]

        public int[] HotspotIds
        {
            get
            {
                return Row?.HotspotIds.ToArrInt();
            }
            set
            {
                Row.HotspotIds = value.ToListString();
                Save();
            }
        }


        public IEnumerable GetHotspotIds()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<HotspotTableOverview>();
        }


    }
}
#endif