#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Pangoo.Editor
{
    public class AssetGroupDetailWrapper : ExcelTableRowDetailWrapper<AssetGroupTableOverview, AssetGroupTable.AssetGroupRow>
    {

        [ShowInInspector]
        public string AssetGroup
        {
            get
            {
                if (Row.AssetGroup.IsNullOrWhiteSpace())
                {
                    Row.AssetGroup = Row.Name.ToPinyin();
                    Save();
                }

                return Row.AssetGroup;
            }
            set
            {
                Row.AssetGroup = value;
                Save();
            }
        }




    }
}
#endif