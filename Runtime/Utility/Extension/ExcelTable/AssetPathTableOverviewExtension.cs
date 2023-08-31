#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;


namespace Pangoo
{

    public static class AssetPathTableOverviewExtension
    {

        public static void RemoveRowById(this AssetPathTableOverview overview, int Id){
                var assetPathRow = overview.Data.GetRowById(Id);
                overview.Data.Rows.Remove(assetPathRow);
                EditorUtility.SetDirty(overview);
                AssetDatabase.SaveAssets();
        }

        public static void FullRemoveRowById(this AssetPathTableOverview overview, int Id){
                var assetPathRow = overview.Data.GetRowById(Id);
                overview.RemoveRowById(Id);
                var prefab_path = assetPathRow.ToPrefabPath();
                AssetDatabase.DeleteAsset(prefab_path);
                AssetDatabase.SaveAssets();
        }




    }
}
#endif