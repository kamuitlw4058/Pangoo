#if UNITY_EDITOR

using System.Collections;
using UnityEngine;

using Sirenix.OdinInspector;
using MetaTable;
using Pangoo.Common;

namespace Pangoo.MetaTable
{
    public partial class DynamicObjectDetailRowWrapper : MetaTableDetailRowWrapper<DynamicObjectOverview, UnityDynamicObjectRow>
    {
        [ShowInInspector]
        [LabelText("默认关闭交互")]
        [TabGroup("交互系统")]
        [PropertyOrder(1)]
        public bool DefaultDisableInteract
        {
            get
            {

                return UnityRow.Row.DefaultDisableInteract;
            }
            set
            {
                UnityRow.Row.DefaultDisableInteract = value;
                Save();
            }
        }


        [ShowInInspector]
        [LabelText("交互对象")]
        [TabGroup("交互系统")]
        [PropertyOrder(1)]
        [ValueDropdown("OnTargetDropdown")]
        public string InteractTarget
        {
            get
            {
                if (UnityRow.Row?.InteractTarget?.IsNullOrWhiteSpace() ?? true)
                {
                    UnityRow.Row.InteractTarget = ConstString.Self;
                    Save();
                }

                return UnityRow.Row?.InteractTarget ?? ConstString.Self;
            }
            set
            {
                UnityRow.Row.InteractTarget = value;
                Save();
            }
        }

        [ShowInInspector]

        [LabelText("交互范围")]
        // [ShowIf("@this.UseHotspot")]
        [TabGroup("交互系统")]
        [PropertyOrder(3)]
        public float InteractRadius
        {
            get
            {
                return UnityRow.Row?.InteractRadius ?? 0f;
            }
            set
            {
                UnityRow.Row.InteractRadius = value;
                Save();
            }
        }


        [ShowInInspector]
        [LabelText("交互偏移")]
        // [ShowIf("@this.UseHotspot")]
        [TabGroup("交互系统")]
        [PropertyOrder(2)]
        public Vector3 InteractOffset
        {
            get
            {
                return UnityRow.Row?.InteractOffset ?? Vector3.zero;
            }
            set
            {
                UnityRow.Row.InteractOffset = value;
                Save();
            }
        }

        [ShowInInspector]
        [LabelText("交互角度")]
        // [ShowIf("@this.UseHotspot")]
        [TabGroup("交互系统")]
        [PropertyOrder(4)]
        public float InteractRadian
        {
            get
            {
                return UnityRow.Row?.InteractRadian ?? 0;
            }
            set
            {
                UnityRow.Row.InteractRadian = value;
                Save();
            }
        }



        IEnumerable OnTargetDropdown()
        {
            return GameSupportEditorUtility.RefPrefabStringDropdown(Prefab);
        }


        [ShowInInspector]
        [LabelText("是否使用热点区域")]
        [TabGroup("交互系统")]
        [PropertyOrder(5)]
        public bool UseHotspot
        {
            get
            {
                return UnityRow.Row?.UseHotspot ?? false;
            }
            set
            {
                UnityRow.Row.UseHotspot = value;
                Save();
            }
        }

        [ShowInInspector]
        [LabelText("默认关闭热点区域")]
        [TabGroup("交互系统")]
        [PropertyOrder(6)]
        public bool DefaultHideHotspot
        {
            get
            {
                return UnityRow.Row?.DefaultHideHotspot ?? false;
            }
            set
            {
                UnityRow.Row.DefaultHideHotspot = value;
                Save();
            }
        }



        [ShowInInspector]

        [LabelText("热点区域范围")]
        [ShowIf("@this.UseHotspot")]
        [TabGroup("交互系统")]
        [PropertyOrder(6)]
        public float HotspotRadius
        {
            get
            {
                return UnityRow.Row?.HotspotRadius ?? 0f;
            }
            set
            {
                UnityRow.Row.HotspotRadius = value;
                Save();
            }
        }



        [ShowInInspector]

        [LabelText("热点区域Uuids")]
        [ShowIf("@this.UseHotspot")]
        [ValueDropdown("GetHotspotUuids", IsUniqueList = true)]
        [ListDrawerSettings(Expanded = true)]
        [TabGroup("交互系统")]
        [PropertyOrder(8)]
        public string[] HotspotUuids
        {
            get
            {
                return UnityRow.Row?.HotspotUuids.ToSplitArr<string>();
            }
            set
            {
                UnityRow.Row.HotspotUuids = value.ToListString();
                Save();
            }
        }


        public IEnumerable GetHotspotIds()
        {
            return GameSupportEditorUtility.GetHotspotIds();
        }

        public IEnumerable GetHotspotUuids()
        {
            return GameSupportEditorUtility.GetHotspotUuids();
        }



    }
}
#endif

