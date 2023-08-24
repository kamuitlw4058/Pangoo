using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using GameFramework;

namespace Pangoo
{

    public static class GameSectionRowExtension
    {
        // public static string ToFullPath(this GameSectionRow.AssetPathRow row){
        //     return Utility.Text.Format("{0}/StreamRes/Prefab/{1}/{2}",row.AssetPackageDir,row.AssetType,row.AssetPath);
        // }

        public static List<int> GetDynamicObjectIdList(this GameSectionTable.GameSectionRow row){
            if(row == null || row.DynamicObjectIds.IsNullOrWhiteSpace()){
                return new List<int>();
            }
            return row.DynamicObjectIds.ToListInt();
        }

         

        public static void AddDynamicObjectId(this GameSectionTable.GameSectionRow row,int id){
            if(row == null){
                Debug.LogError(Utility.Text.Format("row is null.{0}",row));
                return;
            }
            var list = row.GetDynamicObjectIdList();
            list.Add(id);
            row.DynamicObjectIds =  list.ToItemString();
        }


        public static void RemoveDynamicObjectId(this GameSectionTable.GameSectionRow row,int id){
            var list = row.GetDynamicObjectIdList();
            if(list.Contains(id)){
                list.Remove(id);
            }
            row.DynamicObjectIds =  list.ToItemString();
        }



    }
}
