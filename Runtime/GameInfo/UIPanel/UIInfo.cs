using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;

namespace Pangoo
{
    public partial class UIInfo : BaseInfo
    {

        AssetPathTable m_AssetPathTable;
        SimpleUITable m_SimpleUITable;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.ExcelTable.GetExcelTable<AssetPathTable>();
            m_SimpleUITable = PangooEntry.ExcelTable.GetExcelTable<SimpleUITable>();
            foreach (var row in m_SimpleUITable.Rows)
            {
                var assetPath = m_AssetPathTable.GetRowById(row.AssetPathId);
                IdDict.Add(row.Id, new UIInfoRow(row, assetPath));
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