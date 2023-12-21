using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{
    public partial class StaticSceneInfo : BaseInfo
    {

        MetaTable.AssetPathTable m_AssetPathTable;
        MetaTable.StaticSceneTable m_StaticSceneTable;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.MetaTable.GetMetaTable<MetaTable.AssetPathTable>();
            m_StaticSceneTable = PangooEntry.MetaTable.GetMetaTable<MetaTable.StaticSceneTable>();
            foreach (var staticScene in m_StaticSceneTable.BaseRows)
            {
                var staticSceneRow = staticScene as StaticSceneRow;
                var assetPath = m_AssetPathTable.GetRowByUuid(staticSceneRow.AssetPathUuid);
                IdDict.Add(staticScene.Uuid, new StaticSceneInfoRow(staticScene as StaticSceneRow, assetPath));
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