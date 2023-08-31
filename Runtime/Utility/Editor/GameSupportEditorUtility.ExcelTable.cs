using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGameFramework.Runtime;
using GameFramework;

namespace Pangoo
{

    public static partial class GameSupportEditorUtility
    {
#if UNITY_EDITOR
       
        public static List<int> GetExcelTableOverviewIds<T>(List<int> ids = null,string packageDir = null) where T: ExcelTableOverview
        {
            List<int> ret = new List<int>();
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach(var overview in overviews){
                foreach(var row in overview.Table.BaseRows){
                    if(ids == null){
                        ret.Add(row.Id);
                    }else{
                        if(!ids.Contains(row.Id)){
                            ret.Add(row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static IEnumerable GetExcelTableOverviewNamedIds<T>(List<int> ids = null,string packageDir = null) where T: ExcelTableOverview
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach(var overview in overviews){
                var namedRows = overview.Table.NamedBaseRows;
                if(namedRows == null){
                    continue;
                }
                foreach(var row in namedRows){
                    if(ids == null){
                        ret.Add($"{row.Id}-{row.Name}",row.Id);
                    }else{
                        if(!ids.Contains(row.Id)){
                           ret.Add($"{row.Id}-{row.Name}",row.Id);
                        }
                    }
                }
            }
            return ret;
        }


        public static bool ExistsExcelTableOverviewId<T>(int id,string packageDir = null) where T: ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach(var overview in overviews){
                foreach(var row in overview.Table.BaseRows){
                    if(row.Id == id){
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool ExistsExcelTableOverviewName<T>(string name,string packageDir = null) where T:ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach(var overview in overviews){
                var rows = overview.Table.NamedBaseRows;
                if(rows == null){
                    return false;
                }

                foreach(var row in rows){
                    if(row.Name == name){
                        return false;
                    }
                }
            }

            return true;
        }


        public static IEnumerable GetGameSectionIds(List<int> ids = null){
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<GameSectionTableOverview>();
            foreach(var overview in overviews){
                var namedRows = overview.Data.Rows;
                 if(namedRows == null){
                    continue;
                }
                foreach(var row in namedRows){
                    if(ids == null){
                        ret.Add($"{row.Id}-{row.Name}",row.Id);
                    }else{
                        if(!ids.Contains(row.Id)){
                           ret.Add($"{row.Id}-{row.Name}",row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        
        public static GameSectionTable.GameSectionRow GetGameSectionRowById(int id){
            return GetExcelTableRowWithOverviewById<GameSectionTableOverview,GameSectionTable.GameSectionRow>(id);
        }

        public static DynamicObjectTable.DynamicObjectRow GetDynamicObjectRow(int id){
            return GetExcelTableRowWithOverviewById<DynamicObjectTableOverview,DynamicObjectTable.DynamicObjectRow>(id);
        }


        public static StaticSceneTable.StaticSceneRow GetStaticSceneRowById(int id){
            return GetExcelTableRowWithOverviewById<StaticSceneTableOverview,StaticSceneTable.StaticSceneRow>(id);
        }

        public static AssetPathTable.AssetPathRow GetAssetPathRowById(int id){
            return GetExcelTableRowWithOverviewById<AssetPathTableOverview,AssetPathTable.AssetPathRow>(id);
        }

#endif
    }
}
   