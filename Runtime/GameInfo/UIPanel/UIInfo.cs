using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;
using Pangoo.MetaTable;

namespace Pangoo
{
    public partial class UIInfo : BaseInfo
    {

        MetaTable.AssetPathTable m_AssetPathTable;
        MetaTable.SimpleUITable m_SimpleUITable;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.MetaTable.GetMetaTable<MetaTable.AssetPathTable>();
            m_SimpleUITable = PangooEntry.MetaTable.GetMetaTable<MetaTable.SimpleUITable>();
            foreach (var row in m_SimpleUITable.BaseRows)
            {
                var uiRow = row as SimpleUIRow;
                var assetPath = m_AssetPathTable.GetRowByUuid(uiRow.AssetPathUuid);
                IdDict.Add(row.Uuid, new UIInfoRow(uiRow, assetPath));
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