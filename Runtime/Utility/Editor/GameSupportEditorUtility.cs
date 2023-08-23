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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

namespace Pangoo
{

    public static class GameSupportEditorUtility
    {
#if UNITY_EDITOR
        public static IEnumerable GetAssembly()
        {
            List<string> paths = new List<string>();
            IEnumerable<string> assets = AssetDatabase.FindAssets("t:AssemblyDefinitionAsset");
            return assets.Select(x =>
            {
                var path = AssetDatabase.GUIDToAssetPath(x);
                return Path.GetFileNameWithoutExtension(path);
            });
        }


        public static IEnumerable GetAllSceneDirs()
        {
            List<string> paths = new List<string>();
            IEnumerable<string> assets = AssetDatabase.FindAssets("t:Scene");
            return assets.Select(x =>
            {
                var path = AssetDatabase.GUIDToAssetPath(x);
                return Path.GetDirectoryName(path).Replace("\\", "/");
            });
            
        }

        public static IEnumerable<string> GetAllScenes(string prefix)
        {
            IEnumerable<string> assets = AssetDatabase.FindAssets("t:Scene");
            if (!string.IsNullOrEmpty(prefix))
            {
                assets = assets.Where(x => AssetDatabase.GUIDToAssetPath(x).StartsWith(prefix));
            }
            
            return assets.Select(x =>
            {
                var path = AssetDatabase.GUIDToAssetPath(x);
                return Path.GetFileNameWithoutExtension(path);
            });
        }

        public static IEnumerable GetGameSectionOverview(){
            return AssetDatabaseUtility.FindAsset<GameSectionTableOverview>();
        }

        
        public static IEnumerable GetGameSectionIds(){
            var overviews = AssetDatabaseUtility.FindAsset<GameSectionTableOverview>();
            var ret = new ValueDropdownList<int>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                    ret.Add($"{row.Id}-{row.Name}", row.Id);
                }
            }
            return ret;
        }


        public static GameSectionTable.GameSectionRow GetGameSectionRowById(int id){
            var overviews = AssetDatabaseUtility.FindAsset<GameSectionTableOverview>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                   if(row.Id == id){
                    return row;
                   }
                }
            }
            return null;
        }

        public static DynamicObjectTable.DynamicObjectRow GetDynamicObjectRow(int id){
            var overviews = AssetDatabaseUtility.FindAsset<DynamicObjectTableOverview>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                   if(row.Id == id){
                    return row;
                   }
                }
            }
            return null;
        }

        public static StaticSceneTable.StaticSceneRow GetStaticSceneRowById(int id){
            var overviews = AssetDatabaseUtility.FindAsset<StaticSceneTableOverview>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                   if(row.Id == id){
                    return row;
                   }
                }
            }
            return null;
        }

        public static AssetPathTable.AssetPathRow GetAssetPathRowById(int id){
            var overviews = AssetDatabaseUtility.FindAsset<AssetPathTableOverview>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                   if(row.Id == id){
                    return row;
                   }
                }
            }
            return null;
        }


        public static AssetPackageTable.AssetPackageRow GetAssetPackageById(int id){
            var overviews = AssetDatabaseUtility.FindAsset<AssetPackageTableOverview>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                   if(row.Id == id){
                    return row;
                   }
                }
            }
            return null;
        }



        public static IEnumerable GetAllExcelOverview()
        {
            var datas = AssetDatabaseUtility.FindAsset<ExcelTableOverview>();
            return datas;
        }

        public static IEnumerable GetAllEventsOverview()
        {
            // var datas = AssetDatabaseUtility.FindAsset<PangooEventsTableOverview>();
            // return datas;
            return null;
        }

        public static IEnumerable GetAllVolumeOverview(){
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            var ret = new ValueDropdownList<VolumeTableOverview>();
            foreach(var overview in overviews){
                ret.Add(overview.Config.MainNamespace, overview);
            }

            return ret;
        }

   

        public static IEnumerable GetVolumeRow(){
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            var ret = new ValueDropdownList<int>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                    ret.Add($"{row.Id}-{row.Name}", row.Id);
                }
                
            }
            return ret;
        }



        public static bool CheckVolumeId(int id){
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                    if(row.Id == id){
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool ExistsExcelTableOverviewId<T>(int id,string packageDir = null) where T: ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach(var overview in overviews){
                foreach(var row in overview.Table.BaseRows){
                    if(row.Id == id){
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool CheckVolumeDupName(string name){
            var overviews = AssetDatabaseUtility.FindAsset<VolumeTableOverview>();
            foreach(var overview in overviews){
                foreach(var row in overview.Data.Rows){
                    if(row.Name == name){
                        return false;
                    }
                }
            }

            return true;
        }


        public static IEnumerable GetGameInfoItem(){
            var infos = Utility.Assembly.GetTypes(typeof(BaseInfo));
            var ret = new ValueDropdownList<string>();
            foreach(var info in infos){
                ret.Add(info.FullName);
            }

            return ret;
        }


        public static IEnumerable<string> GetTypeNames<T>(){
            return TypeUtility.GetRuntimeTypeNames(typeof(T));
        }

        public static IEnumerable<System.Type> GetTypes<T>(){
            return TypeUtility.GetRuntimeTypes(typeof(T));
        }


        public static IEnumerable GetPackageConfig()
        {
            var datas = AssetDatabaseUtility.FindAsset<PackageConfig>();
            var ret = new ValueDropdownList<PackageConfig>();
            foreach(var data in datas){
                ret.Add(data.MainNamespace,data);
            }

            return datas;
        }

        public static IEnumerable GetNamespaces()
        {
            List<string> Namespaces = new List<string>();
            var assets = AssetDatabaseUtility.FindAsset<PackageConfig>();
            var namespaces = assets.Select(x => x.MainNamespace).ToList();
            namespaces.Insert(0, "");
            return namespaces;
        }
#endif
    }
}
