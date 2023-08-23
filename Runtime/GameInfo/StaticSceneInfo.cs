using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;

namespace Pangoo
{   
    public partial class StaticSceneInfo:BaseInfo
    {

        Dictionary<int,StaticSceneInfoRow> Dict = new Dictionary<int, StaticSceneInfoRow>();

        AssetPathTable m_AssetPathTable;
        AssetPackageTable m_AssetPackageTable;
        StaticSceneTable m_StaticSceneTable;
        EntityGroupTable m_EntityGroupTable;
        protected override void OnInit(){
            m_AssetPathTable = PangooEntry.ExcelTable.GetExcelTable<AssetPathTable>();
            m_AssetPackageTable = PangooEntry.ExcelTable.GetExcelTable<AssetPackageTable>();
            m_StaticSceneTable = PangooEntry.ExcelTable.GetExcelTable<StaticSceneTable>();
            m_EntityGroupTable = PangooEntry.ExcelTable.GetExcelTable<EntityGroupTable>();
            foreach(var staticScene in m_StaticSceneTable.Rows){
                var assetPath=  m_AssetPathTable.GetRowById(staticScene.AssetPathId);
                var assetPackage = m_AssetPackageTable.GetRowById(assetPath.AssetPackageId);
                var entityGroup = m_EntityGroupTable.GetRowById(staticScene.EntityGroupId);
                Dict.Add(staticScene.Id,new StaticSceneInfoRow(staticScene,assetPath,assetPackage,entityGroup));
            }

        }

        protected override void OnShutdown(){
            foreach(var kv in Dict){
                kv.Value.Remove();
            }
        }


        public StaticSceneInfoRow GetRowById(int id){
            StaticSceneInfoRow ret;
            if(Dict.TryGetValue(id,out ret)){
                return ret;
            }

            return null;
        }



    }
}