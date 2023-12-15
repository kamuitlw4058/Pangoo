using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;
using Pangoo.Common;

namespace Pangoo
{

    public static class GameSectionRowExtension
    {

        public static List<int> GetDynamicObjectIdList(this GameSectionTable.GameSectionRow row)
        {
            if (row == null || row.DynamicObjectIds.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.DynamicObjectIds.ToSplitList<int>();
        }


        public static void AddDynamicObjectId(this GameSectionTable.GameSectionRow row, int id)
        {
            if (row == null)
            {
                Debug.LogError(Utility.Text.Format("row is null.{0}", row));
                return;
            }
            var list = row.GetDynamicObjectIdList();
            list.Add(id);
            list.Sort();
            row.DynamicObjectIds = list.ToListString();
        }


        public static void RemoveDynamicObjectId(this GameSectionTable.GameSectionRow row, int id)
        {
            var list = row.GetDynamicObjectIdList();
            if (list.Contains(id))
            {
                list.Remove(id);
            }
            list.Sort();
            row.DynamicObjectIds = list.ToListString();
        }

        public static List<int> GetDynamicSceneIdList(this GameSectionTable.GameSectionRow row)
        {
            if (row == null || row.DynamicSceneIds.IsNullOrWhiteSpace())
            {
                return new List<int>();
            }
            return row.DynamicSceneIds.ToSplitList<int>();
        }




        public static void AddDynamicSceneId(this GameSectionTable.GameSectionRow row, int id)
        {
            if (row == null)
            {
                Debug.LogError(Utility.Text.Format("row is null.{0}", row));
                return;
            }
            var list = row.GetDynamicSceneIdList();
            list.Add(id);
            list.Sort();
            row.DynamicSceneIds = list.ToListString();
        }


        public static void RemoveDynamicSceneId(this GameSectionTable.GameSectionRow row, int id)
        {
            var list = row.GetDynamicSceneIdList();
            if (list.Contains(id))
            {
                list.Remove(id);
            }
            list.Sort();
            row.DynamicSceneIds = list.ToListString();
        }

    }
}
