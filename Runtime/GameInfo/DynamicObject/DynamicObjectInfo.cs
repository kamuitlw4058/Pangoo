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

        MetaTable.AssetPathTable m_AssetPathTable;
        MetaTable.DynamicObjectTable m_DynamicObjectTable;
        MetaTable.EntityGroupTable m_EntityGroupTable;
        protected override void OnInit()
        {
            m_AssetPathTable = PangooEntry.MetaTable.GetMetaTable<MetaTable.AssetPathTable>();
            m_DynamicObjectTable = PangooEntry.MetaTable.GetMetaTable<MetaTable.DynamicObjectTable>();
            foreach (var baseRow in m_DynamicObjectTable.BaseRows)
            {
                var dynamicObejctRow = baseRow as MetaTable.DynamicObjectRow;
                var assetPath = m_AssetPathTable.GetRowByUuid(dynamicObejctRow.AssetPathUuid);
                IdDict.Add(baseRow.Uuid, new DynamicObjectInfoRow(dynamicObejctRow, assetPath));
            }
        }

    }
}