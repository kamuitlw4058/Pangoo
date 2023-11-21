using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using GameFramework;
using Pangoo.Core.VisualScripting;

namespace Pangoo
{

    public static partial class GameSupportEditorUtility
    {
#if UNITY_EDITOR

        public static bool GetExcelTableNameInPangoo(string name)
        {
            var types = Utility.Assembly.GetTypes(typeof(ExcelTableBase));
            foreach (var type in types)
            {
                if (type.FullName == $"Pangoo.{name}Table")
                {
                    return true;
                }
            }

            return false;
        }

        public static List<string> GetExcelTablePangooTableNames()
        {
            return Utility.Assembly.GetTypes(typeof(ExcelTableBase)).Select(o => o.FullName).Where(o => o.StartsWith("Pangoo.")).ToList();

        }

        public static string GetExcelTablePangooTableName(string name)
        {
            return $"Pangoo.{name}Table";
        }



        public static IEnumerable GetExcelTableOverview<T>() where T : ExcelTableOverview
        {
            var ret = new ValueDropdownList<T>();
            var overviews = AssetDatabaseUtility.FindAsset<T>();
            foreach (var overview in overviews)
            {
                ret.Add(overview.Config.MainNamespace, overview);
            }
            return ret;
        }

        public static List<int> GetExcelTableOverviewIds<T>(List<int> ids = null, string packageDir = null) where T : ExcelTableOverview
        {
            List<int> ret = new List<int>();
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Table.BaseRows)
                {
                    if (ids == null)
                    {
                        ret.Add(row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add(row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static IEnumerable GetExcelTableOverviewNamedIds<T>(List<int> excludeIds = null, string packageDir = null, List<int> includeIds = null) where T : ExcelTableOverview
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach (var overview in overviews)
            {
                var namedRows = overview.Table.NamedBaseRows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    if (includeIds != null && includeIds.Count != 0)

                    {
                        if (includeIds.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                        else
                        {
                            continue;
                        }

                    }

                    if (excludeIds == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!excludeIds.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static bool ExistsAssetPath(string packageDir, string name, string assetType, string fileType)
        {
            var path = AssetUtility.GetAssetPath(packageDir, assetType, $"{name}.{fileType}");
            return File.Exists(path);
        }

        public static IEnumerable GetAssetPathIds(List<int> ids = null, List<string> assetTypes = null, string packageDir = null)
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<AssetPathTableOverview>(packageDir);
            foreach (var overview in overviews)
            {
                var Rows = overview.Data.Rows;
                if (Rows == null)
                {
                    continue;
                }
                foreach (var row in Rows)
                {
                    if (assetTypes != null)
                    {
                        if (!assetTypes.Contains(row.AssetType))
                        {
                            continue;
                        }
                    }

                    if (ids == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }



        public static bool ExistsExcelTableOverviewId<T>(int id, string packageDir = null) where T : ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Table.BaseRows)
                {
                    if (row.Id == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool ExistsExcelTableOverviewName<T>(string name, string packageDir = null) where T : ExcelTableOverview
        {
            var overviews = AssetDatabaseUtility.FindAsset<T>(packageDir);
            foreach (var overview in overviews)
            {
                var rows = overview.Table.NamedBaseRows;
                if (rows == null)
                {
                    return false;
                }

                foreach (var row in rows)
                {
                    if (row.Name == name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static IEnumerable GetGameSectionIds(List<int> ids = null)
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<GameSectionTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    if (ids == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static IEnumerable GetCharacterIds(bool onlyPlayer = false, bool hasDefault = false, List<int> ids = null)
        {

            var ret = new ValueDropdownList<int>();
            if (hasDefault)
            {
                ret.Add("Default", 0);
            }
            var overviews = AssetDatabaseUtility.FindAsset<CharacterTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    if (onlyPlayer && !row.IsPlayer) continue;

                    if (ids == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static void TryAddIdByExcludeIds(ValueDropdownList<int> list, ExcelNamedRowBase row, List<int> exclude_ids = null)
        {
            if (exclude_ids == null)
            {
                list.Add($"{row.Id}-{row.Name}", row.Id);
            }
            else
            {
                if (!exclude_ids.Contains(row.Id))
                {
                    list.Add($"{row.Id}-{row.Name}", row.Id);
                }
            }
        }

        public static IEnumerable GetVariableIds(string valueType, string variableType = null, List<int> ids = null)
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<VariablesTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    if (variableType != null && !row.VariableType.Equals(variableType))
                    {
                        continue;
                    }

                    if (valueType.IsNullOrWhiteSpace())
                    {
                        TryAddIdByExcludeIds(ret, row, ids);
                    }
                    else
                    {
                        if (valueType.Equals(row.ValueType))
                        {
                            TryAddIdByExcludeIds(ret, row, ids);
                        }
                    }
                }
            }
            return ret;
        }

        public static IEnumerable GetDynamicObjectIds(bool hasDefault = false, List<int> ids = null)
        {
            var ret = new ValueDropdownList<int>();
            if (hasDefault)
            {
                ret.Add("Self", 0);
            }
            var overviews = AssetDatabaseUtility.FindAsset<DynamicObjectTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    if (ids == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!ids.Contains(row.Id))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }

        public static string GetAssetGroupByAssetGroupId(int assetGroupId)
        {
            if (assetGroupId == 0)
            {
                return null;
            }


            var overviews = AssetDatabaseUtility.FindAsset<AssetGroupTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {

                    if (row.Id.Equals(assetGroupId))
                    {
                        return row.AssetGroup;
                    }
                }
            }
            return null;
        }

        public static int GetAssetGroupIdByAssetGroup(string AssetGroup)
        {
            if (AssetGroup.IsNullOrWhiteSpace())
            {
                return 0;
            }

            var overviews = AssetDatabaseUtility.FindAsset<AssetGroupTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {

                    if (row.AssetGroup.Equals(AssetGroup))
                    {
                        return row.Id;
                    }
                }
            }
            return 0;
        }

        public static IEnumerable GetAssetGroupIdDropdown(string AssetGroup = null)
        {
            var ret = new ValueDropdownList<int>();
            ret.Add("Default", 0);

            var overviews = AssetDatabaseUtility.FindAsset<AssetGroupTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    if (AssetGroup == null)
                    {
                        ret.Add($"{row.Id}-{row.Name}", row.Id);
                    }
                    else
                    {
                        if (!row.AssetGroup.Equals(AssetGroup))
                        {
                            ret.Add($"{row.Id}-{row.Name}", row.Id);
                        }
                    }
                }
            }
            return ret;
        }


        public static IEnumerable GetConditionIds(ConditionTypeEnum valueType, List<int> ids = null)
        {
            var ret = new ValueDropdownList<int>();
            var overviews = AssetDatabaseUtility.FindAsset<ConditionTableOverview>();
            foreach (var overview in overviews)
            {
                var namedRows = overview.Data.Rows;
                if (namedRows == null)
                {
                    continue;
                }
                foreach (var row in namedRows)
                {
                    var condition = ClassUtility.CreateInstance<Condition>(row.ConditionType);
                    if (valueType.Equals(condition.ConditionType))
                    {
                        TryAddIdByExcludeIds(ret, row, ids);
                    }
                }
            }
            return ret;
        }




        public static GameSectionTable.GameSectionRow GetGameSectionRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<GameSectionTableOverview, GameSectionTable.GameSectionRow>(id);
        }

        public static DynamicObjectTable.DynamicObjectRow GetDynamicObjectRow(int id)
        {
            return GetExcelTableRowWithOverviewById<DynamicObjectTableOverview, DynamicObjectTable.DynamicObjectRow>(id);
        }


        public static StaticSceneTable.StaticSceneRow GetStaticSceneRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<StaticSceneTableOverview, StaticSceneTable.StaticSceneRow>(id);
        }

        public static AssetPathTable.AssetPathRow GetAssetPathRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<AssetPathTableOverview, AssetPathTable.AssetPathRow>(id);
        }

        public static TriggerEventTable.TriggerEventRow GetTriggerEventRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<TriggerEventTableOverview, TriggerEventTable.TriggerEventRow>(id);
        }

        public static ConditionTable.ConditionRow GetConditionRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<ConditionTableOverview, ConditionTable.ConditionRow>(id);
        }

        public static InstructionTable.InstructionRow GetInstructionRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<InstructionTableOverview, InstructionTable.InstructionRow>(id);
        }

        public static HotspotTable.HotspotRow GetHotspotRowById(int id)
        {
            return GetExcelTableRowWithOverviewById<HotspotTableOverview, HotspotTable.HotspotRow>(id);
        }

#endif
    }
}
