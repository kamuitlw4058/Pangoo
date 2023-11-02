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
        [PropertyOrder(1)]
        [LabelText("交互范围")]
        // [ShowIf("@this.UseHotspot")]
        [TabGroup("交互系统")]
        public float InteractRadius
        {
            get
            {
                return Row?.InteractRadius ?? 0f;
            }
            set
            {
                Row.InteractRadius = value;
                Save();
            }
        }


        [ShowInInspector]
        [LabelText("交互偏移")]
        // [ShowIf("@this.UseHotspot")]
        [TabGroup("交互系统")]
        public Vector3 InteractOffset
        {
            get
            {
                return Row?.InteractOffset ?? Vector3.zero;
            }
            set
            {
                Row.InteractOffset = value;
                Save();
            }
        }

        [ShowInInspector]
        [LabelText("交互角度")]
        // [ShowIf("@this.UseHotspot")]
        [TabGroup("交互系统")]
        public float InteractRadian
        {
            get
            {
                return Row?.InteractRadian ?? 0;
            }
            set
            {
                Row.InteractRadian = value;
                Save();
            }
        }



        [ShowInInspector]
        [PropertyOrder(2)]
        [LabelText("是否使用热点区域")]
        [TabGroup("交互系统")]
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
        [TabGroup("交互系统")]
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
        [PropertyOrder(9)]
        [LabelText("热点区域Ids")]
        [ShowIf("@this.UseHotspot")]
        [ValueDropdown("GetHotspotIds", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [TabGroup("交互系统")]

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