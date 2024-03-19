using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using MetaTable.Editor;
using MetaTable;
using Pangoo.MetaTable;


namespace Pangoo.Editor
{
    public class MetaTableRefreshOverview
    {
        [MenuItem("Pangoo/刷新所有Overview", false, 11)]
        private static void RefreshOverview()
        {
            Debug.Log($"刷新所有Overview");
            var overviews = AssetDatabaseUtility.FindAsset<MetaTableOverview>();
            foreach (var overview in overviews)
            {
                overview.RefreshRows();
                EditorUtility.SetDirty(overview);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

    }

}