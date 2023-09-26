using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;

namespace Pangoo
{
    public partial class DynamicObjectInfo : BaseInfo
    {

        AssetPathTable m_AssetPathTable;
        DynamicObjectTable m_DynamicObjectTable;
        EntityGroupTable m_EntityGroupTable;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.ExcelTable.GetExcelTable<AssetPathTable>();
            m_DynamicObjectTable = PangooEntry.ExcelTable.GetExcelTable<DynamicObjectTable>();
            m_EntityGroupTable = PangooEntry.ExcelTable.GetExcelTable<EntityGroupTable>();
            foreach (var row in m_DynamicObjectTable.Rows)
            {
                var assetPath = m_AssetPathTable.GetRowById(row.AssetPathId);
                var entityGroup = m_EntityGroupTable.GetRowById(1);
                IdDict.Add(row.Id, new DynamicObjectInfoRow(row, assetPath, entityGroup));
            }

        }

    }
}