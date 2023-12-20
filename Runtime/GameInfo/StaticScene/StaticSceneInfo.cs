using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;

namespace Pangoo
{
    public partial class StaticSceneInfo : BaseInfo
    {


        AssetPathTable m_AssetPathTable;
        StaticSceneTable m_StaticSceneTable;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.ExcelTable.GetExcelTable<AssetPathTable>();
            m_StaticSceneTable = PangooEntry.ExcelTable.GetExcelTable<StaticSceneTable>();
            foreach (var staticScene in m_StaticSceneTable.Rows)
            {
                var assetPath = m_AssetPathTable.GetRowById(staticScene.AssetPathId);
                IdDict.Add(staticScene.Id, new StaticSceneInfoRow(staticScene.ToInterface(), assetPath.ToInterface()));
            }

        }

        protected override void OnShutdown()
        {
            // foreach (var kv in IdDict)
            // {
            //     kv.Value.Remove();
            // }
        }



    }
}