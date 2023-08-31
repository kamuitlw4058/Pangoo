#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;


namespace Pangoo
{

    public static class DynamicObjectOverviewExtension
    {

        public static void RemoveRowById(this DynamicObjectTableOverview overview, int Id){
                var dynamicObjectRow = overview.Data.GetRowById(Id);
                overview.Data.Rows.Remove(dynamicObjectRow);
                EditorUtility.SetDirty(overview);
        }

        public static void FullRemoveRowById(this DynamicObjectTableOverview overview, int Id){
            var dynamicObjectRow = overview.Data.GetRowById(Id);
            overview.RemoveRowById(Id);
            var assetPathOverview = GameSupportEditorUtility.GetExcelTableOverviewByRowId<AssetPathTableOverview>(dynamicObjectRow.AssetPathId);
            assetPathOverview.FullRemoveRowById(dynamicObjectRow.AssetPathId);



        }




    }
}
#endif