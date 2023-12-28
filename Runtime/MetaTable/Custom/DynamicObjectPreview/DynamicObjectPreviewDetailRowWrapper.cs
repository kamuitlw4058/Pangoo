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
    }
}
#endif

