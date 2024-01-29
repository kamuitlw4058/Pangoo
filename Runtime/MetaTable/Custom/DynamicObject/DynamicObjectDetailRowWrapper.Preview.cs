#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using Sirenix.OdinInspector;
using MetaTable;
using Pangoo.Common;
using Pangoo.Core.VisualScripting;

namespace Pangoo.MetaTable
{
    public partial class DynamicObjectDetailRowWrapper : MetaTableDetailRowWrapper<DynamicObjectOverview, UnityDynamicObjectRow>
    {
        [LabelText("预览显示名字")]
        [TabGroup("预览")]
        [ShowInInspector]
        public string PreviewName
        {
            get
            {
                return UnityRow.Row.PreviewName;
            }
            set
            {
                UnityRow.Row.PreviewName = value;
                Save();
            }
        }

        [LabelText("预览描述")]
        [TabGroup("预览")]
        [ShowInInspector]
        public string PreviewDesc
        {
            get
            {
                return UnityRow.Row.PreviewDesc;
            }
            set
            {
                UnityRow.Row.PreviewDesc = value;
                Save();
            }
        }

        [LabelText("预览朝向")]
        [TabGroup("预览")]
        [ShowInInspector]
        public Vector3 PreviewRotation
        {
            get
            {
                return UnityRow.Row.PreviewRotation;
            }
            set
            {
                UnityRow.Row.PreviewRotation = value;
                Save();
            }
        }


        [LabelText("预览缩放")]
        [TabGroup("预览")]
        [ShowInInspector]
        public Vector3 PreviewScale
        {
            get
            {
                return UnityRow.Row.PreviewScale;
            }
            set
            {
                UnityRow.Row.PreviewScale = value;
                Save();
            }
        }




    }
}
#endif

