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
    public class AssetPathDetailWrapper : ExcelTableRowDetailWrapper<AssetPathTableOverview, AssetPathTable.AssetPathRow>
    {
        [ShowInInspector]
        public string AssetType
        {
            get
            {
                return Row.AssetType;
            }
        }

    }
}
#endif